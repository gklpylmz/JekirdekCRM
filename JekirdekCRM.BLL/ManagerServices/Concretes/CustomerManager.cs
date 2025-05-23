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

        public List<Customer> GetCustomerByFirtName(string firstName)
        {
            return _customerRepository.GetCustomerByFirtName(firstName);
        }

        public List<Customer> GetCustomerByRegion(string region)
        {
            return _customerRepository.GetCustomerByRegion(region);
        }
    }
}
