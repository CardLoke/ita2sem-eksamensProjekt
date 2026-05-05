using ServerApi.Interfaces;
using Core.Model;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ServerApi.Repositories
{
    public class StudioRepositoryMongoDB : IStudio
    {
        private readonly IMongoCollection<Studio>? _Studios;

        public StudioRepositoryMongoDB()
        {
            var client = new MongoClient("mongodb+srv://SessionSyncVitus:ilDBtBfI3Sr81WOl@cluster0.uq6pnjx.mongodb.net/");
            var database = client.GetDatabase("SessionSync");
            _Studios = database.GetCollection<Studio>("Studio");

        }

        public void registerStudio(Studio studio)
        {
            _Studios.InsertOne(studio);
        }
        
        
    }
}