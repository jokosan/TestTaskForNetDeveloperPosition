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
                TempData["listError"] = _loadingSite.Loading(url, numberOfLinks.Value, idLink);

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

            if (TempData.ContainsKey("listError"))
            {
                ViewBag.Error = TempData["listError"] as List<string>;
                TempData.Keep("listError");
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