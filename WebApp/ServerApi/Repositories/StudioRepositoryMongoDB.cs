using ServerApi.Interfaces;
using Core.Model;
using System.Collections.Generic;
using MongoDB.Driver;

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

        public void registerStudio(Studio studio)
        {
            _studios.InsertOne(studio);
        }

        public List<Studio> GetAll()
        { //studio => true er egenligt et filter, men i dette tilfælde er alt true hver gang.
            return _studios.Find(studio => true).ToList();
        }
        
    }
}