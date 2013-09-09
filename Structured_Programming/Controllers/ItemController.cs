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
using WebMatrix.WebData;

namespace Structured_Programming.Controllers
{
    [InitializeSimpleMembership]
    public class ItemController : Controller
    {
        DataEntities db = new DataEntities();
        //
        // GET: /Item/

        public ActionResult Index()
        {
            var itemModel = new ItemIndexModel
            {
                Items = this.db.Items.OrderByDescending(m => m.ItemId)
            };
            return View(itemModel);
        }
        [Authorize]
        public ActionResult Add()
        {
            var vm = new ItemAddModel
            {
                TypeList = new SelectList(db.Types, "TypeId", "Name")
            };
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ItemAddModel model)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(new Item
                {
                    Name = model.ItemName,
                    Description = model.ItemDescription,
                    Price = model.ItemPrice,
                    TypeId = model.ItemTypeId,
                    UserId = WebSecurity.CurrentUserId
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Item");
            }
            else
            {

            }
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var vm = this.db.Items.Find(id);
            if (vm == null)
            {
                return this.HttpNotFound();
            }
            return View(vm);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var item = this.db.Items.Find(id);
            if (item.UserId != WebSecurity.CurrentUserId)
            {
                return RedirectToAction("Index", "Item");
            }
            return this.View(new ItemEditModel
            {
                Item = item,
                TypeList = new SelectList(db.Types, "TypeId", "Name")
            });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(ItemEditModel model)
        {
            //Cannot edit other user's items
            var isValid = (from c in db.Items
                                    where c.UserId == WebSecurity.CurrentUserId
                                    select c).Any(c => c.ItemId == model.Item.ItemId);
            if (model.Item.UserId != WebSecurity.CurrentUserId || !isValid)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Items.Attach(model.Item);
                var entry = db.Entry(model.Item);
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Item");
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var item = db.Items.Find(id);
            if (item == null || item.UserId != WebSecurity.CurrentUserId)
            {
                return HttpNotFound();
            }
            if (Request.HttpMethod == "GET")
            {
                return View(item);
            }
            if (Request.HttpMethod == "POST")
            {
                var isValid = (from c in db.Items
                               where c.UserId == WebSecurity.CurrentUserId
                               select c).Any(c => c.ItemId == id);
                if (!isValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                db.Items.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }
    }
}