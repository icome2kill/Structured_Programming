using PagedList;
using PagedList.Mvc;
using Structured_Programming.Models;
using Structured_Programming.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Controllers
{
    public class SearchController : Controller
    {
        DataEntities db = new DataEntities();
        //
        // Search result
        // GET: /Search/s={parameter}
        public ActionResult Index(int? page, SearchOptionsModel model)
        {
            ViewBag.Result = "Search results";
            int pageNumber = page ?? 1;
            var vm = new SearchResultModel();

            if (model.TypeList == null)
            {
                model.TypeList = new SelectList(db.Types, "TypeId", "Name");
            }
            var itemsToDisplay = ItemsManagement.ItemSearch(model).ToPagedList(pageNumber, 9);
            if (itemsToDisplay.Count == 0)
            {
                ViewBag.Result = "No result found.";
            }

            vm.Items = itemsToDisplay;
            vm.CurrentOptions = model;

            return this.View(vm);

        }
    }
}
