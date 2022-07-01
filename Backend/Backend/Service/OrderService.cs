using AutoMapper;
using Backend.Dto;
using Backend.Infrastructure;
using Backend.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly RestorauntDbContext _dbContext;
        private static Object thisLock = new Object();
        private static Object addLock = new object();

        public OrderService(IMapper mapper, RestorauntDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public OrderDto AddOrder(string customerEmail, CreateOrderDto orderDto)
        {
            lock (addLock)
            {
                var customer = _dbContext.Users.Find(customerEmail);
                var customerOrder = _dbContext.Orders.ToList().Find(i => i.CustomerEmail == customerEmail && i.DeliveredTimeExpected == 0);
                if (customerOrder != null)
                    throw new Exception("You can't create new order until you are waiting for order.");

                List<OrderDetail> orderDetailsList = new List<OrderDetail>();
                foreach (var orderDetailsDto in orderDto.OrderDetails)
                {
                    var orderDetails = _mapper.Map<OrderDetail>(orderDetailsDto);
                    var product = _dbContext.Products.Find(orderDetailsDto.ProductId);
                    orderDetails.Product = product;
                    orderDetails.ProductPrice = product.Price;

                    orderDetailsList.Add(orderDetails);
                }

                var order = _mapper.Map<Order>(orderDto);
                order.OrderDetails = orderDetailsList;
                order.Customer = customer;
                order.CustomerEmail = customer.Email;
                _dbContext.Add(order);
                _dbContext.OrderDetails.AddRange(orderDetailsList);
                _dbContext.SaveChanges();


                return _mapper.Map<OrderDto>(order);
            }
        }

        public OrderDto DeliverOrder(int id, string userEmail)
        {
            lock (thisLock)
            {
                Order order = _dbContext.Orders.Find(id);
                if (order == null)
                    throw new Exception("No order with given id");

                if (order.DeliveredTimeExpected == 0)
                    throw new Exception("Delivering of given order is already started.");

                var user = _dbContext.Users.Find(userEmail);
                order.UTCTimeDeliveryStarted = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                Random rnd = new Random();
                int month = rnd.Next(1, 13);
                order.DeliveredTimeExpected = rnd.Next(30, 60) * 1000;
                order.DelivererEmail = userEmail;
                _dbContext.SaveChanges();

                var retVal = _mapper.Map<OrderDto>(order);

                return retVal;
            }
        }

        public OrderDto GetOrder(int id)
        {
            return _mapper.Map<OrderDto>(_dbContext.Orders.Find(id));
        }

        public List<OrderDto> GetOrders()
        {
            return _mapper.Map<List<OrderDto>>(_dbContext.Orders.ToList());
        }

        public List<OrderDto> GetOrdersByUser(string userEmail)
        {
            return _mapper.Map<List<OrderDto>>(_dbContext.Orders.ToList().FindAll(i => i.Customer.Email == userEmail));
        }

        public List<OrderDto> GetPendingOrders()
        {
            return _mapper.Map<List<OrderDto>>(_dbContext.Orders.ToList().FindAll(i => i.DeliveredTimeExpected == 0).ToList());
        }
    }
}
