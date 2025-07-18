using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Xml.Linq;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private  IDatabase db;

        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDatabase(0);

        }

        public IActionResult Index()
        {

            db.StringSet("name","ferhat ture");

            return View();
        }
        public IActionResult Show()
        {
            var value = db.StringGet("name");

            if (value.HasValue)
            {
                ViewBag.value = value;
            }


            return View();
        }
    }
}
