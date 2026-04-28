using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.DTOs.Sale;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;

        public SaleService(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto)
        {
            decimal discount = 0;
            string message = "No discount applied";

            // 🔥 Loyalty Logic
            if (dto.TotalAmount > 5000)
            {
                discount = dto.TotalAmount * 0.10m;
                message = "10% loyalty discount applied";
            }

            decimal finalAmount = dto.TotalAmount - discount;

            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                DiscountAmount = discount,
                FinalAmount = finalAmount
            };

            var created = await _repository.AddSaleAsync(sale);

            return new SaleResponseDto
            {
                Id = created.Id,
                CustomerId = created.CustomerId,
                TotalAmount = created.TotalAmount,
                DiscountAmount = created.DiscountAmount,
                FinalAmount = created.FinalAmount,
                SaleDate = created.SaleDate,
                Message = message
            };
        }

        public async Task<List<SaleResponseDto>> GetAllSalesAsync()
        {
            var sales = await _repository.GetAllSalesAsync();

            return sales.Select(s => new SaleResponseDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                TotalAmount = s.TotalAmount,
                DiscountAmount = s.DiscountAmount,
                FinalAmount = s.FinalAmount,
                SaleDate = s.SaleDate,
                Message = s.DiscountAmount > 0 ? "Discount Applied" : "No Discount"
            }).ToList();
        }

        public async Task<SaleResponseDto?> GetSaleByIdAsync(int id)
        {
            var s = await _repository.GetSaleByIdAsync(id);

            if (s == null) return null;

            return new SaleResponseDto
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                TotalAmount = s.TotalAmount,
                DiscountAmount = s.DiscountAmount,
                FinalAmount = s.FinalAmount,
                SaleDate = s.SaleDate,
                Message = s.DiscountAmount > 0 ? "Discount Applied" : "No Discount"
            };
        }
    }
}