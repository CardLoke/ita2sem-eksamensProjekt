using System.Collections.Generic;
using Core.Model;
namespace ServerApi.Interfaces
{
    public interface IInvite
    {
        void PostInvite(Invite invite);
        List<Invite> GetInvites(string username);
        void Delete(int id);
    }
}
