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
    public class StateRepository : IStateRepository
    {
        private readonly UserDbContext _contexto;
        private readonly IConfiguration _configuration;

        public StateRepository(UserDbContext context, IConfiguration configuration)
        {
            _contexto = context;
            _configuration = configuration;
        }

        public IEnumerable<State> findByContry(int id)
        {
            return _contexto.States.Where(s => s.CountryId == id).ToList();
        }
    }
}
