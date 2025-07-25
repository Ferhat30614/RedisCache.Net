using Microsoft.Extensions.Configuration.EnvironmentVariables;
using RedisExampleApp.API.Dtos;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repositories;

namespace RedisExampleApp.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateAsync(Product product)
        {
            var result = await _productRepository.CreateAsync(product);    
            return new ProductDto { 
                Id = result.Id,    
                Name = result.Name,        
                Price = result.Price
            };    

            
        }

        public async Task<List<ProductDto>> GetAsync()
        {
            var productList = await _productRepository.GetAsync();
      
            return productList.Select(product => new ProductDto
            {
                Id = product.Id,    
                Name = product.Name,    
                Price = product.Price,  
            }).ToList();    

        }

        public async  Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);    
            return new ProductDto{ 
                Name =product.Name,
                Price = product.Price,  
                Id = product.Id     
            };
        }
    }
}
