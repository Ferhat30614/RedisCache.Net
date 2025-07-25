using Microsoft.AspNetCore.Mvc;
using RedisExampleApp.API.Models;
using RedisExampleApp.API.Services;


namespace RedisExampleApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IProductRepository _productService;
        //private readonly RedisService _redisService;
        //private readonly IDatabase db;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAsync());     
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }   
        
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            return Created(string.Empty,await _productService.CreateAsync(product));

            // created() 201 Created HTTP status code’u döner. (yani “başarıyla oluşturuldu” demek)

            //Opsiyonel olarak bir "Location" başlığı ekler → locationUrl parametresi. created(locationUrl,responseObject)

            //Cevap body'sine de responseObject’i koyar.
        }



    }
}
