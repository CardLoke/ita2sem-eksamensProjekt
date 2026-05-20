using Core.Model;

namespace ServerApi.Services
{
    public interface IEmailService
    {
        Task SendBookingNotificationAsync(BookingData booking, string studioOwnerEmail);
        Task SendStatusNotificationAsync(BookingData booking, string studioOwnerEmail);
    }
}
