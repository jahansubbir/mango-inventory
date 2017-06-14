using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MangoInventory.BLL;
using MangoInventory.Models;
using MangoInventory.DBContext;

namespace MangoInventory.Controllers
{
    public class TypeController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        CategoryManager categoryManager=new CategoryManager();

        // GET: /Type/
        public ActionResult Index()
        {
            var types = db.Types.Include(d => d.Category).Include(d => d.SubCategory);
            return View(types.ToList());
        }

        // GET: /Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType devicetype = db.Types.Find(id);
            if (devicetype == null)
            {
                return HttpNotFound();
            }
            return View(devicetype);
        }

        // GET: /Type/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name");
            return View();
        }

        // POST: /Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CategoryId,SubCategoryId,Name")] DeviceType devicetype)
        {
            if (ModelState.IsValid)
            {
                categoryManager.CreateType(devicetype);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", devicetype.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", devicetype.SubCategoryId);
            return View(devicetype);
        }

        // GET: /Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType devicetype = db.Types.Find(id);
            if (devicetype == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", devicetype.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", devicetype.SubCategoryId);
            return View(devicetype);
        }

        // POST: /Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CategoryId,SubCategoryId,Name")] DeviceType devicetype)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(devicetype).State = EntityState.Modified;
                //db.SaveChanges();
                categoryManager.EditType(devicetype);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", devicetype.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", devicetype.SubCategoryId);
            return View(devicetype);
        }

        // GET: /Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType devicetype = db.Types.Find(id);
            if (devicetype == null)
            {
                return HttpNotFound();
            }
            return View(devicetype);
        }

        // POST: /Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceType devicetype = db.Types.Find(id);
            db.Types.Remove(devicetype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
