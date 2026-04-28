using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IRepositories;
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
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }
    }
}