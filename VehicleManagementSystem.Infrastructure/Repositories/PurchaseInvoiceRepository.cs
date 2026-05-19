using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.Infrastructure.Persistence;
using VehicleManagementSystem.Infrastructure.Repositories;

namespace VehicleManagementSystem.Infrastructure.Repositories
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseInvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseInvoice> CreateAsync(PurchaseInvoice invoice)
        {
            // PartName bata stock deduct garne
            var part = await _context.Parts
                .FirstOrDefaultAsync(p => p.PartName.ToLower() == invoice.PartName.ToLower());

            if (part != null)
            {
                part.Quantity -= invoice.QuantityPurchased;
            }

            _context.PurchaseInvoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<IEnumerable<PurchaseInvoice>> GetAllAsync()
        {
            return await _context.PurchaseInvoices
                .Include(p => p.Vendor)
                .OrderByDescending(p => p.PurchasedAt)
                .ToListAsync();
        }

        public async Task<PurchaseInvoice?> GetByIdAsync(int id)
        {
            return await _context.PurchaseInvoices
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}


