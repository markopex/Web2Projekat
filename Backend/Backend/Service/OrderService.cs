using AutoMapper;
using Backend.Dto;
using Backend.Infrastructure;
using Backend.Interfaces;
using Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly RestorauntDbContext _dbContext;

        public OrderService(IMapper mapper, RestorauntDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public OrderDto AddOrder(CreateOrderDto orderDto)
        {
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();
            foreach(var orderDetailsDto in orderDto.OrderDetails)
            {
                var orderDetails = _mapper.Map<OrderDetail>(orderDetailsDto);
                var product = _dbContext.Products.Find(orderDetailsDto.ProductId);
                orderDetails.Product = product;
                orderDetails.ProductPrice = product.Price;

                orderDetailsList.Add(orderDetails);
            }

            var order = _mapper.Map<Order>(orderDto);
            order.OrderDetails = orderDetailsList;
            order.UTCTimeDeliveryStarted = -1;
            order.DeliveredTimeExpected = -1;
            _dbContext.Add(order);
            _dbContext.OrderDetails.AddRange(orderDetailsList);
            _dbContext.SaveChanges();


            return _mapper.Map<OrderDto>(order);
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
    }
}
