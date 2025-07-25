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

        public Task<ProductDto> CreateAsync(Product product)
        {
            throw new NotImplementedException();
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

        public Task<ProductDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
