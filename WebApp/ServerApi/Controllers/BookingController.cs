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
    public void Booking(BookingData data)
    {
        bookingRepo.Booking(data);
    }
}

