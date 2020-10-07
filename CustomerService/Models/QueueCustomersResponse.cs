using Customers.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService.Models
{
    public class QueueCustomersResponse
    {
        public Customer CustomerInService { get; set; }
        public Customer NextCustomer { get; set; }
    }
}