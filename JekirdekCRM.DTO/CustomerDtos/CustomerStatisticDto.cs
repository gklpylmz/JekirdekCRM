using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.DTO.CustomerDtos
{
    public class CustomerStatisticDto
    {
        public int ActiveCustomerCount { get; set; }
        public int PassiveCustomerCount { get; set; }
        public int DistinctRegionCustomerCount { get; set; }
    }
}
