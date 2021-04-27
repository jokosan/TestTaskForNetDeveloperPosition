using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestTaskForNetDeveloperPosition.Bll;
using TestTaskForNetDeveloperPosition.Bll.Contract;

namespace TestTaskForNetDeveloperPosition.Controllers
{
    // В начале идея была определить карту сайта через robots.txt к примеру: https://docs.microsoft.com/robots.txt 
    // и уже в данном файле найти Sitemap.xml 
    // не немного времени прогулки по сайтам пришел к выводу что не все сайты указывают ссылку на Sitemap примером был этот сайт https://elmir.ua/robots.txt
    // а если я не смогу определить карту сайта то не чего не смогу ввыести пользователю на его запрос
    // тестовое задание очен понравилось ! Спасибо! 
    // Если не затруднит сможете сделать отзыв по коду и прислать на почту vetalvasilenko@gmail.com

    public class HomeController : Controller
    {
        private readonly ILoadingSiteAddresses _loadingSite;

        public HomeController(
             ILoadingSiteAddresses loadingSite)
        {
            _loadingSite = loadingSite;
        }

        public ActionResult Index()
            => View();

        [HttpGet]
        public ActionResult UrlPages(string url, int? numberOfLinks)
        {
            if (_loadingSite.ValidationAddresses(url) && numberOfLinks != null)
            {
                int idLink = _loadingSite.SaveUserRequest(url);
                _loadingSite.Loading(url, numberOfLinks.Value, idLink);

                return RedirectToAction("UserQueryResult", "Home", new RouteValueDictionary(new
                {
                    id = idLink
                }));
            }
            else
            {
                ViewBag.Eror = "wrong address";
                return RedirectToAction("Index");
            }
        }

        public ActionResult UserQueryResult(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View(_loadingSite.GetSitemaps(id.Value));
        }



        public ActionResult ArxivRequest()
            => View(_loadingSite.Arxiv());

        public ActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;

            if (date == null)
            {
                var result = _loadingSite.Arxiv(id.Value);
                ViewBag.DateGroup = result.GroupBy(g =>
                    g.PageTestDate.Value.Date)
                    .ToDictionary(x => x.Key);

                return View(result);
            }
            else
            {
                return View(_loadingSite.Arxiv(id.Value, date.Value));
            }
        }


    }
}