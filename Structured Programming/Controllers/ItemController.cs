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
using PagedList;
using WebMatrix.WebData;

namespace Structured_Programming.Controllers
{
    [InitializeSimpleMembership]
    public class ItemController : BaseController
    {
        DataEntities db = new DataEntities();
        //
        // GET: /Item/

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
            var itemsToDisplay = items.ToPagedList(pageNumber, itemsPerPage);
            var itemModel = new ItemIndexModel
            {
                TypeName = db.Types.Find(typeId) != null ? db.Types.Find(typeId).Name : "All",
                Items = itemsToDisplay
            };
            return View(itemModel);
        }
        [Authorize]
        public ActionResult Add()
        {
            var vm = new ItemFormModel
            {
                TypeList = new SelectList(db.Types, "TypeId", "Name"),
                Item = new Item()
            };
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ItemFormModel model)
        {
            if (ModelState.IsValid)
            {
                model.Item.UserId = WebSecurity.CurrentUserId;
                db.Items.Add(model.Item);
                db.SaveChanges();
                // Save the file as {ItemId}.{extension of the file uploaded}
                if (model.Image != null)
                {
                    // Delete old images
                    string[] oldImages = System.IO.Directory.GetFiles(Server.MapPath("/Images/"), model.Item.ItemId + ".*");
                    foreach (var oldimage in oldImages) {
                        System.IO.File.Delete(oldimage);
                    }
                    string extension = System.IO.Path.GetExtension(model.Image.FileName);
                    model.Image.SaveAs(Server.MapPath("/Images/") + model.Item.ItemId.ToString() + extension);
                }
                ViewBag.Message = "Your item has successfully been added";
                ViewBag.ReturnUrl = Url.Action("Index");
                return View("Success");
            }
            model.TypeList = new SelectList(db.Types, "TypeId", "Name");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var vm = this.db.Items.Find(id);
            if (vm == null)
            {
                return View("Error");
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
            return this.View(new ItemFormModel
            {
                Item = item,
                TypeList = new SelectList(db.Types, "TypeId", "Name")
            });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(ItemFormModel model)
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
                // Save the file as {ItemId}.{extension of the file uploaded}
                if (model.Image != null)
                {
                    // Delete old images
                    string[] oldImages = System.IO.Directory.GetFiles(Server.MapPath("/Images/"), model.Item.ItemId + ".*");
                    foreach (var oldimage in oldImages)
                    {
                        System.IO.File.Delete(oldimage);
                    }
                    string extension = System.IO.Path.GetExtension(model.Image.FileName);
                    model.Image.SaveAs(Server.MapPath("/Images/") + model.Item.ItemId.ToString() + extension);
                }
                ViewBag.Message = "Your item has been edited successfully";
                ViewBag.ReturnUrl = Url.Action("Index");
                return View("Success");
            }
            model.TypeList = new SelectList(db.Types, "TypeId", "Name");
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
                    return View("Error");
                }
                db.Items.Remove(item);
                db.SaveChanges();
                ViewBag.Message = "The item has successfully been removed";
                ViewBag.ReturnUrl = Url.Action("Index");
                return View("Success");
            }
            return View(item);
        }
    }
}