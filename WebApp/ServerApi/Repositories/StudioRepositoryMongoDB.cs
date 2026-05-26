using Core.Model;
using MongoDB.Driver;
using ServerApi.Interfaces;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ServerApi.Repositories
{
    public class StudioRepositoryMongoDB : IStudio
    {
        private readonly IMongoCollection<Studio>? _studios;

        public StudioRepositoryMongoDB()
        {
            var client = new MongoClient("mongodb+srv://kris600m:eyh94zkh@cluster0.xpou06p.mongodb.net/");
            var database = client.GetDatabase("SessionSync");
            _studios = database.GetCollection<Studio>("Studio");
        }

        public void RegisterStudio(Studio studio)
        {
            _studios.InsertOne(studio);
        }

        public List<Studio> GetAll()
        { //studio => true er egenligt et filter, men i dette tilfælde er alt true hver gang.
            return _studios.Find(studio => true).ToList();
        }
        public void Delete(int id)
        {
            _studios.DeleteOne(item => item.Id == id);
        }
        
        public async Task Edit(Studio studio)
        {
            await _studios.ReplaceOneAsync(s => s.Id == studio.Id, studio);
        }
        public void Invite(Invite invite)
        {
            var filter = Builders<Studio>.Filter.Eq(item => item.Id, invite.Studio.Id);
            var update = Builders<Studio>.Update.AddToSet(item => item.PrivateUsers, invite.Username);
            _studios.UpdateOne(filter, update);
        }
    }
}