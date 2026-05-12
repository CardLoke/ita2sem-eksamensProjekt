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
    }
}
