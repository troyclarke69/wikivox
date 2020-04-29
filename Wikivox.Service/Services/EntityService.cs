using System;
using System.Collections.Generic;
using System.Linq;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    public class EntityService : IEntity
    {
        private readonly WikivoxDbContext _context;
        public EntityService(WikivoxDbContext context) { _context = context; }
        public void Add(Entity newEntity)
        {
            _context.Add(newEntity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Entity.Where(g => g.Id == id)
               .ToList().ForEach(g => _context.Entity.Remove(g));
            _context.SaveChanges();
        }

        public Entity Get(int id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return _context.Entity;
        }

        public void Update(Entity newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
