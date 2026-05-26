using ServerApi.Interfaces;
using Core.Model;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ServerApi.Repositories
{
    public class BookingRepositoryMongoDB : IBooking
    {
        private readonly IMongoCollection<BookingData>? _bookings;
        private readonly IMongoCollection<Studio>? _studios; // Add this line
        private readonly IMongoCollection<User>? _users;     // Add this line

        public BookingRepositoryMongoDB()
        {
            var client = new MongoClient("mongodb+srv://kris600m:eyh94zkh@cluster0.xpou06p.mongodb.net/");
            var database = client.GetDatabase("SessionSync");
            _bookings = database.GetCollection<BookingData>("Booking");
            _studios = database.GetCollection<Studio>("Studio"); // Add this line
            _users = database.GetCollection<User>("Users");       // Add this line
        }
        public void Booking(BookingData data)
        {
            _bookings.InsertOne(data);
        }
        public async Task<List<BookingData>> GetRequests(User user)
        {
            return await _bookings.Find(item => item.Name == user.Username).ToListAsync();
        }
        public async Task<List<BookingData>> GetStudioRequests(User user)
        {
            return await _bookings.Find(item => item.StudioOwner == user.Username).ToListAsync();
        }
        public async Task<List<BookingData>> GetByStudioId(int studioId) 
        {
            return await _bookings.Find(item => item.StudioId == studioId).ToListAsync();
        }
        public void Status(int id, string status)
        {
            Console.WriteLine("Repo");
            var filter = Builders<BookingData>.Filter.Eq(item => item.Id, id);
            var update = Builders<BookingData>.Update.Set(item => item.Status, status);
            _bookings.UpdateOne(filter, update);
        }
        public void Delete(int id)
        {
            _bookings.DeleteOne(item => item.Id == id);
        }

        public List<BookingData> GetBookings()
        {
            return _bookings.Find(item => true).ToList();
        }
        public async Task<string> GetStudioOwnerEmailStatus(int bookingId) {

            var bookingFilter = Builders<BookingData>.Filter.Eq(s => s.Id, bookingId);
            var booking = await _bookings.Find(bookingFilter).FirstOrDefaultAsync();

            var userFilter = Builders<User>.Filter.Eq(u => u.Username, booking.Name);
            var user = await _users.Find(userFilter).FirstOrDefaultAsync();

            return user?.Mail;
        }
        public async Task<BookingData> GetStatusData(int id)
        {
            var bookingFilter = Builders<BookingData>.Filter.Eq(s => s.Id, id);
            var bookingData = await _bookings.Find(bookingFilter).FirstOrDefaultAsync();
            return bookingData;
        }
        
        public async Task<string?> GetStudioOwnerEmail(int studioId)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Looking for studio with ID: {studioId}");

                // Step 1: Find the studio
                var studioFilter = Builders<Studio>.Filter.Eq(s => s.Id, studioId);
                var studio = await _studios.Find(studioFilter).FirstOrDefaultAsync();

                if (studio == null)
                {
                    Console.WriteLine($"[DEBUG] Studio with ID {studioId} not found");
                    return null;
                }

                Console.WriteLine($"[DEBUG] Studio found. Owner username: {studio.Owner}");

                if (string.IsNullOrEmpty(studio.Owner))
                {
                    Console.WriteLine("[DEBUG] Studio has no Owner");
                    return null;
                }

                // Step 2: Find the user
                var userFilter = Builders<User>.Filter.Eq(u => u.Username, studio.Owner);
                var user = await _users.Find(userFilter).FirstOrDefaultAsync();

                if (user == null)
                {
                    Console.WriteLine($"[DEBUG] User with username '{studio.Owner}' not found");
                    return null;
                }

                Console.WriteLine($"[DEBUG] User found. Email: {user.Mail}");

                return user.Mail;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetStudioOwnerEmail failed: {ex.Message}");
                return null;
            }
        }
    }
}
