using Customers.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Customers.DAL
{
    public class QueueCustomersDataAccess
    {
        private static List<Customer> _customers;
        public QueueCustomersDataAccess()
        {
            _customers = new List<Customer>();
        }

        public Customer CallNext(Customer currentCustomer, Customer nextCustomer)
        {
            using (var db = new CustomersDBcontext())
            {
                if (currentCustomer != null)
                {
                    db.Customers.First(c => c.ID == currentCustomer.ID).QueueStatus = QueueStatus.Done;
                }
                if (nextCustomer != null)
                {
                    db.Customers.First(c => c.ID == nextCustomer.ID).QueueStatus = QueueStatus.InService;
                }
                db.SaveChanges();
                return nextCustomer != null ? db.Customers.First(c => c.ID == nextCustomer.ID) : null;
            }

        }

        public int GetCurrentQueueNumber()
        {
            using (var db = new CustomersDBcontext())
            {
                return db.Customers.Max(x => x.QueueNumber);
            }
        }

        public IEnumerable<Customer> GetWaitingList()
        {
            using (var db = new CustomersDBcontext())
            {
                return db.Customers.Where(c => c.QueueStatus == QueueStatus.Waiting).ToList();
            }
        }

        public Customer InsertCustomerToQueue(Customer customer)
        {
            using (var db = new CustomersDBcontext())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            return customer;
        }

        public Customer GetCustomerInService()
        {
            using (var db = new CustomersDBcontext())
            {
                return db.Customers.FirstOrDefault(c => c.QueueStatus == QueueStatus.InService);
            }
        }
    }
}
