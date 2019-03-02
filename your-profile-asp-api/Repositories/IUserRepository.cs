using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
  public interface IUserRepository
{
    void Add(User user);

    IEnumerable<User> GetAll();

    User Find(int? id);

    User Authenticate(string email);

    void Remove(int id);

    void Update(User user);
}
}
