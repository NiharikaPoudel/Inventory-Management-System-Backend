using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Autolaya.Application.DTOs.Buyer;
using Autolaya.Application.Interfaces.IServices;

namespace Autolaya.Controllers;

[ApiController]
[Route("api/buyer")]
//[Authorize]
public class BuyerController : ControllerBase
{
    private readonly IBuyerService _service;

    public BuyerController(IBuyerService service) => _service = service;

    private string GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard() =>
        Ok(await _service.GetDashboardSummaryAsync(GetUserId()));

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _service.GetProfileAsync(GetUserId());
        return profile == null ? NotFound() : Ok(profile);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateCustomerDto dto)
    {
        var updated = await _service.UpdateProfileAsync(GetUserId(), dto);
        return updated ? Ok(new { message = "Profile updated." }) : NotFound();
    }

    [HttpGet("garage")]
    public async Task<IActionResult> GetGarage() =>
        Ok(await _service.GetGarageAsync(GetUserId()));

    [HttpGet("appointments")]
    public async Task<IActionResult> GetAppointments() =>
        Ok(await _service.GetAppointmentsAsync(GetUserId()));

    [HttpPost("appointments")]
    public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDto dto)
    {
        var (success, message) = await _service.BookAppointmentAsync(GetUserId(), dto);
        return success ? Ok(new { message }) : BadRequest(new { message });
    }

    [HttpGet("service-history")]
    public async Task<IActionResult> GetServiceHistory() =>
        Ok(await _service.GetServiceHistoryAsync(GetUserId()));

    [HttpPost("reviews")]
    public async Task<IActionResult> SubmitReview([FromBody] CreateReviewDto dto)
    {
        var (success, message) = await _service.SubmitReviewAsync(GetUserId(), dto);
        return success ? Ok(new { message }) : BadRequest(new { message });
    }

    [HttpGet("reviews/pending")]
    public async Task<IActionResult> GetPendingReviews() =>
        Ok(await _service.GetPendingReviewsAsync(GetUserId()));

    [HttpGet("reviews/featured")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFeaturedReviews() =>
        Ok(await _service.GetFeaturedReviewsAsync());

    [HttpPost("parts")]
    public async Task<IActionResult> SubmitPartRequest([FromBody] CreatePartRequestDto dto)
    {
        var (success, message) = await _service.SubmitPartRequestAsync(GetUserId(), dto);
        return success ? Ok(new { message }) : BadRequest(new { message });
    }

    [HttpGet("parts")]
    public async Task<IActionResult> GetPartRequests() =>
        Ok(await _service.GetPartRequestsAsync(GetUserId()));
}