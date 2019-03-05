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
using System.Security.Cryptography;
using aspApi.Services;

namespace aspApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly UserDbContext _contexto;
        private readonly IConfiguration _configuration;

        public CityRepository(UserDbContext context, IConfiguration configuration)
        {
            _contexto = context;
            _configuration = configuration;
        }

        public IEnumerable<City> findByState(int id)
        {
            return _contexto.Cities.Where(s => s.StateId == id).ToList();
        }
    }
}
