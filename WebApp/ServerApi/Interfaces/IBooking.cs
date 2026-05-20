using System.Collections.Generic;
using Core.Model;
namespace ServerApi.Interfaces
{
    public interface IBooking
    {
        void Booking(BookingData data);
        List<BookingData> GetBookings();
        Task<List<BookingData>> GetRequests(User user);
        Task<List<BookingData>> GetStudioRequests(User user);
        Task<List<BookingData>> GetByStudioId(int studioId);
        void Status(int id, string status);
        void Delete(int id);
    }
}
