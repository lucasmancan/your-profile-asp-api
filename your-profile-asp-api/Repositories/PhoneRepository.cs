using aspApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Repositories
{
    public class PhoneRepository : IPhoneRepository
{
    private readonly UserDbContext _contexto;

    public PhoneRepository(UserDbContext context)
    {
        _contexto = context;
    }
    public void Add(Phone Phone)
    {
        _contexto.Phones.Add(Phone);
        _contexto.SaveChanges();
    }

    public Phone Find(long id)
    {
        return _contexto.Phones.FirstOrDefault(u => u.id == id);
    }

    public IEnumerable<Phone> GetAll()
    {
        return _contexto.Phones.ToList();
    }

    public void Remove(long id)
    {
        var entity = _contexto.Phones.First(u => u.id == id);
        _contexto.Phones.Remove(entity);
        _contexto.SaveChanges();
    }

    public void Update(Phone Phone)
    {
        _contexto.Phones.Update(Phone);
        _contexto.SaveChanges();
    }
}
}

