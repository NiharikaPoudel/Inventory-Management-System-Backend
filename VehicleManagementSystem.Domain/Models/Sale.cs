using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagementSystem.Domain.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal RemainingAmount { get; set; }

        [Column("DueDate")]
        public DateTime? CreditDueDate { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public bool ReminderSent { get; set; } = false;

        public DateTime? ReminderSentAt { get; set; }
    }
}