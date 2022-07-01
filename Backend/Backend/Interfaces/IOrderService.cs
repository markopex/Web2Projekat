using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IOrderService
    {
        OrderDto AddOrder(string customerEmail, CreateOrderDto orderDto);
        List<OrderDto> GetOrders();
        List<OrderDto> GetPendingOrders();
        List<OrderDto> GetOrdersByUser(string userEmail);
        OrderDto GetOrder(int id);
        OrderDto DeliverOrder(int id, string userEmail);


    }
}
