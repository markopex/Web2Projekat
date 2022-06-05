using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public User Customer { get; set; }
        public User Deliverer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public String Comment { get; set; }
        public String Address { get; set; }
        public double Price { get; set; }
        public long UTCTimeOrdered { get; set; }
        public long UTCTimeDeliveryStarted { get; set; }
    }
}
