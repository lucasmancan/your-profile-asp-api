using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
    public interface IPhoneRepository
{
    void Add(Phone phone);

    IEnumerable<Phone> GetAll();

    Phone Find(int? id);

    void Remove(int? id);

    void Update(Phone phone);
}
}
