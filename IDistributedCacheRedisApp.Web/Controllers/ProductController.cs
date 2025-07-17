using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

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
            distributedCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            //_distributedCache.SetString("ad","Ferhat", distributedCacheEntryOptions);
            //await _distributedCache.SetStringAsync("soyad","Ferhat", distributedCacheEntryOptions);

            Product product = new Product() { Id=1,Name="Araba",Price=758123};

            string jsonProduct=JsonConvert.SerializeObject(product);   // bu şekilde ben bütün complext typlarımı vs serilize edip redise gönderebilirim

            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);

            _distributedCache.Set("product:1", byteProduct, distributedCacheEntryOptions);

            return View();
        }
        public IActionResult Show()
        {
            //string? ad=_distributedCache.GetString("ad");
            //ViewBag.Ad = ad;
            //


            var byteProduct = _distributedCache.Get("product:1");

            var jsonProduct = Encoding.UTF8.GetString(byteProduct);


            //string jsonProduct = _distributedCache.GetString("product:1");
            if (jsonProduct != null)
            {
                Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);

                ViewBag.product = product;
            }

            return View();
        }
        public IActionResult Remove()
        {
            _distributedCache.Remove("ad");


            return View();
        }
        
        public IActionResult ImageCache()
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/Uzay_Resmi.jpg");//dosya yolum

            Byte[] imageByte= System.IO.File.ReadAllBytes(path); //dosyamı byte dizinine çevirdim 
            _distributedCache.Set("resim", imageByte); // byte dizinimi kaydettim redise
             
            return View();
        }
        public IActionResult ImageUrl()
        {

            Byte[] imageByte = _distributedCache.Get("resim");



            return File(imageByte, "image/jpg");
        }
        public IActionResult PdfCache()
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Folders/Vize_Sınavı.pdf");//dosya yolum

            Byte[] pdfByte = System.IO.File.ReadAllBytes(path); //dosyamı byte dizinine çevirdim 
            _distributedCache.Set("pdf", pdfByte); // byte dizinimi kaydettim redise

            return View();
        }

        public IActionResult PdfUrl()
        {

            Byte[] pdfByte = _distributedCache.Get("pdf");

            if (pdfByte == null)
            {
                return NotFound(); // Eğer dosya bulunamazsa 404 döndür
            }
            return File(pdfByte, "application/pdf");
        }
    }
}
