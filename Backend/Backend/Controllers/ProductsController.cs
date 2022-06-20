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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            if (_productService.GetById(id) == null)
                return NotFound();
            return Ok(_productService.GetById(id));
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            if (_productService.GetById(id) == null)
                return NotFound();
            return Ok(_productService.UpdateProduct(id, product));
        }

        // POST: api/Products
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult PostProduct(ProductDto product)
        {
            return Ok(_productService.AddProduct(product));
        }

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public IActionResult DeleteProduct(int id)
        //{
        //    return Ok(_productService.DeleteProduct(id));
        //}
    }
}
