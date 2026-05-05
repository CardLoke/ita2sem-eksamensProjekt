using ServerApi.Interfaces;
using Core.Model;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ServerApi.Repositories
{
    public class UserRepositoryMongoDB : IUser
    {
        private readonly IMongoCollection<User>? _Users;

        public UserRepositoryMongoDB()
        {
            //var client = new MongoClient("mongodb+srv://kris600m:eyh94zkh@cluster0.xpou06p.mongodb.net/");
            var client = new MongoClient("mongodb+srv://Semesterprojekt:Hundlika1@cluster0.s0l3m62.mongodb.net/");
            var database = client.GetDatabase("SessionSync");
            _Users = database.GetCollection<User>("Users");

        }
        public void SignUp(User user) {
            _Users.InsertOne(user);
        }
        public async Task<User?> LogIn(LoginRequest loginRequest)
        {
            return await _Users.Find(u =>
                u.Username == loginRequest.Username && u.Password == loginRequest.Password)
                .FirstOrDefaultAsync();
        }
        public async Task Edit(User user)
        {
            await _Users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}
