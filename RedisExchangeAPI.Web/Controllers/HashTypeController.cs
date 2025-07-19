using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        private string hashKey = "Sozluk";
        public HashTypeController(RedisService redisService):base(redisService){
        
        }
        public IActionResult Index()
        {
            Dictionary<string,string> dictionary = new Dictionary<string,string>();

            db.HashGetAll(hashKey).ToList().ForEach(x =>
            {

                dictionary.Add(x.Name.ToString(),x.Value.ToString());

            });

            return View(dictionary);
        }


        public IActionResult Add(string name,string value)
        {
            db.HashSet(hashKey,name,value);
            
            return RedirectToAction("Index");   
        }


        public IActionResult DeleteItem(string name)
        {
            db.HashDelete(hashKey,name);
            
            return RedirectToAction("Index");   
        }
    }
}
