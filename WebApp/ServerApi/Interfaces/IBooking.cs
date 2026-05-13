using System.Collections.Generic;
using Core.Model;
namespace ServerApi.Interfaces
{
    public interface IBooking
    {
        void Booking(BookingData data);
        Task<List<BookingData>> GetRequests(User user);
        Task<List<BookingData>> GetStudioRequests(User user);
    }
}
