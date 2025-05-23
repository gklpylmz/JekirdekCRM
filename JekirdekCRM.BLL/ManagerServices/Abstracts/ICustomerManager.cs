using JekirdekCRM.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.BLL.ManagerServices.Abstracts
{
    public interface ICustomerManager:IManager<Customer>
    {
        //Customer's Special Methods
        List<Customer> GetCustomerByRegion(string region);
        List<Customer> GetCustomerByFirtName(string firstName);
    }
}
