﻿using JekirdekCRM.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.DAL.Repositories.Abstracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        //Customer's Special Methods
        List<Customer> GetCustomerByFirstNameAndRegion(string? firstName = null,string? region =null);
        int GetActiveCustomerCount();
        int GetPassiveCustomerCount();
        int GetDistinctRegionCount();
    }
}
