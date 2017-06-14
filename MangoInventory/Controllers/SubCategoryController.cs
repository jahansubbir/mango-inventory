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
using EntityState = System.Data.Entity.EntityState;

namespace MangoInventory.Controllers
{
    public class SubCategoryController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        CategoryManager categoryManager=new CategoryManager();

        // GET: /SubCategory/
        public ActionResult Index()
        {
            var subcategories = db.SubCategories.Include(s => s.Category);
            return View(subcategories.ToList());
        }

        // GET: /SubCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = db.SubCategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: /SubCategory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: /SubCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CategoryId,Name")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
               ViewBag.Message= categoryManager.CreateSubcategory(subcategory);
               if (ViewBag.Message == subcategory.Name + " has been created successfully")
                {
                    return RedirectToAction("Index");
                }
                return View();

            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subcategory.CategoryId);
            return View(subcategory);
        }

        // GET: /SubCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = db.SubCategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subcategory.CategoryId);
            return View(subcategory);
        }

        // POST: /SubCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CategoryId,Name")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(subcategory).State = EntityState.Modified;
                //db.SaveChanges();
                ViewBag.Message=categoryManager.EditSubCategory(subcategory);
                if (ViewBag.Message=="Edited")
                {
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subcategory.CategoryId);
                return View(subcategory);
               
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", subcategory.CategoryId);
            return View(subcategory);
        }

        // GET: /SubCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = db.SubCategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: /SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subcategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subcategory);
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

        public JsonResult GetSubCategoryByCategoryId(int catId)
        {
            var subcategories = db.SubCategories.Where(a => a.CategoryId == catId);
            return Json(subcategories, JsonRequestBehavior.AllowGet);
        }
    }
}
