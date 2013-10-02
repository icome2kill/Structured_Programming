using Structured_Programming.Filters;
using Structured_Programming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using PagedList;
using PagedList.Mvc;

namespace Structured_Programming.Controllers
{
    [InitializeSimpleMembership]
    [Authorize]
    public class TransactionController : Controller
    {
        DataEntities db = new DataEntities();
        //
        // GET: /Transaction/

        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int userId = WebSecurity.CurrentUserId;

            var transactions = db.Transactions.Where(m => m.BuyerId == userId || m.Item.UserId == userId).OrderByDescending(m => m.DateModified).ToPagedList(pageNumber, 20);
            return View(new TransactionIndexModel()
            {
                Transactions = transactions
            });
        }

        // itemId represent id of item to be bought
        // GET: /Transaction/Create?{itemId}
        public ActionResult Create(int itemId)
        {
            var item = db.Items.Find(itemId);
            var methodList = new SelectList(db.Methods, "MethodId", "Name");
            var transaction = new Transaction();
            var myItems = db.Items.Where(m => m.UserId == WebSecurity.CurrentUserId);

            var vm = new TransactionCreateModel()
            {
                Item = item,
                MyItems = myItems,
                MethodList = methodList,
                Transaction = transaction
            };

            return View(vm);
        }
        // POST
        [HttpPost]
        public ActionResult Create(TransactionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Transaction.BuyerId = WebSecurity.CurrentUserId;
                model.Transaction.ItemId = model.Item.ItemId;
                model.Transaction.StatusId = 1;
                model.Transaction.DateCreated = DateTime.Now;
                model.Transaction.DateModified = DateTime.Now;

                db.Transactions.Add(model.Transaction);
                db.SaveChanges();

                ViewBag.ReturnUrl = Url.Action("Index", "Transaction");
                return View("Success");
            }
            var item = db.Items.Find(model.Item.ItemId);
            model.MethodList = new SelectList(db.Methods, "MethodId", "Name");
            return View(model);
        }
    }
}
