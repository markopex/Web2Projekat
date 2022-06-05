using AutoMapper;
using Backend.Dto;
using Backend.Infrastructure;
using Backend.Interfaces;
using Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly RestorauntDbContext _dbContext;

        public ProductService(IMapper mapper, RestorauntDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public ProductDto AddProduct(ProductDto newProductDto)
        {
            newProductDto.Id = 0;
            var product = _mapper.Map<Product>(newProductDto);
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return _mapper.Map<ProductDto>(newProductDto);
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public ProductDto GetById(int id)
        {
            return _mapper.Map<ProductDto>(_dbContext.Products.Find(id));
        }

        public List<ProductDto> GetProducts()
        {
            return _mapper.Map<List<ProductDto>>(_dbContext.Products.ToList());
        }

        public ProductDto UpdateProduct(int id, ProductDto newProductData)
        {
            Product product = _dbContext.Products.Find(id); //Ucitavamo objekat u db context (ako postoji naravno)
            product.Name = newProductData.Name;
            product.Description = newProductData.Description;
            product.Price = newProductData.Price;

            _dbContext.SaveChanges(); //Samo menjanje polja ucitanog studenta iz baze podataka je dovoljno
                                      //da se ti podaci promene i u bazi nakon cuvanja

            return _mapper.Map<ProductDto>(product);
        }
    }
}
