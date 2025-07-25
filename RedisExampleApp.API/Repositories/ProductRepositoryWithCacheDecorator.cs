using RedisExampleApp.API.Models;
using RedisExampleApp.Cache;
using StackExchange.Redis;
using System.Reflection;
using System.Text.Json;

namespace RedisExampleApp.API.Repositories
{
    public class ProductRepositoryWithCacheDecorator : IProductRepository
    {

        private const string hashKey = "productCaches";
        private readonly IProductRepository _productRepository;        
        private readonly RedisService _redisService;
        private readonly IDatabase db;   

        public ProductRepositoryWithCacheDecorator(IProductRepository repository, RedisService redisService,IDatabase database)
        {
            _productRepository = repository;
            _redisService = redisService;
            db=database;
            db = _redisService.GetDataBase(2);
        }

        public Task<Product> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAsync()
        {
            if (!await db.KeyExistsAsync(hashKey))
            {
                return await LoadToCacheFromDbAsync();
            }

            var products = new List<Product>();

            var cacheProducts = db.HashGetAll(hashKey);

            foreach (var item in cacheProducts.ToList()) {


                var product = JsonSerializer.Deserialize<Product>(item.Value.ToString());

                 if(product != null ) 
                    products.Add(product);  

            }
            return products;    


        }

        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products= await _productRepository.GetAsync();

            products.ForEach(p =>
            {
                 db.HashSetAsync(hashKey,p.Id,JsonSerializer.Serialize(p));
            });

            return products;    
            
        }



    }
}
