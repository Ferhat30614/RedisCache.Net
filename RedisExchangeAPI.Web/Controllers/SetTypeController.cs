using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Runtime.ConstrainedExecution;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {

        private readonly RedisService _redisService;
        private IDatabase db;
        private string listKey = "SetNames";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDatabase(2);

        }


        public IActionResult Index()
        {
            HashSet<string> hashName = new HashSet<string>();
            db.SetAdd(listKey,"ferhat");

            if (db.KeyExists(listKey)) {

                 db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    hashName.Add(x.ToString());

                });

                return View(hashName);  
            
            }

                return View();
        }


        [HttpPost]
        public IActionResult Add(string name)
        {

            //if (!db.KeyExists(listKey)) {
            //    db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));

            //}
            ////Bu kullanım  sadece absolute time için olan kullanım 

            db.KeyExpire(listKey, DateTime.Now.AddMinutes(1)); //buda sliding gibi olan k.

            db.SetAdd(listKey,name);

            return RedirectToAction("Index");


            //Redis’in Set, List, Hash, SortedSet gibi koleksiyon tiplerinde,
            //bireysel elemanlara farklı expiration süresi veremezsin.

            //“Her elemanı ayrı bir key olarak tutarsan, o zaman tabii ki expiration veririz.”
            // ama burdada bukez koleksiyon yapısh bozuuır..
        }

        public async  Task<IActionResult> DeleteItem(string name)
        {
            await db.SetRemoveAsync(listKey,name);      
            return RedirectToAction("Index");
        }



    }
}
