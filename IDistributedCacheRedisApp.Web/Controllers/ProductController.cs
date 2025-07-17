using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
            distributedCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            _distributedCache.SetString("ad","Ferhat", distributedCacheEntryOptions);
            await _distributedCache.SetStringAsync("soyad","Ferhat", distributedCacheEntryOptions);

            return View();
        }
        public IActionResult Show()
        {
            string? ad=_distributedCache.GetString("ad");

            ViewBag.Ad = ad;    


            return View();
        }
        public IActionResult Remove()
        {
            _distributedCache.Remove("ad");


            return View();
        }
    }
}
