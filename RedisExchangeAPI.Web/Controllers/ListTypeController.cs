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
            List<string> names = new List<string>();     

            if (db.KeyExists(listKey))  // bu isimde key vardsa dbmde true döner
            {
                db.ListRange(listKey).ToList().ForEach(x =>
                {

                    names.Add(x.ToString());
                });

                return View(names);

            } 


            return View();
        }

        [HttpPost]
        public IActionResult Add(string name)
        {

            db.ListRightPush(listKey,name);


            return RedirectToAction("Index");   
        }

      
        public IActionResult DeleteItem(string name)
        {

            db.ListRemoveAsync(listKey,name).Wait();


            return RedirectToAction("Index");   
        }
        public IActionResult RemoveFirstItem()
        {

            db.ListLeftPop(listKey);


            return RedirectToAction("Index");   
        }
    }
}
