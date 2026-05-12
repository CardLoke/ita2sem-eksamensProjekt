using System.Collections.Generic;
using Core.Model;
namespace ServerApi.Interfaces
{
    public interface IBooking
    {
        public void Booking(BookingData data);
    }
}
