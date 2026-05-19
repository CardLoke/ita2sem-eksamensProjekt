using Core.Model;
using ServerApi.Interfaces;
using ServerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;



namespace ServerApi.Controllers;

[ApiController]
[Route("api/booking")]

public class BookingController : ControllerBase
    {
    private readonly IBooking bookingRepo;

    public BookingController(IBooking _repo)
    {
        bookingRepo = _repo;
    }
    [HttpPost]
    public string Booking(BookingData data)
    {
        (bool con, string error) = validateBooking(data);
        if (con)
        {
            return error;
        }
        bookingRepo.Booking(data);
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

