using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pastella.Backend.Application.Services;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using FcmNotification = FirebaseAdmin.Messaging.Notification; // Alias kullanarak çakışmayı çözüyoruz

namespace Pastella.Backend.Infrastructure.ExternalServices
{
    public class FCMService : IFCMService
    {
        private readonly IRepository<DeviceToken> _deviceTokenRepository;
        private readonly ILogger<FCMService> _logger;
        private readonly FirebaseMessaging _messaging;

        public FCMService(IRepository<DeviceToken> deviceTokenRepository, ILogger<FCMService> logger, IConfiguration configuration)
        {
            _deviceTokenRepository = deviceTokenRepository;
            _logger = logger;

            // Firebase Admin SDK initialization
            if (FirebaseApp.DefaultInstance == null)
            {
                var credential = GoogleCredential.FromFile(configuration["Firebase:ServiceAccountKeyPath"]);
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = credential,
                    ProjectId = configuration["Firebase:ProjectId"]
                });
            }
            
            _messaging = FirebaseMessaging.DefaultInstance;
        }

        public async Task<bool> SendToDevice(string deviceToken, string title, string body, Dictionary<string, string>? data = null)
        {
            try
            {
                var message = new Message()
                {
                    Token = deviceToken,
                    Notification = new FcmNotification() // Alias kullanıyoruz
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data ?? new Dictionary<string, string>(),
                    Android = new AndroidConfig()
                    {
                        Notification = new AndroidNotification()
                        {
                            Icon = "ic_notification",
                            Color = "#FF6B35" // Pastella brand color
                        }
                    },
                    Apns = new ApnsConfig()
                    {
                        Aps = new Aps()
                        {
                            Alert = new ApsAlert()
                            {
                                Title = title,
                                Body = body
                            },
                            Badge = 1,
                            Sound = "default"
                        }
                    }
                };

                var response = await _messaging.SendAsync(message);
                _logger.LogInformation($"FCM message sent successfully: {response}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send FCM message to device: {deviceToken}");
                return false;
            }
        }

        public async Task<bool> SendToUser(int userId, string title, string body, Dictionary<string, string>? data = null)
        {
            try
            {
                var userTokens = await GetUserActiveTokens(userId);
                if (!userTokens.Any())
                {
                    _logger.LogWarning($"No active device tokens found for user: {userId}");
                    return false;
                }

                var tokens = userTokens.Select(t => t.Token).ToList();
                return await SendToMultipleDevices(tokens, title, body, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send FCM message to user: {userId}");
                return false;
            }
        }

        public async Task<bool> SendToMultipleDevices(List<string> deviceTokens, string title, string body, Dictionary<string, string>? data = null)
        {
            try
            {
                var message = new MulticastMessage()
                {
                    Tokens = deviceTokens,
                    Notification = new FcmNotification() // Alias kullanıyoruz
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data ?? new Dictionary<string, string>()
                };

                // Deprecated method yerine yeni method kullanıyoruz
                var response = await _messaging.SendEachForMulticastAsync(message);
                _logger.LogInformation($"FCM multicast sent. Success: {response.SuccessCount}, Failed: {response.FailureCount}");
                
                return response.SuccessCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send FCM multicast message");
                return false;
            }
        }

        public async Task<bool> RegisterDeviceToken(int userId, string token, string deviceType)
        {
            try
            {
                // Check if token already exists
                var existingTokens = await _deviceTokenRepository.GetAll();
                var existingToken = existingTokens.FirstOrDefault(t => t.Token == token);

                if (existingToken != null)
                {
                    // Update existing token
                    existingToken.LastUsed = DateTime.UtcNow;
                    existingToken.IsActive = true;
                    existingToken.UserId = userId;
                    await _deviceTokenRepository.Update(existingToken.Id, existingToken);
                }
                else
                {
                    // Create new token
                    var deviceToken = new DeviceToken
                    {
                        UserId = userId,
                        Token = token,
                        DeviceType = deviceType,
                        CreatedDate = DateTime.UtcNow,
                        LastUsed = DateTime.UtcNow,
                        IsActive = true
                    };
                    await _deviceTokenRepository.Create(deviceToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to register device token for user: {userId}");
                return false;
            }
        }

        public async Task<bool> UnregisterDeviceToken(string token)
        {
            try
            {
                var allTokens = await _deviceTokenRepository.GetAll();
                var deviceToken = allTokens.FirstOrDefault(t => t.Token == token);
                
                if (deviceToken != null)
                {
                    deviceToken.IsActive = false;
                    await _deviceTokenRepository.Update(deviceToken.Id, deviceToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to unregister device token: {token}");
                return false;
            }
        }

        private async Task<List<DeviceToken>> GetUserActiveTokens(int userId)
        {
            var allTokens = await _deviceTokenRepository.GetAll();
            return allTokens.Where(t => t.UserId == userId && t.IsActive).ToList();
        }
    }
}