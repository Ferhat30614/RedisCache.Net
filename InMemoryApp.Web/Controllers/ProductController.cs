using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mono.TextTemplating;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //_memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //_memoryCache.Remove("zaman");
            //if (!String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{

            //    _memoryCache.Remove("zaman");

            //}

            //if (!_memoryCache.TryGetValue("zaman", out string zamanCache))
            //{
            //    Console.WriteLine("zaman cache  =  null");
            //}
            //Console.WriteLine("zaman cache  = "+zamanCache);


            MemoryCacheEntryOptions option = new MemoryCacheEntryOptions();

            option.AbsoluteExpiration=DateTime.Now.AddSeconds(10);
            //option.SlidingExpiration=TimeSpan.FromSeconds(10);   

            option.Priority=CacheItemPriority.High;
            option.RegisterPostEvictionCallback((key, value, reason, state) =>
            {

                //cachede silme işlemi olunca ilk önce parametrelere değer  gelcek sonra metod gövdesi çalışcak

                _memoryCache.Set<string>("callback", $" key: {key} -> değer: {value}----> sebep:  {reason}");

            });

            

            _memoryCache.Set<string>("zaman",DateTime.Now.ToString(),option);

            ProductModel p =new ProductModel() { Id=1 , Name="Telefon", Price=123};

            _memoryCache.Set<ProductModel>("product:1",p);


            return View();
        }
        public IActionResult Show()
        {

            _memoryCache.TryGetValue("zaman",out string? zamanCache);
            _memoryCache.TryGetValue("callback", out string? callback);



            ViewBag.Zaman = zamanCache;
            ViewBag.CallBack = callback;
            ViewBag.Product = _memoryCache.Get<ProductModel>("product:1");

            return View();
        }
    }
}
