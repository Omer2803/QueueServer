using Customers.Common.Models;
using Customers.DAL;
using System;
using System.Collections.Generic;

namespace Customers.BL
{
    public class QueueCustomersManager
    {
        private int _queueNumber;
        private readonly QueueCustomersDataAccess _queueCustomersDataAccess;

        public QueueCustomersManager()
        {
            _queueCustomersDataAccess = new QueueCustomersDataAccess();
        }

        public IEnumerable<Customer> GetWaitingList()
        {
            return _queueCustomersDataAccess.GetWaitingList();
        }

        public Customer InsertToQueue(string name)
        {
            try
            {
                _queueNumber = _queueCustomersDataAccess.GetCurrentQueueNumber();
            }
            catch (Exception)
            {

                _queueNumber = 0;
            }
            var newCustomer = new Customer()
            {
                Name = name,
                QueueNumber = ++_queueNumber,
                QueueStatus = QueueStatus.Waiting,
                CheckInTime = DateTime.Now
            };
            return _queueCustomersDataAccess.InsertCustomerToQueue(newCustomer);
        }

        public Customer CallNext(Customer customerInService, Customer nextCustomer)
        {
            return _queueCustomersDataAccess.CallNext(customerInService, nextCustomer);
        }

        public Customer GetCustomerInService()
        {
            return _queueCustomersDataAccess.GetCustomerInService();
        }
    }
}
