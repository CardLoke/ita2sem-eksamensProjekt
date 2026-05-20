using Core.Model;

namespace ServerApi.Services
{
    public interface IEmailService
    {
        Task SendBookingNotificationAsync(BookingData booking, string studioOwnerEmail);
    }
}
