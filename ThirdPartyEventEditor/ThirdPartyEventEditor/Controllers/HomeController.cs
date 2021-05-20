using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThirdPartyEventEditor.Models;

namespace ThirdPartyEventEditor.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var circusEvent = new ThirdPartyEvent()
            {
                Name = "Почти серьезно",
                EndDate = new DateTime(2021, 06, 30, 21, 00, 00),
                StartDate = new DateTime(2021, 05, 30, 15, 00, 00),
                PosterImage = await UploadSampleImage(),
                Description = @"С 15 мая по 1 августа Белгосцирк и Московский цирк Ю.Никулина на
Цветном бульваре представляют новую цирковую программу «Почти серьезно», посвященную 100-летию со Дня рождения Юрия Никулина!
В программе- дрессированные лошади, медведи, козы, бразильское колесо смелости,мото-шар,
эквилибристы на канате, акробаты на мачте, воздушные гимнасты, жонглеры и клоуны! Спешите!"
            };
            return View(new List<ThirdPartyEvent> { circusEvent });
        }

        private async Task<string> UploadSampleImage()
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/"), "poster_circus.png");
            using (var memoryStream = new MemoryStream())
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
                return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}