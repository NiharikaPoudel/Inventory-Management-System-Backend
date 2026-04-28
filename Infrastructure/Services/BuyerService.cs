using Autolaya.Application.DTOs.Buyer;
using Autolaya.Application.Interfaces.IRepositories;
using Autolaya.Application.Interfaces.IServices;
using Autolaya.Domain.Models;

namespace Autolaya.Infrastructure.Services;

public class BuyerService : IBuyerService
{
    private readonly ICustomerRepository _repo;
    public BuyerService(ICustomerRepository repo) => _repo = repo;

    public async Task<CustomerDto?> GetProfileAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return null;
        return new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address
        };
    }

    public async Task<bool> UpdateProfileAsync(string userId, UpdateCustomerDto dto)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return false;
        c.FirstName = dto.FirstName; c.LastName = dto.LastName;
        c.PhoneNumber = dto.PhoneNumber; c.Address = dto.Address;
        await _repo.UpdateAsync(c);
        return true;
    }

    public async Task<List<VehicleDto>> GetGarageAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return new List<VehicleDto>();
        var vehicles = await _repo.GetVehiclesByCustomerIdAsync(c.Id);
        return vehicles.Select(v => new VehicleDto
        {
            Id = v.Id,
            VehicleCode = v.VehicleCode,
            Make = v.Make,
            Model = v.Model,
            Year = v.Year,
            LicensePlate = v.LicensePlate,
            Color = v.Color
        }).ToList();
    }

    public async Task<List<AppointmentDto>> GetAppointmentsAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return new List<AppointmentDto>();
        var list = await _repo.GetAppointmentsByCustomerIdAsync(c.Id);
        return list.Select(a => new AppointmentDto
        {
            Id = a.Id,
            AppointmentCode = $"#{a.Id:D4}",
            VehicleName = $"{a.Vehicle.Make} {a.Vehicle.Model}",
            ServiceType = a.ServiceType,
            AppointmentDate = a.AppointmentDate,
            TimeSlot = a.TimeSlot,
            Status = a.Status
        }).ToList();
    }

    public async Task<(bool Success, string Message)> BookAppointmentAsync(string userId, CreateAppointmentDto dto)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return (false, "Customer not found.");
        int count = await _repo.GetAppointmentCountByDateAsync(dto.AppointmentDate);
        if (count >= 4) return (false, $"No slots available on {dto.AppointmentDate:yyyy-MM-dd}.");
        var appointment = new Appointment
        {
            CustomerId = c.Id,
            VehicleId = dto.VehicleId,
            ServiceType = dto.ServiceType,
            Description = dto.Description,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot,
            Status = "Pending"
        };
        await _repo.AddAppointmentAsync(appointment);
        return (true, "Appointment booked successfully.");
    }

    public async Task<List<ServiceHistoryDto>> GetServiceHistoryAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return new List<ServiceHistoryDto>();
        var list = await _repo.GetServiceHistoryByCustomerIdAsync(c.Id);
        return list.Select(s => new ServiceHistoryDto
        {
            Id = s.Id,
            ServiceDate = s.ServiceDate,
            VehicleName = $"{s.Vehicle.Make} {s.Vehicle.Model}",
            ServiceType = s.ServiceType,
            TotalCost = s.TotalCost,
            ReviewPending = s.ReviewPending
        }).ToList();
    }

    public async Task<(bool Success, string Message)> SubmitReviewAsync(string userId, CreateReviewDto dto)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return (false, "Customer not found.");
        var s = await _repo.GetServiceHistoryByIdAsync(dto.ServiceHistoryId);
        if (s == null || s.CustomerId != c.Id) return (false, "Service not found.");
        var review = new Review
        {
            CustomerId = c.Id,
            ServiceHistoryId = dto.ServiceHistoryId,
            StarRating = dto.StarRating,
            Comment = dto.Comment,
            IsFeatured = dto.StarRating >= 4.5m
        };
        s.ReviewPending = false;
        await _repo.AddReviewAsync(review);
        return (true, "Review submitted.");
    }

    public async Task<List<ReviewDto>> GetPendingReviewsAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return new List<ReviewDto>();
        var list = await _repo.GetServiceHistoryByCustomerIdAsync(c.Id);
        return list.Where(s => s.ReviewPending).Select(s => new ReviewDto
        {
            Id = s.Id,
            VehicleName = $"{s.Vehicle.Make} {s.Vehicle.Model}",
            ServiceType = s.ServiceType,
            Comment = s.ServiceDate.ToString("yyyy-MM-dd")
        }).ToList();
    }

    public async Task<List<ReviewDto>> GetFeaturedReviewsAsync()
    {
        var list = await _repo.GetFeaturedReviewsAsync();
        return list.Select(r => new ReviewDto
        {
            Id = r.Id,
            CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
            VehicleName = $"{r.ServiceHistory.Vehicle.Make} {r.ServiceHistory.Vehicle.Model}",
            ServiceType = r.ServiceHistory.ServiceType,
            StarRating = r.StarRating,
            Comment = r.Comment
        }).ToList();
    }

    public async Task<(bool Success, string Message)> SubmitPartRequestAsync(string userId, CreatePartRequestDto dto)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return (false, "Customer not found.");
        await _repo.AddPartRequestAsync(new PartRequest
        {
            CustomerId = c.Id,
            VehicleId = dto.VehicleId,
            PartDescription = dto.PartDescription
        });
        return (true, "Part request submitted.");
    }

    public async Task<List<PartRequestDto>> GetPartRequestsAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return new List<PartRequestDto>();
        var list = await _repo.GetPartRequestsByCustomerIdAsync(c.Id);
        return list.Select(p => new PartRequestDto
        {
            Id = p.Id,
            VehicleName = $"{p.Vehicle.Make} {p.Vehicle.Model}",
            PartDescription = p.PartDescription,
            Status = p.Status,
            RequestedAt = p.RequestedAt
        }).ToList();
    }

    public async Task<object> GetDashboardSummaryAsync(string userId)
    {
        var c = await _repo.GetByUserIdAsync(userId);
        if (c == null) return new { };
        var vehicles = await _repo.GetVehiclesByCustomerIdAsync(c.Id);
        var appointments = await _repo.GetAppointmentsByCustomerIdAsync(c.Id);
        var history = await _repo.GetServiceHistoryByCustomerIdAsync(c.Id);
        return new
        {
            FirstName = c.FirstName,
            RegisteredVehicles = vehicles.Count,
            PendingAppointments = appointments.Count(a => a.Status == "Pending"),
            LastServiceDate = history.OrderByDescending(s => s.ServiceDate)
                                         .FirstOrDefault()?.ServiceDate.ToString("yyyy-MM-dd") ?? "N/A",
            ActivityStream = appointments.Take(3).Select(a => new
            {
                Description = $"{a.ServiceType} booking — {a.Status}",
                Date = a.AppointmentDate.ToString("yyyy-MM-dd"),
                TimeSlot = a.TimeSlot,
                VehicleName = $"{a.Vehicle.Make} {a.Vehicle.Model}"
            })
        };
    }
}