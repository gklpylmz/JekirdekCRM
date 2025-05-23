using JekirdekCRM.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.DAL.Repositories.Abstracts
{
    public interface IRepository<T> where T : class,IEntity
    {
        //List Commands
        List<T> GetListAll();

        //CUD Commands
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Destroy(T entity);

        //Filter Commands
        T GetById(int id);
    }
}
