using Structured_Programming.Filters;
using Structured_Programming.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList.Mvc;
using PagedList;
using WebMatrix.WebData;

namespace Structured_Programming.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page, int typeId = 0)
        {
            DataEntities db = new DataEntities();
            var pageNumber = page ?? 1;
            var itemsPerPage = 9;
            var items = db.Items.OrderByDescending(i => i.ItemId);
            if (typeId != 0)
            {
                var selectedType = db.Types.Find(typeId);
                if (selectedType != null)
                {
                    items = db.Items.Where(i => i.TypeId == typeId).OrderByDescending(i => i.ItemId);
                }
                else
                {
                    return View("Error");
                }
            }
            var typeList = new SelectList(db.Types, "TypeId", "Name");
            var itemModel = new HomeIndexModel
            {
                TypeId = typeId,
                Items = items.ToPagedList(pageNumber, itemsPerPage),
                LastestItems = items.Take(5)
            };
            return View(itemModel);
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
        public ActionResult Error()
        {
            return View();
        }
    }
}
