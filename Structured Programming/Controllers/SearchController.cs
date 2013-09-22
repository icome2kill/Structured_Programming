using PagedList;
using PagedList.Mvc;
using Structured_Programming.Models;
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

            string s = model.Query ?? "";
            // Flag to see if none of the search options ticked, default to search for Item name.
            bool flag = false;
            var pageNumber = page ?? 1;

            // List of items to display on result page
            List<Item> itemsToDisplay = new List<Item>();

            // All items is queried first to minimize request to database
            var allItems = db.Items.ToList();

            if (model.InclName == true)
            {
                flag = true;
                itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.Name.Contains(s))).ToList();
            }
            if (model.InclDescription == true)
            {
                flag = true;
                itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.Description.Contains(s))).ToList();
            }
            if (model.InclTrade == true)
            {
                flag = true;
                itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => (row.Trade != null) ? row.Trade.Contains(s) : false)).ToList();
            }
            if (model.InclUser == true)
            {
                flag = true;
                itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.UserProfile.UserName.Contains(s))).ToList();
            }
            if (model.TypeId != 0)
            {
                itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.TypeId == model.TypeId)).ToList();
            }
            if (flag == false)
            {
                flag = true;
                itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.Name.Contains(s))).ToList();
            }
            if (model.TypeList == null)
            {
                model.TypeList = new SelectList(db.Types, "TypeId", "Name");
            }
            var vm = new SearchResultModel()
            {
                Items = itemsToDisplay.ToPagedList(pageNumber, 3),
                CurrentOptions = model
            };
            return View(vm);
        }
    }
}
