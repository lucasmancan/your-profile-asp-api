using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
    public interface IStateRepository
    {

    IEnumerable<State> findByContry(int id);

    }
}
