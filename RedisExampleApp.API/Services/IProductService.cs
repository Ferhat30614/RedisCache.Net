using RedisExampleApp.API.Dtos;
using RedisExampleApp.API.Models;

namespace RedisExampleApp.API.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(Product product);
    }
}
