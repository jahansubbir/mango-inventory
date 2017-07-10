using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using MangoInventory.BLL;
using MangoInventory.Models;
using MangoInventory.DBContext;
using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Word;
using Rotativa;
using EntityState = System.Data.Entity.EntityState;
//using System = Microsoft.Office.Interop.Word.System;

namespace MangoInventory.Controllers
{
    public class QuotationController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        QuotationManager quotationManager=new QuotationManager();
        private List<Quotation> quotations;
        private List<QuotationView> quotationList;
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;
        private int eId;
       
        public QuotationController()
        {
            cookie = context.Request.Cookies["loginCookie"];
            if (cookie!=null)
            {
                eId = Convert.ToInt32(cookie["eId"]);
            }
        }

        public ActionResult Index()
        {
            //int id = 2;
            var quotation = db.Quotations.ToList();
            

            quotation = quotation.DistinctBy(a => a.QuotationId).OrderByDescending(a => a.Date).ThenBy(a => a.CompanyName).ToList();
            return View(quotation);
        }

        public ActionResult WorkOrders()
        {
            var workOrders = db.WorkOrders.ToList();
            workOrders = workOrders.DistinctBy(a => a.WorkOrderNo).OrderByDescending(a => a.QuotationId).ThenBy(a => a.WorkOrderNo).ToList();
            return View(workOrders);
        }
        public ActionResult Search(string quotationId)
        {
            var quotationViewModel = db.QuotationView.ToList();
            quotationViewModel = quotationViewModel.Where(q => q.QuotationId == quotationId).ToList();
            ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == quotationId);
            return View(quotationViewModel);
        }

        [HttpPost]
        public ActionResult Search(WorkOrder work)
        {
            work.WorkOrderNo = "W-" + work.QuotationId;
            ViewBag.Message =quotationManager.OrderWork(work);

            if (ViewBag.Message != "Internal Work Order")
            {
                var quotationViewModel = db.QuotationView.ToList();
                quotationViewModel = quotationViewModel.Where(q => q.QuotationId == work.QuotationId).ToList();
                ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == work.QuotationId);
                return View(quotationViewModel);
            }
            return RedirectToAction("WorkOrder",new {id=work.QuotationId});
            // return RedirectToAction("WorkOrder", new { id = work.WorkOrderNo });
        }

        public ActionResult WorkOrder(string id)
        {
            var workOrders = quotationManager.GeWorkOrderViews();
            workOrders = workOrders.Where(a => a.QuotationId == id).ToList();
            ViewBag.WorkOrder = workOrders.Find(a => a.QuotationId == id);
            return View(workOrders);
        }

        
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
                quotation.QuotationId = quotationManager.GetQuotationId(quotation);
                List<Quotation> quotations = (List<Quotation>) Session["quotations"];
                quotationManager.Create(quotations);
                if (quotation.Status==1)
                {

                    return RedirectToAction("Search",new{quotationId=quotation.QuotationId});
                }

               DropDownLists(quotation);
                return View(quotation);
            }
            catch (Exception exception)
            {
               DropDownLists(quotation);
                ViewBag.Message = exception.Message;
                return View(quotation);
                throw;
            }
        }

       
        // GET: /Quotation/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<QuotationView> quotations = db.QuotationView.Where(a => a.QuotationId == id).ToList();
            if (quotations.Count==0)
            {
                return HttpNotFound();
            }
            ViewBag.Products = db.Products.ToList();
           DropDownLists();
            ViewBag.Quotation = quotations.Find(a => a.QuotationId == id);
            return View(quotations);
        }

        // POST: /Quotation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ContactPerson,CompanyName,Address,Date,CategoryId,SubCategoryId,TypeId,ProductId,Quantity,UnitPrice,EmployeeId,QuotationId,Status")] Quotation qu)
        {
            
            Quotation quotation = db.Quotations.Find(qu.Id);
            if (qu.ContactPerson==null)
            {
                qu.ContactPerson = quotation.ContactPerson;
            }
            if (qu.CompanyName == null)
            {
                qu.CompanyName = quotation.CompanyName;
            }
            if (qu.Address == null)
            {
                qu.Address = quotation.Address;
            }
            if (qu.Date==DateTime.MinValue)
            {
                qu.Date = quotation.Date;
            }
           
            if (qu.Quantity == 0)
            {
                qu.Quantity = quotation.Quantity;
            }
            if (qu.UnitPrice == 0)
            {
                qu.UnitPrice = quotation.UnitPrice;
            }
           
            //qu.QuotationId = quotationManager.GetQuotationId(qu);
            var message = quotationManager.Edit(qu);
            var quotationViewModel = db.QuotationView.ToList();
            quotationViewModel = quotationViewModel.Where(q => q.QuotationId == qu.QuotationId).ToList();
            ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == qu.QuotationId);
            return View(quotationViewModel);
        }

        public PartialViewResult /*ActionResult*/ ViewQuotation(Quotation qu)
        {
            
            Quotation quotation = db.Quotations.Find(qu.Id);
            if (qu.Quantity==0)
            {
                qu.Quantity = quotation.Quantity;
            }
            if (qu.UnitPrice==0)
            {
                qu.UnitPrice = quotation.UnitPrice;
            }
            var message = quotationManager.Edit(qu);
            var quotationViewModel = db.QuotationView.ToList();
            quotationViewModel = quotationViewModel.Where(q => q.QuotationId == qu.QuotationId).ToList();
            ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == qu.QuotationId);
            return PartialView("_ViewQuotationPartial", quotationViewModel);
           // return p
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
            return RedirectToAction("Search");
        }
     

       
        public ActionResult Report(string quotationId,string type)
        {
            var quotation = db.QuotationView.ToList();
            quotation = quotation.Where(a => a.QuotationId == quotationId).ToList();
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Report"), "ReportQuotation.rpt"));
            reportDocument.SetDataSource(quotation);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            if (type.Equals("Word"))
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + quotationId + ".doc");
            }
            if (type.Equals("Excel"))
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + quotationId + ".xls");
            }
            else
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + quotationId + ".pdf");
            }
           
        }
        public ActionResult ReportWorkOrder(string workOrderNo, string type)
        {
            var workOrders = quotationManager.GeWorkOrderViews().ToList();
            workOrders = workOrders.Where(a => a.WorkOrderNo == workOrderNo).ToList();
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Report"), "ReportWorkOrder.rpt"));
            reportDocument.SetDataSource(workOrders);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            if (type.Equals("Word"))
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + workOrderNo + ".doc");
            }
            if (type.Equals("Excel"))
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + workOrderNo + ".xls");
            }
            else
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + workOrderNo + ".pdf");
            }

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
            quotation.EmployeeId = eId;
            quotation.QuotationId = quotationManager.GetQuotationId(quotation);
            // mr.MRNo = "MR-" + mr.ReceiveDate.ToString("yy-MM-dd") + mr.Supplier.Substring(0, 3) + entryNo;
            if (Session["quotations"] == null)
            {
                quotations = new List<Quotation>();
            }
            else
            {
                quotations = (List<Quotation>)Session["quotations"];
            }
            quotations.Add(quotation);
            Session["quotations"] = quotations;
            if (Session["quotationView"] == null)
            {
                quotationList = new List<QuotationView>();
            }
            else
            {
                quotationList = (List<QuotationView>)Session["quotationView"];
            }
            quotationList.Add(quotationManager.SessionReceive(quotation));
            Session["quotationView"] = quotationList;
            var receive = (List<QuotationView>)Session["quotationView"];
            return Json(receive, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditQuotation(Quotation quotation)
        {
            var message = quotationManager.Edit(quotation);
            return Json(message, JsonRequestBehavior.AllowGet);

        }

      
    }
}
