using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private IDatabase db;
        private string listKey = "SortedSetName";

        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDatabase(3);

        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (db.KeyExists(listKey)) {
                //db.SortedSetScan(listKey).ToList().ForEach(x =>
                //{
                    // x.element diyerek sadece valueyi aldım 
                //    list.Add(x.Element.ToString());  // anlamadım bi şekilde burda score sırasına göre hashsete ekliyor ... karıştıması gerekirdi oysaki
                //});
                db.SortedSetRangeByRank(listKey,0,3, order: Order.Descending).ToList().ForEach(x => {  //bu kod mesela redisdeki tüm verimi alır önce bbuyukten kucuge
                                                                                                       //sıralar sopnraysa sıralı  liste 
                                                                                                       //içinde 0-3 indexlerini sırayla getirir
                    list.Add(x.ToString());
                });       
                
            }
            return View(list);

        }
        public IActionResult Add(string name ,int score)
        {
            db.SortedSetAdd(listKey,name,score);
            db.KeyExpire(listKey,DateTime.Now.AddMinutes(1));


            return RedirectToAction("Index");   
        }
        
        public IActionResult DeleteItem(string name)
        {

            db.SortedSetRemove(listKey,name);    // burdaki silme işlemine dikkat et sadece value olmalı ÖR: value:score gelirse çalışmaz

            return RedirectToAction("Index");
        }

    }
}
