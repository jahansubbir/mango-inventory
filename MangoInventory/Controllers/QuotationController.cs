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
    public class QuotationController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        QuotationManager quotationManager=new QuotationManager();

        // GET: /Quotation/
        public ActionResult Index()
        {
            var quotations = db.Quotations.Include(q => q.Category).Include(q => q.DeviceType).Include(q => q.Product).Include(q => q.SubCategory);
            return View(quotations.ToList());
        }

        // GET: /Quotation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // GET: /Quotation/Create
        public ActionResult Create()
        {
           DropDownLists();
            return View();
        }

        // POST: /Quotation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ContactPerson,CompanyName,Address,Date,CategoryId,SubCategoryId,TypeId,ProductId,Quantity,UnitPrice,EmployeeId,QuotationId,Status")] Quotation quotation)
        {
            try
            {
                quotationManager.Create(quotation);
                if (ModelState.IsValid)
                {

                    return RedirectToAction("Index");
                }

               DropDownLists(quotation);
                return View(quotation);
            }
            catch (Exception exception)
            {
               
                ViewBag.Message = exception.Message;
                return View(quotation);
                throw;
            }
        }

       
        // GET: /Quotation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
           DropDownLists(quotation);
            return View(quotation);
        }

        // POST: /Quotation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ContactPerson,CompanyName,Address,Date,CategoryId,SubCategoryId,TypeId,ProductId,Quantity,UnitPrice,EmployeeId,QuotationId,Status")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message=quotationManager.Edit(quotation);
                return RedirectToAction("Index");
            }
            DropDownLists(quotation);
            return View(quotation);
        }

        // GET: /Quotation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: /Quotation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            db.Quotations.Remove(quotation);
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

        void DropDownLists()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name");
        }
        private void DropDownLists(Quotation quotation)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", quotation.CategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", quotation.TypeId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", quotation.ProductId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "Name", quotation.SubCategoryId);
        }

        public JsonResult Quote(Quotation quotation)
        {
            quotation.QuotationId = quotationManager.GetQuotationId(quotation);
            //quotation.QuotationId = "Q-"+quotation.Category.Name.Substring()
            //if (Session["createMr"] == null)
            //{
            //    mrs = new List<MR>();
            //}
            //else
            //{
            //    mrs = (List<MR>)Session["createMr"];
            //}
            //mrs.Add(mr);
            //Session["createMr"] = mrs;
            //if (Session["mr"] == null)
            //{
            //    mrList = new List<MrView>();
            //}
            //else
            //{
            //    mrList = (List<MrView>)Session["mr"];
            //}
            //mrList.Add(mrManager.SessionReceive(mr));
            //Session["mr"] = mrList;
            var receive = (List<MrView>)Session["mr"];
            return Json(receive, JsonRequestBehavior.AllowGet);
        }
       
    }
}
