using System;

namespace Customers.Common.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public QueueStatus QueueStatus { get; set; }
        public DateTime CheckInTime { get; set; }
        public int QueueNumber { get; set; }

    }

    public enum QueueStatus
    {
        Waiting,
        InService,
        Done
    }
}
