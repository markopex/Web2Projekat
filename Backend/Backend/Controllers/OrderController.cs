using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure;
using Backend.Models;
using Backend.Interfaces;
using Backend.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            return Ok(_orderService.GetOrders());
        }

        //// PUT: api/Order/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public IActionResult PutOrder(int id, Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult PostOrder(CreateOrderDto createOrderDto)
        {
            return Ok(_orderService.AddOrder(createOrderDto));
        }
    }
}
