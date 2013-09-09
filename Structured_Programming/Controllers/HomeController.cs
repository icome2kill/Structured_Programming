using Structured_Programming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataEntities db = new DataEntities();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var items = (from i in db.Items
                     orderby i.ItemId descending
                     select i).Take(10);
            var users = (from i in db.UserProfiles
                         orderby i.UserId descending
                         select i).Take(10);
            var vm = new HomeIndexModel
            {
                NewestItems = items,
                TopUsers = users
            };
            return View(vm);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
