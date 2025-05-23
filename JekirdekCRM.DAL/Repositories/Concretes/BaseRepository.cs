using JekirdekCRM.DAL.Context;
using JekirdekCRM.DAL.Repositories.Abstracts;
using JekirdekCRM.ENTITIES.Enums;
using JekirdekCRM.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.DAL.Repositories.Concretes
{
    public class BaseRepository<T> : IRepository<T> where T : class,IEntity
    {
        private readonly MyContext _context;

        public BaseRepository(MyContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Status = DataStatus.Deleted;
            _context.SaveChanges();
        }

        public void Destroy(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Status = DataStatus.Updated;
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
