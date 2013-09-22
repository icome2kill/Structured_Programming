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
        // Simple search result: Search for item name only
        // GET: /Search/s={parameter}
        public ActionResult Index(int? page, SearchOptionsModel model)
        {
            int pageNumber = page ?? 1;
            if (model.TypeList == null)
            {
                model.TypeList = new SelectList(db.Types, "TypeId", "Name");
            }
            var vm = new SearchResultModel()
            {
                Items = ItemsManagement.ItemSearch(model).ToPagedList(pageNumber, 3),
                CurrentOptions = model
            };
            return this.View(vm);
        }
    }
}
