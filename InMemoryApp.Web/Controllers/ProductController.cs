using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            _memoryCache.Remove("zaman");
            //if (!String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{

            //    _memoryCache.Remove("zaman");

            //}

            if (!_memoryCache.TryGetValue("zaman", out string zamanCache))
            {


                Console.WriteLine("zaman cache  =  null");

            }

            Console.WriteLine("zaman cache  = "+zamanCache);

            

            return View();
        }
        public IActionResult Show()
        {

            var sonuc= _memoryCache.GetOrCreate<string>("zaman", entry =>
            {
                return DateTime.Now.ToString();
            });

            Console.WriteLine("sonucum "+ sonuc);

            ViewBag.Zaman=_memoryCache.Get<string>("zaman");

            return View();
        }
    }
}
