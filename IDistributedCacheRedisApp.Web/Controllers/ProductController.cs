using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions distributedCacheEntryOptions=
                new DistributedCacheEntryOptions();
            distributedCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            //_distributedCache.SetString("ad","Ferhat", distributedCacheEntryOptions);
            //await _distributedCache.SetStringAsync("soyad","Ferhat", distributedCacheEntryOptions);

            Product product = new Product() { Id=1,Name="Telefon",Price=1233};

            string jsonProduct=JsonConvert.SerializeObject(product);

            _distributedCache.SetString("product:1",jsonProduct,distributedCacheEntryOptions);

            return View();
        }
        public IActionResult Show()
        {
            //string? ad=_distributedCache.GetString("ad");
            //ViewBag.Ad = ad;    
            string jsonProduct = _distributedCache.GetString("product:1");
            if (jsonProduct != null)
            {
                Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);

                ViewBag.product = product;
            }

            return View();
        }
        public IActionResult Remove()
        {
            //_distributedCache.Remove("ad");


            return View();
        }
    }
}
