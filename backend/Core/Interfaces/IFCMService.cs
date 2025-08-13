using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IFCMService
    {
        Task<bool> SendToDevice(string deviceToken, string title, string body, Dictionary<string, string>? data = null);
        Task<bool> SendToUser(int userId, string title, string body, Dictionary<string, string>? data = null);
        Task<bool> SendToMultipleDevices(List<string> deviceTokens, string title, string body, Dictionary<string, string>? data = null);
        Task<bool> RegisterDeviceToken(int userId, string token, string deviceType);
        Task<bool> UnregisterDeviceToken(string token);
    }
}