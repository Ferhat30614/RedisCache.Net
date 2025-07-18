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
            db.StringSet("ziyaretci", 125);
            return View();
        }
        public IActionResult Show()
        {
            //var value = db.StringGet("name");
            //var value = db.StringGetRange("name",1,5);
            var value = db.StringLength("name");

            //var ziyaretci = db.StringIncrementAsync("ziyaretci", 1).Result; //ziyaretic 1 artırıp sonuc döner

            var ziyaretci = db.StringDecrement("ziyaretci",1);

            Byte[] byteDizi = default(byte[]);
            db.StringSet("dosyam", byteDizi);

     
          
                ViewBag.value= value;
                ViewBag.ziyaretci= ziyaretci;
        

            return View();
        }
    }
}
