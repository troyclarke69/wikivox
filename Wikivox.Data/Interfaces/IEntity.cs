using System.Collections.Generic;
using Wikivox.Models;

namespace Wikivox.Data.Interfaces
{
    public interface IEntity
    {
        IEnumerable<Entity> GetAll();
        Entity Get(int id);
        void Add(Entity newEntity);
        void Update(Entity newEntity);
        void Delete(int id);
    }
}
