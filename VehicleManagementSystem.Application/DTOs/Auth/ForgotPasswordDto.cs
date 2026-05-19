using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.DTOs.Auth
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
