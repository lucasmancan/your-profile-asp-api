using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
    interface IPhoneRepository
{
    void Add(Phone phone);

    IEnumerable<Phone> GetAll();

    Phone Find(long id);

    void Remove(long id);

    void Update(Phone phone);
}
}
