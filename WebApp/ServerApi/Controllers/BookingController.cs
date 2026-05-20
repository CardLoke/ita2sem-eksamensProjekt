using Core.Model;
using ServerApi.Interfaces;
using ServerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;
using ServerApi.Services;



namespace ServerApi.Controllers;

[ApiController]
[Route("api/booking")]

public class BookingController : ControllerBase
    {
    private readonly IBooking bookingRepo;
    private readonly IEmailService emailService;

    public BookingController(IBooking _repo, IEmailService _emailService)
    {
        bookingRepo = _repo;
        emailService = _emailService;
    }
    [HttpPost]
    public async Task<string> Booking(BookingData data)
    {
        (bool con, string error) = validateBooking(data);
        if (con)
        {
            return error;
        }
        bookingRepo.Booking(data);
        var ownerEmail = await bookingRepo.GetStudioOwnerEmail(data.StudioId);

        if (!string.IsNullOrEmpty(ownerEmail))
        {
            await emailService.SendBookingNotificationAsync(data, ownerEmail);
        }
        return "ok";
    }
    [HttpPost]
    [Route("requests")]
    public async Task<List<BookingData>> Requests(User user) 
    {
        return await bookingRepo.GetRequests(user);
        
    }
    [HttpPost]
    [Route("studioRequests")]
    public async Task<List<BookingData>> StudioRequests(User user)
    {
        return await bookingRepo.GetStudioRequests(user);
        
    }
    [HttpPut]
    [Route("status/{id:int}")]
    public void Status(int id, [FromBody] string status)
    {
        Console.WriteLine("Controller");
        bookingRepo.Status(id, status);
    }
    [HttpDelete]
    [Route("delete/{id:int}")]
    
    public void Delete(int id)
    {
        Console.WriteLine("hej");
        bookingRepo.Delete(id);
    }
    [HttpGet]
    [Route("studio/{id}")]
    public async Task<List<BookingData>> GetStudioBookings(int id)
    {
        return await bookingRepo.GetByStudioId(id);
    }
    private (bool, string) validateBooking(BookingData data) 
    {
        
        DateTime newStartTime = DateTime.Parse(data.StartTime);
        DateTime newEndTime = DateTime.Parse(data.EndTime);
        // Check if the start time is before the end time
        if (newStartTime >= newEndTime)
        {
            return (false, "start time is before the end time");
        }
        List<BookingData> bookings = bookingRepo.GetBookings();
        bool res = bookings.Any(booking =>
                                     data.Date.Year * 1000 + data.Date.DayOfYear == booking.Date.Year * 1000 + booking.Date.DayOfYear &&
                                     newStartTime < DateTime.Parse(booking.EndTime) &&
                                     newEndTime > DateTime.Parse(booking.StartTime));
        return (res, "The studio is already booked for the selected time slot.");
    }
}

