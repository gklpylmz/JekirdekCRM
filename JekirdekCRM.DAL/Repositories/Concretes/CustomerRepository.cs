using JekirdekCRM.DAL.Context;
using JekirdekCRM.DAL.Repositories.Abstracts;
using JekirdekCRM.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.DAL.Repositories.Concretes
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly MyContext _context;

        public CustomerRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public List<Customer> GetCustomerByFirtName(string firstName)
        {
            return _context.Customers.Where(x=>x.FirstName == firstName).ToList();
        }

        public List<Customer> GetCustomerByRegion(string region)
        {
            return _context.Customers.Where(x => x.Region == region).ToList();
        }
    }
}
