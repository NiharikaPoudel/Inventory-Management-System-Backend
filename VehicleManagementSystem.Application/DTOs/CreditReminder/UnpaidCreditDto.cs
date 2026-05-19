namespace VehicleManagementSystem.DTOs.CreditReminder
{
    public class UnpaidCreditDto
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }

        public DateTime SaleDate { get; set; }
        public DateTime? CreditDueDate { get; set; }

        public bool ReminderSent { get; set; }
        public DateTime? ReminderSentAt { get; set; }
    }
}
