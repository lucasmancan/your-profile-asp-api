using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
    public interface ICountryRepository
    {

    IEnumerable<Country> GetAll();

    }
}
