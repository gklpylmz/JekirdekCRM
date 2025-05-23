using JekirdekCRM.BLL.ManagerServices.Abstracts;
using JekirdekCRM.DAL.Repositories.Abstracts;
using JekirdekCRM.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        public BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void Create(T entity)
        {
            _repository.Create(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Destroy(T entity)
        {
            _repository.Destroy(entity);
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<T> GetListAll()
        {
            return _repository.GetListAll();
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }
    }
}
