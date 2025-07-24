using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repositories;
using RedisExampleApp.Cache;
using StackExchange.Redis;

namespace RedisExampleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        //private readonly RedisService _redisService;
        private readonly IDatabase db;

        public ProductsController(IProductRepository productRepository,RedisService redisService,IDatabase database)
        {
            _productRepository = productRepository;
            //_redisService = redisService;
            //var db = _redisService!.GetDataBase(0);
            //db.StringSet("ad","ferhat babar edis deneme");
            db = database;

            db.StringSet("soyadı ","Türe");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAsync());     
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productRepository.GetByIdAsync(id));
        }   
        
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            return Created(string.Empty,await _productRepository.CreateAsync(product));

            // created() 201 Created HTTP status code’u döner. (yani “başarıyla oluşturuldu” demek)

            //Opsiyonel olarak bir "Location" başlığı ekler → locationUrl parametresi. created(locationUrl,responseObject)

            //Cevap body'sine de responseObject’i koyar.
        }



    }
}
