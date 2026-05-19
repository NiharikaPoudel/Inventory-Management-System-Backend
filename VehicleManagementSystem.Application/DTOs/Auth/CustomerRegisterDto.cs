using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.DTOs.Auth
{
    public class CustomerRegisterDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}