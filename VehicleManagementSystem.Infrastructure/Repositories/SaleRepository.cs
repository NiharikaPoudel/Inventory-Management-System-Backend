using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.DTOs.Sale;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.Infrastructure.Persistence;

namespace VehicleManagementSystem.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> AddSaleAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .ToListAsync();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<CustomerReportDto>> GetRegularCustomersAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .GroupBy(s => s.Customer)
                .Where(g => g.Count() >= 2)
                .Select(g => new CustomerReportDto
                {
                    CustomerId = g.Key!.Id,
                    FullName = g.Key.FullName,
                    Phone = g.Key.Phone,
                    Email = g.Key.Email,
                    TotalVisits = g.Count(),
                    TotalSpent = g.Sum(s => s.FinalAmount),
                    PendingCreditAmount = g
                        .Where(s => s.Status == "credit")
                        .Sum(s => s.RemainingAmount > 0
                            ? s.RemainingAmount
                            : Math.Max(s.FinalAmount - s.PaidAmount, 0))
                })
                .OrderByDescending(r => r.TotalVisits)
                .ToListAsync();
        }

        public async Task<List<CustomerReportDto>> GetHighSpendersAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .GroupBy(s => s.Customer)
                .Select(g => new CustomerReportDto
                {
                    CustomerId = g.Key!.Id,
                    FullName = g.Key.FullName,
                    Phone = g.Key.Phone,
                    Email = g.Key.Email,
                    TotalVisits = g.Count(),
                    TotalSpent = g.Sum(s => s.FinalAmount),
                    PendingCreditAmount = g
                        .Where(s => s.Status == "credit")
                        .Sum(s => s.RemainingAmount > 0
                            ? s.RemainingAmount
                            : Math.Max(s.FinalAmount - s.PaidAmount, 0))
                })
                .Where(r => r.TotalSpent > 5000)
                .OrderByDescending(r => r.TotalSpent)
                .ToListAsync();
        }

        public async Task<List<CustomerReportDto>> GetPendingCreditCustomersAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .Where(s => s.Status == "credit")
                .GroupBy(s => s.Customer)
                .Select(g => new CustomerReportDto
                {
                    CustomerId = g.Key!.Id,
                    FullName = g.Key.FullName,
                    Phone = g.Key.Phone,
                    Email = g.Key.Email,
                    TotalVisits = g.Count(),
                    TotalSpent = g.Sum(s => s.FinalAmount),
                    PendingCreditAmount = g.Sum(s => s.RemainingAmount > 0
                        ? s.RemainingAmount
                        : Math.Max(s.FinalAmount - s.PaidAmount, 0))
                })
                .OrderByDescending(r => r.PendingCreditAmount)
                .ToListAsync();
        }
    }
}