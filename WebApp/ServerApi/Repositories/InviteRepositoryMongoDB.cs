using ServerApi.Interfaces;
using Core.Model;
using System.Collections.Generic;
using MongoDB.Driver;
namespace ServerApi.Repositories
{
    public class InviteRepositoryMongoDB : IInvite
    {
        private readonly IMongoCollection<Invite>? _invites;

        public InviteRepositoryMongoDB()
        {
            var client = new MongoClient("mongodb+srv://kris600m:eyh94zkh@cluster0.xpou06p.mongodb.net/");
            var database = client.GetDatabase("SessionSync");
            _invites = database.GetCollection<Invite>("Invites");
            
        }
        public void PostInvite(Invite invite)
        {
            _invites.InsertOne(invite);
        }
        public List<Invite> GetInvites(string username)
        {
            var filter = Builders<Invite>.Filter.Eq(item => item.Username, username);
            List<Invite> inviteList = _invites.Find(filter).ToList();
            return inviteList;
        }
        public void Delete(int id)
        {
            _invites.DeleteOne(item => item.Id == id);
        }
    }
}
