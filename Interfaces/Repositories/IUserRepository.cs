using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(AspNetUsers user);

        IEnumerable<AspNetUsers> GetAllUsers();

        AspNetUsers GetUser(int id);

        AspNetUsers GetUserByUsername(string username);

        AspNetUsers GetUserByEmailAddress(string emailAddress);

        void UpdateUser(AspNetUsers user);

        void DeleteUser(AspNetUsers user);
    }
}