using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User Customer { get; set; }
        public User Deliverer { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public long TimeOrdered { get; set; }
        public long TimeDeliveryStarted { get; set; }
    }
}
