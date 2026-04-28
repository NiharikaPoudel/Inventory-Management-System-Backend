using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
    }
}