using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IOrderService
    {
        OrderDto AddOrder(CreateOrderDto order);
        List<OrderDto> GetOrders();
        List<OrderDto> GetOrdersByUser(string userEmail);
        OrderDto GetOrder(int id);

    }
}
