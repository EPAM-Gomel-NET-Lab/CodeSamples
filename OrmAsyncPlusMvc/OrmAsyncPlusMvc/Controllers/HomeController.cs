using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using EF_Task;

namespace AsyncVsSyncExample.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new System.Uri("https://www.google.com/")
        };

        private readonly IProductCategoryService _productCategoryService;

        public HomeController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        public ActionResult Category(int categoryId)
        {
            return RedirectToAction(nameof(Async), categoryId);
        }

        public ActionResult Index()
        {
            var productInfo = _productCategoryService.GetProductInfoForCategory(3);
            return Json(productInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sleep()
        {
            Thread.Sleep(500);
            return new EmptyResult();
        }

        public async Task<ActionResult> SleepAsync()
        {
            await Task.Delay(500);
            return new EmptyResult();
        }

        public async Task<ActionResult> Async()
        {
            var productInfo = await _productCategoryService.GetProductInfoForCategoryAsync(3);
            return Json(productInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public async Task<ActionResult> NetworkCallAsync()
        {
            var response = await _httpClient.GetAsync("");
            var result = await response.Content.ReadAsStringAsync();
            return Content(result);
        }

        public ActionResult NetworkCall()
        {
            var webRequest = WebRequest.Create("https://www.google.com/");
            var response = webRequest.GetResponse();
            var memoryStream = new MemoryStream();
            using (var stream = response.GetResponseStream())
            {
                stream.CopyTo(memoryStream);
            }
            var result = Encoding.UTF8.GetString(memoryStream.ToArray());

            return Content(result);
        }

        public async Task<ActionResult> AsyncCategory()
        {
            var productInfo = await _productCategoryService.GetProductInfoForCategoryAsync(3);
            return Json(productInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
