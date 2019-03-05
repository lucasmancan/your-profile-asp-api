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
    public class CountryRepository : ICountryRepository
    {
        private readonly UserDbContext _contexto;
        private readonly IConfiguration _configuration;

        public CountryRepository(UserDbContext context, IConfiguration configuration)
        {
            _contexto = context;
            _configuration = configuration;
        }

        public IEnumerable<Country> GetAll()
        {
            return _contexto.Countries.ToList();
        }
    }
}
