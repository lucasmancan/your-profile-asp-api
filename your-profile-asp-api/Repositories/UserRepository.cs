using aspApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace aspApi.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserDbContext _contexto;

        public UserRepository(UserDbContext context)
        {
            _contexto = context;
        }
        public void Add(User user)
        {
            _contexto.Users.Add(user);
            _contexto.SaveChanges();
        }

        public User Find(int? id)
        {
            return _contexto.Users.FirstOrDefault(u => u.id == id);
        }
        public User Authenticate(string email)
        {
            return _contexto.Users.FirstOrDefault(u => u.email == email);
        }

        public IEnumerable<User> GetAll()
        {
            return _contexto.Users.ToList();
        }

        public void Remove(long id)
        {
            var entity = _contexto.Users.First(u => u.id == id);
            _contexto.Users.Remove(entity);
            _contexto.SaveChanges();
        }

        public async void UpdateAsync(User user)
        {

            try
            {


                _contexto.Entry(user.address).State = user.addressId == 0 ? EntityState.Added : EntityState.Modified;
                _contexto.Entry(user.address.city).State = user.address.cityId == 0 ? EntityState.Added : EntityState.Modified;
                _contexto.Entry(user.address.city.state).State = user.address.city.stateId == 0 ? EntityState.Added : EntityState.Modified;
                _contexto.Entry(user.address.city.state).State = user.address.city.state.countryId == 0 ? EntityState.Added : EntityState.Modified;


                _contexto.Entry(user).State = EntityState.Modified;
                //_contexto.Users.Update(user);
                _contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
