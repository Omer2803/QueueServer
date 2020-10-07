namespace Customers.DAL
{
    using Customers.Common.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CustomersDBcontext : DbContext
    {
        public CustomersDBcontext()
            : base("name=CustomersDBcontext")
        {
        }

         public virtual DbSet<Customer> Customers { get; set; }
    }

}