using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private IDatabase db;
        private string listKey = "names";

        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDatabase(1);

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name)
        {

            db.ListRightPush(listKey,name);


            return RedirectToAction("Index");   
        }
    }
}
