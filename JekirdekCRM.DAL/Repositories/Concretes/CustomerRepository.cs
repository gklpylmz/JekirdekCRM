using JekirdekCRM.DAL.Context;
using JekirdekCRM.DAL.Repositories.Abstracts;
using JekirdekCRM.ENTITIES.Enums;
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

        public List<Customer> GetCustomerByFirstNameAndRegion(string? firstName = null, string? region = null)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(x => x.FirstName == firstName);
            }

            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(x=>x.Region == region);
            }

            return query.ToList();
        }
        public int GetActiveCustomerCount()
        {
            return _context.Customers.Where(x=>x.Status !=DataStatus.Deleted).Count();
        }
        public int GetDistinctRegionCount()
        {
            return _context.Customers.Select(x=>x.Region).Distinct().Count();
        }

        public int GetPassiveCustomerCount()
        {
            return _context.Customers.Where(x => x.Status == DataStatus.Deleted).Count();
        }
    }
}
