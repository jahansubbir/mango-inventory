using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MangoInventory.BLL;
using MangoInventory.Models;
using MangoInventory.DBContext;
using Newtonsoft.Json;
using EntityState = System.Data.Entity.EntityState;

namespace MangoInventory.Controllers
{
    public class ProductController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        ProductManager productManager=new ProductManager();

        // GET: /Product/
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Type).Include(p => p.Unit);
            return View(products.ToList());
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name");
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CategoryId,SubCategoryId,TypeId,Name,Model,Brand,UnitId,Description,File,ImagePath")] Product product)
        {
            string path = null;
            if (product.File!=null)
            {
                
                string pic = product.Model;/*fileName.ToString();*/ /*System.IO.Path.GetFileName(company.Photo.FileName);*/
                string ext = System.IO.Path.GetExtension(product.File.FileName);
                path = Server.MapPath("~/Images/" + pic + ext);
                
                product.ImagePath = pic + ext;
                
            }
            if (product.Model==null)
            {
                product.Model = product.Name;
            }
            if (ModelState.IsValid)
            {
               string message= productManager.SaveProduct(product);
                if (message == "Product has been Saved to Database" && product.File != null)
                    if (path != null) product.File.SaveAs(path);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", product.SubCategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", product.TypeId);
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name", product.UnitId);
            return View(product);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", product.SubCategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", product.TypeId);
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name", product.UnitId);
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CategoryId,SubCategoryId,TypeId,Name,Model,Brand,UnitId,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                productManager.EditProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", product.SubCategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", product.TypeId);
            ViewBag.UnitId = new SelectList(db.Units, "Id", "Name", product.UnitId);
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public JsonResult GetTypesBySubCategoryId(int subId)
        {
            var types = db.Types.Where(a => a.SubCategoryId == subId).ToList();
            return Json(types, JsonRequestBehavior.AllowGet);
        }
     
        public JsonResult GetProductByTypeId(int tId)
        {
            db.Configuration.ProxyCreationEnabled=false;
            var products = db.Products.Where(a => a.TypeId == tId).ToList();

            ////var obj = JsonConvert.SerializeObject(products, Formatting.Indented,
            ////                  new JsonSerializerSettings()
            ////                  {
            ////                      ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            ////                  });
            //var obj = new JavaScriptSerializer().Serialize(products);
            
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductByProductName(string pName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Product> products= db.Products.Where(a=>a.Name==pName).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductDetailsById(int pId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var prodcuts = productManager.GetProducts();
            var product = prodcuts.Find(a => a.Id == pId);
           var productList = JsonConvert.SerializeObject(product);
            return Json(productList, JsonRequestBehavior.AllowGet);
        }
    }
}
