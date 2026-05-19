namespace VehicleManagementSystem.DTOs.Notification
{
    public class LowStockNotificationDto
    {
        public int PartId { get; set; }

        public string PartName { get; set; } = string.Empty;

        public string PartNumber { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
