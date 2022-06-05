﻿namespace Backend.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public uint Quantity { get; set; }
        public double ProductPrice { get; set; }
        public double Price { get {return ProductPrice * Quantity; } }
    }
}
