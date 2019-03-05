using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
    public interface IUserRepository
    {
        User Add(User user);

        IEnumerable<User> GetAll();

        User Find(int? id);

        User FindByEmail(string email);

        string Authenticate(Credentials credentials);

        string createToken(User user);

        void Remove(int id);

        void Update(User user);

        Task uploadCoverImage(int userId, string image);

        Task uploadProfileImage(int userId, string image);

        void UpdatePassword(User user);
    }
}
