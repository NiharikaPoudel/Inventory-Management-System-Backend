using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.DTOs.CreditReminder;
using VehicleManagementSystem.Infrastructure.Persistence;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class CreditReminderService : ICreditReminderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public CreditReminderService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<List<UnpaidCreditDto>> GetUnpaidCreditsAsync()
        {
            var today = DateTime.UtcNow.Date;

            var sales = await _context.Sales
                .Include(s => s.Customer)
                .Where(s =>
                    (s.RemainingAmount > 0 || (s.RemainingAmount == 0 && s.FinalAmount > s.PaidAmount)))
                //.OrderBy(s => s.CreditDueDate)
                .ToListAsync();

            return sales.Select(s => new UnpaidCreditDto
            {
                SaleId = s.Id,
                CustomerId = s.CustomerId,

                CustomerName = s.Customer != null ? s.Customer.FullName : string.Empty,
                CustomerEmail = s.Customer != null ? s.Customer.Email : string.Empty,
                CustomerPhone = s.Customer != null ? s.Customer.Phone : string.Empty,

                TotalAmount = s.TotalAmount,
                PaidAmount = s.PaidAmount,
                RemainingAmount = s.RemainingAmount,

                SaleDate = s.SaleDate,
                CreditDueDate = s.CreditDueDate,

                ReminderSent = s.ReminderSent,
                ReminderSentAt = s.ReminderSentAt
            }).ToList();
        }

        public async Task<bool> SendReminderAsync(int saleId)
        {
            var sale = await _context.Sales
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.Id == saleId);

            if (sale == null)
                return false;

            if (sale.Customer == null || string.IsNullOrWhiteSpace(sale.Customer.Email))
                return false;

            if (sale.RemainingAmount <= 0)
                return false;

            var subject = "Payment Reminder - Unpaid Credit";

            var body = $@"
                <h2>Payment Reminder</h2>

                <p>Dear {sale.Customer.FullName},</p>

                <p>This is a reminder that you have an unpaid credit amount in Vehicle Parts Management System.</p>

                <table border='1' cellpadding='8' cellspacing='0'>
                    <tr>
                        <td><b>Invoice/Sale ID</b></td>
                        <td>{sale.Id}</td>
                    </tr>
                    <tr>
                        <td><b>Total Amount</b></td>
                        <td>NPR {sale.TotalAmount}</td>
                    </tr>
                    <tr>
                        <td><b>Paid Amount</b></td>
                        <td>NPR {sale.PaidAmount}</td>
                    </tr>
                    <tr>
                        <td><b>Remaining Amount</b></td>
                        <td>NPR {sale.RemainingAmount}</td>
                    </tr>
                    <tr>
                        <td><b>Credit Due Date</b></td>
                        <td>{sale.CreditDueDate?.ToString("yyyy-MM-dd")}</td>
                    </tr>
                </table>

                <p>Please clear your remaining payment as soon as possible.</p>

                <p>Thank you,<br/>
                Vehicle Parts Management System</p>
            ";

            await _emailService.SendEmailAsync(sale.Customer.Email, subject, body);

            sale.ReminderSent = true;
            sale.ReminderSentAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
