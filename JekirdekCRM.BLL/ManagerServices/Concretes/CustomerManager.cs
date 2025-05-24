using JekirdekCRM.BLL.ManagerServices.Abstracts;
using JekirdekCRM.DAL.Repositories.Abstracts;
using JekirdekCRM.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.BLL.ManagerServices.Concretes
{
    public class CustomerManager : BaseManager<Customer>,ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository) :base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetCustomerByFirstNameAndRegion(string? firstName = null, string? region = null)
        {
            return _customerRepository.GetCustomerByFirstNameAndRegion(firstName, region);
        }
        public int GetActiveCustomerCount()
        {
            return _customerRepository.GetActiveCustomerCount();
        }
        public int GetDistinctRegionCount()
        {
            return _customerRepository.GetDistinctRegionCount();
        }

        public int GetPassiveCustomerCount()
        {
            return _customerRepository.GetPassiveCustomerCount();
        }
    }
}
