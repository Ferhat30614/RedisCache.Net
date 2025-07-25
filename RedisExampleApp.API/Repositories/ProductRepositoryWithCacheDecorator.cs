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
        private readonly IDatabase _cacheRepository;   

        public ProductRepositoryWithCacheDecorator(IProductRepository repository, RedisService redisService,IDatabase database)
        {
            _productRepository = repository;
            _redisService = redisService;
            _cacheRepository=database;
            _cacheRepository = _redisService.GetDataBase(2);
        }

        public Task<Product> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAsync()
        {
            if (!await _cacheRepository.KeyExistsAsync(hashKey))
            {
                return await LoadToCacheFrom_cacheRepositoryAsync();
            }

            var products = new List<Product>();

            var cacheProducts = _cacheRepository.HashGetAll(hashKey);

            foreach (var item in cacheProducts.ToList()) {

                var product = JsonSerializer.Deserialize<Product>(item.Value.ToString());

                 if(product != null ) 
                    products.Add(product);  

            }
            return products;    
        }

        public async Task<Product?> GetByIdAsync(int id)
        {

            if (_cacheRepository.KeyExists(hashKey)) {

                var cacheProduct =  await _cacheRepository.HashGetAsync(hashKey,id);
                return cacheProduct.HasValue ? JsonSerializer.Deserialize<Product>(cacheProduct) : null;        
           

            }

            var products = await LoadToCacheFromDbAsync();
            var product = products.FirstOrDefault(x => x.Id == id);

            return product;

        }

        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products= await _productRepository.GetAsync();

            products.ForEach(p =>
            {
                 _cacheRepository.HashSetAsync(hashKey,p.Id,JsonSerializer.Serialize(p));
            });

            return products;    
            
        }



    }
}
