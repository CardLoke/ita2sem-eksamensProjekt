using Core.Model;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ServerApi.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendBookingNotificationAsync(BookingData booking, string studioOwnerEmail)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(studioOwnerEmail));
        email.Subject = $"New Booking Request - {booking.StudioName ?? "Studio"}";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <h2 style='color:#00ffcc;'>New Booking Request</h2>
                <p><strong>From:</strong> {booking.Name}</p>
                <p><strong>Studio:</strong> {booking.StudioName}</p>
                <p><strong>Date:</strong> {booking.Date.ToShortDateString()}</p>
                <p><strong>Time:</strong> {booking.StartTime} - {booking.EndTime}</p>
                <p><strong>Attendees:</strong> {booking.Attendees}</p>
                {(!string.IsNullOrEmpty(booking.Notes) ? $"<p><strong>Notes:</strong> {booking.Notes}</p>" : "")}
                <hr>
                <p>Go to your <a href='http://localhost:yourblazorport/inbox'>Inbox</a> to Accept or Reject.</p>"
        };

        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
    public async Task SendStatusNotificationAsync(BookingData booking, string bookingOwnerEmail)
    {
        string color = "";
        if (booking.Status == "Rejected") color = "#ff0000";
        if (booking.Status == "Accepted") color = "#00ff00";
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(bookingOwnerEmail));
        email.Subject = $"Your Booking Request For - {booking.StudioName} - Has Been {booking.Status}";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <h2 style='color:{color};'>Your Booking Request For - {booking.StudioName} - Has Been {booking.Status}
                <p><strong>From:</strong> {booking.Name}</p>
                <p><strong>Studio:</strong> {booking.StudioName}</p>
                <p><strong>Date:</strong> {booking.Date.ToShortDateString()}</p>
                <p><strong>Time:</strong> {booking.StartTime} - {booking.EndTime}</p>
                <p><strong>Attendees:</strong> {booking.Attendees}</p>
                {(!string.IsNullOrEmpty(booking.Notes) ? $"<p><strong>Notes:</strong> {booking.Notes}</p>" : "")}
            "
        };
        email.Body = bodyBuilder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
