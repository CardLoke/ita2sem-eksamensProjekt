using ServerApi.Interfaces;
using Core.Model;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ServerApi.Repositories
{
    public class BookingRepositoryMongoDB : IBooking
    {
        private readonly IMongoCollection<BookingData>? _bookings;

        public BookingRepositoryMongoDB()
        {
            var client = new MongoClient("mongodb+srv://kris600m:eyh94zkh@cluster0.xpou06p.mongodb.net/");
            var database = client.GetDatabase("SessionSync");
            _bookings = database.GetCollection<BookingData>("Booking");
        }
        public void Booking(BookingData data)
        {
            Console.WriteLine(data.Id);
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
    }
}
