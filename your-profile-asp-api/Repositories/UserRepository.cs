using aspApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace aspApi.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserDbContext _contexto;
        private readonly IConfiguration _configuration;


        public UserRepository(UserDbContext context, IConfiguration configuration)
        {
            _contexto = context;
            _configuration = configuration;
        }

        public void Add(User user)
        {
            _contexto.Users.Add(user);
            _contexto.SaveChanges();
        }

        string createToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["SecurityKey"],
              claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()) },
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User Find(int? id)
        {
            return _contexto.Users.FirstOrDefault(u => u.id == id);
        }

        public User Authenticate(Credentials credentials)
        {
            _contexto.Users.FirstOrDefault(u => u.email == credentials.email);



        }

        public IEnumerable<User> GetAll()
        {
            return _contexto.Users.ToList();
        }

        public void Remove(int id)
        {
            var entity = _contexto.Users.First(u => u.id == id);
            _contexto.Users.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(User user)
        {
            try
            {    
                _contexto.Entry(user.address).State = EntityState.Modified;
                _contexto.Entry(user.address.city).State = EntityState.Detached;
                _contexto.Entry(user.address.city.state).State = EntityState.Detached;
                _contexto.Entry(user.address.city.state.country).State = EntityState.Detached;
          
           
                _contexto.Entry(user).State = EntityState.Modified;
                _contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
