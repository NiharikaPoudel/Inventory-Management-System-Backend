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

            var status = string.IsNullOrWhiteSpace(dto.Status) ? "paid" : dto.Status;
            var paidAmount = status.Equals("paid", StringComparison.OrdinalIgnoreCase)
                ? finalAmount
                : 0m;
            var remainingAmount = status.Equals("paid", StringComparison.OrdinalIgnoreCase)
                ? 0m
                : finalAmount;

            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                DiscountAmount = discount,
                FinalAmount = finalAmount,
                PaidAmount = paidAmount,
                RemainingAmount = remainingAmount,
                CreditDueDate = DateTime.UtcNow.Date,
                Status = status
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
                Status = created.Status,
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
                CustomerName = s.Customer?.FullName ?? string.Empty,
                TotalAmount = s.TotalAmount,
                DiscountAmount = s.DiscountAmount,
                FinalAmount = s.FinalAmount,
                SaleDate = s.SaleDate,
                Status = s.Status,
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
                CustomerName = s.Customer?.FullName ?? string.Empty,
                TotalAmount = s.TotalAmount,
                DiscountAmount = s.DiscountAmount,
                FinalAmount = s.FinalAmount,
                SaleDate = s.SaleDate,
                Status = s.Status,
                Message = s.DiscountAmount > 0 ? "Discount Applied" : "No Discount"
            };
        }

        public async Task<List<CustomerReportDto>> GetRegularCustomersAsync()
        {
            return await _repository.GetRegularCustomersAsync();
        }

        public async Task<List<CustomerReportDto>> GetHighSpendersAsync()
        {
            return await _repository.GetHighSpendersAsync();
        }

        public async Task<List<CustomerReportDto>> GetPendingCreditCustomersAsync()
        {
            return await _repository.GetPendingCreditCustomersAsync();
        }

        private static IEnumerable<CustomerReportDto> BuildCustomerReports(IEnumerable<Sale> sales)
        {
            return sales
                .Where(s => s.Customer != null)
                .GroupBy(s => s.CustomerId)
                .Select(group =>
                {
                    var sample = group.First();
                    var pendingCredit = group.Sum(s =>
                        s.RemainingAmount > 0
                            ? s.RemainingAmount
                            : Math.Max(s.FinalAmount - s.PaidAmount, 0));

                    return new CustomerReportDto
                    {
                        CustomerId = group.Key,
                        FullName = sample.Customer?.FullName ?? string.Empty,
                        Phone = sample.Customer?.Phone ?? string.Empty,
                        Email = sample.Customer?.Email ?? string.Empty,
                        TotalVisits = group.Count(),
                        TotalSpent = group.Sum(s => s.FinalAmount),
                        PendingCreditAmount = pendingCredit
                    };
                });
        }
    }
}