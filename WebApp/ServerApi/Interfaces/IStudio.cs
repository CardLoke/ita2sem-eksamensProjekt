using System.Collections.Generic;
using Core.Model;
namespace ServerApi.Interfaces
{ 
    public interface IStudio
    {
        void RegisterStudio(Studio studio);
        List<Studio> GetAll();
        void Delete(int id);
        Task Edit(Studio studio);
        void Invite(Invite invite);
    }
    
}