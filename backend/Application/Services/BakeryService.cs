using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Repositories;

namespace Pastella.Backend.Application.Services
{
    public class BakeryService : IBakeryService
    {
        private readonly IRepository<Bakery> _bakeryRepository;

        public BakeryService(IRepository<Bakery> bakeryRepository)
        {
            _bakeryRepository = bakeryRepository;
        }

        public async Task<BakeryDto?> CreateBakery(CreateBakeryDto createBakeryDto)
        {
            var bakery = new Bakery
            {
                Name = createBakeryDto.Name,
                Description = createBakeryDto.Description,
                Address = createBakeryDto.Address,
                Phone = createBakeryDto.Phone,
                Email = createBakeryDto.Email,
                IsApproved = false, // New bakeries need approval
                CreatedDate = DateTime.UtcNow,
                SocialMediaLinks = createBakeryDto.SocialMediaLinks ?? new List<string>()
            };

            await _bakeryRepository.Create(bakery);

            return new BakeryDto
            {
                Id = bakery.Id,
                Name = bakery.Name,
                Description = bakery.Description,
                Address = bakery.Address,
                Phone = bakery.Phone,
                Email = bakery.Email,
                IsApproved = bakery.IsApproved,
                SocialMediaLinks = bakery.SocialMediaLinks
            };
        }

        public async Task<IEnumerable<BakeryDto>> GetAllBakeries()
        {
            var bakeries = await _bakeryRepository.GetAll();
            return bakeries.Select(b => new BakeryDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Address = b.Address,
                Phone = b.Phone,
                Email = b.Email,
                IsApproved = b.IsApproved,
                SocialMediaLinks = b.SocialMediaLinks
            });
        }

        public async Task<BakeryDto?> GetBakeryById(int id)
        {
            var bakery = await _bakeryRepository.GetById(id);
            if (bakery == null) return null;

            return new BakeryDto
            {
                Id = bakery.Id,
                Name = bakery.Name,
                Description = bakery.Description,
                Address = bakery.Address,
                Phone = bakery.Phone,
                Email = bakery.Email,
                IsApproved = bakery.IsApproved,
                SocialMediaLinks = bakery.SocialMediaLinks
            };
        }

        public async Task<IEnumerable<OrderDto>> GetBakeryOrders(int bakeryId)
        {
            var bakeryRepo = _bakeryRepository as BakeryRepository;
            if (bakeryRepo == null) return new List<OrderDto>();

            var orders = await bakeryRepo.GetBakeryOrders(bakeryId);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CakeId = o.CakeId,
                TotalPrice = o.TotalPrice,
                OrderDate = o.OrderDate,
                Status = o.Status
            });
        }

        public async Task<bool> ApproveBakery(int id, bool approve)
        {
            var bakery = await _bakeryRepository.GetById(id);
            if (bakery == null) return false;

            bakery.IsApproved = approve;
            await _bakeryRepository.Update(id, bakery);
            return true;
        }

        public async Task<IEnumerable<BakeryDto>> GetPendingBakeries()
        {
            var bakeryRepo = _bakeryRepository as BakeryRepository;
            if (bakeryRepo == null) return new List<BakeryDto>();

            var pendingBakeries = await bakeryRepo.GetPendingBakeries();
            return pendingBakeries.Select(b => new BakeryDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Address = b.Address,
                Phone = b.Phone,
                Email = b.Email,
                IsApproved = b.IsApproved,
                SocialMediaLinks = b.SocialMediaLinks
            });
        }
    }
}