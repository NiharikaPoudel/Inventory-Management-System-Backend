using Microsoft.EntityFrameworkCore;
using Autolaya.Application.Interfaces.IRepositories;
using Autolaya.Domain.Models;
using Autolaya.Infrastructure.Data;

namespace Autolaya.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _db;
    public CustomerRepository(AppDbContext db) => _db = db;

    public async Task<Customer?> GetByUserIdAsync(string userId) =>
        await _db.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<Customer?> GetByIdAsync(int id) =>
        await _db.Customers.FindAsync(id);

    public async Task UpdateAsync(Customer customer)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId) =>
        await _db.Vehicles.Where(v => v.CustomerId == customerId).ToListAsync();

    public async Task<List<Appointment>> GetAppointmentsByCustomerIdAsync(int customerId) =>
        await _db.Appointments
            .Include(a => a.Vehicle)
            .Where(a => a.CustomerId == customerId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<List<ServiceHistory>> GetServiceHistoryByCustomerIdAsync(int customerId) =>
        await _db.ServiceHistories
            .Include(s => s.Vehicle)
            .Where(s => s.CustomerId == customerId)
            .OrderByDescending(s => s.ServiceDate)
            .ToListAsync();

    public async Task<List<Review>> GetFeaturedReviewsAsync() =>
        await _db.Reviews
            .Include(r => r.Customer)
            .Include(r => r.ServiceHistory).ThenInclude(s => s.Vehicle)
            .Where(r => r.StarRating >= 4.5m)
            .OrderByDescending(r => r.StarRating)
            .Take(10)
            .ToListAsync();

    public async Task<int> GetAppointmentCountByDateAsync(DateTime date)
    {
        var dateOnly = date.Date;
        return await _db.Appointments
            .Where(a => a.AppointmentDate.Date == dateOnly && a.Status != "Cancelled")
            .CountAsync();
    }

    public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
    {
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync();
        return appointment;
    }

    public async Task<PartRequest> AddPartRequestAsync(PartRequest partRequest)
    {
        _db.PartRequests.Add(partRequest);
        await _db.SaveChangesAsync();
        return partRequest;
    }

    public async Task<List<PartRequest>> GetPartRequestsByCustomerIdAsync(int customerId) =>
        await _db.PartRequests
            .Include(p => p.Vehicle)
            .Where(p => p.CustomerId == customerId)
            .OrderByDescending(p => p.RequestedAt)
            .ToListAsync();

    public async Task<Review> AddReviewAsync(Review review)
    {
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();
        return review;
    }

    public async Task<ServiceHistory?> GetServiceHistoryByIdAsync(int id) =>
        await _db.ServiceHistories
            .Include(s => s.Vehicle)
            .FirstOrDefaultAsync(s => s.Id == id);
}