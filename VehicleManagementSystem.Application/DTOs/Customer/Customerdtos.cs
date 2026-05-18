using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.DTOs.Customer
{
    // ─── Feature 12: Self-Registration ───────────────────────────────────
    public class RegisterCustomerDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }

    public class UpdateProfileDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }

    // ─── Shared: Simple Profile Response (No vehicles) ───────────────────
    public class CustomerProfileDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
    }

    // ─── NEW Features: Request Form Submissions ──────────────────────────
    public class CreateBookingDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string ServiceType { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string BookingDate { get; set; } = string.Empty;
        [Required]
        public string TimeSlot { get; set; } = string.Empty;
    }

    public class CreatePartRequestDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string PartDescription { get; set; } = string.Empty;
    }

    public class SubmitReviewDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        [Range(1.0, 5.0, ErrorMessage = "Rating must be between 1 and 5 stars.")]
        public decimal StarRating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }

    // Response structure for your Kisaan Bazaar dynamic homepage layout
    public class FeaturedVendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}