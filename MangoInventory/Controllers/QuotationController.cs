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
       
        public ActionResult Search(string quotationId)
        {
            var quotationViewModel = db.QuotationView.ToList();
            quotationViewModel = quotationViewModel.Where(q => q.QuotationId == quotationId).ToList();
            ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == quotationId);
            return View(quotationViewModel);
        }

        //public ActionResult Search(string quotationId)
        //{
        //    var quotationViewModel = db.QuotationView.ToList();
        //    quotationViewModel = quotationViewModel.Where(q => q.QuotationId == quotationId).ToList();
        //    ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == quotationId);
        //    return View(quotationViewModel);
        //}

        //[HttpPost]
        //public ActionResult Search(Quotation quotation)
        //{
        //    var message = quotationManager.Edit(quotation);
        //    var quotationViewModel = db.QuotationView.ToList();
        //    quotationViewModel = quotationViewModel.Where(q => q.QuotationId == quotation.QuotationId).ToList();
        //    ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == quotation.QuotationId);
        //    return View(quotationViewModel);

        //}
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
        public ActionResult Edit([Bind(Include="Id,ContactPerson,CompanyName,Address,Date,CategoryId,SubCategoryId,TypeId,ProductId,Quantity,UnitPrice,EmployeeId,QuotationId,Status")] Quotation quotation)
        {
            //if (ModelState.IsValid)
            //{
            /* ViewBag.Message*/
             Quotation q= quotationManager.Edit(quotation);

            List<QuotationView> quotations =db.QuotationView.Where(a => a.QuotationId == q.QuotationId).ToList();
                ViewBag.Products = db.Products.ToList();
                DropDownLists();
                ViewBag.Quotation = quotations.Find(a => a.QuotationId == q.QuotationId);
            return View(quotations);
            //}
            //DropDownLists(quotation);
            //return View(quotation);
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
        public ActionResult Index()
        {
            var quotation = db.Quotations.ToList();
            quotation = quotation.DistinctBy(a => a.QuotationId).OrderByDescending(a => a.Date).ThenBy(a => a.CompanyName).ToList();
            return View(quotation);
        }

        //public ActionResult Index()
        //{
        //    var quotation = db.Quotations.ToList();
        //    quotation = quotation.DistinctBy(a => a.QuotationId).OrderByDescending(a=>a.Date).ThenBy(a=>a.CompanyName).ToList();
        //    return View(quotation);
        //}

        ////public ActionResult PrintQuotation()
        ////{
        ////    return View();
        ////}

        ////[WordDocument]
        //public ActionResult PrintQuotation(string quotationId)
        //{
        //    //HttpCookie cookie = context.Request.Cookies["loginCookie"];
        //    //quotationId = (string)cookie["QuotationId"];
        //    var quotationViewModel = db.QuotationView.ToList();
        //    quotationViewModel = quotationViewModel.Where(q => q.QuotationId == quotationId).ToList();
        //    ViewBag.Model = quotationViewModel;
        //    ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == quotationId);

        //   return View(quotationViewModel);
        //   // return new ActionAsPdf("PrintQuotation", quotationViewModel);

        //}

        //public ActionResult Print(string quotationId)
        //{
        //    return new ActionAsPdf("PrintQuotation", new { quotationId=quotationId })
        //    {
        //        FileName = quotationId+".pdf"/*Server.MapPath("~/Rotativa/Downloads/"+quotationId+".pdf")*/
        //    };

        //}

//        //[HttpPost]
//        //[WordDocument]
//        public ActionResult Print(string quotationId)
//        {
//            cookie["QuotationId"] = quotationId; //var word = new Microsoft.Office.Interop.Word.Application();
//            //word.Visible = false;
//            Response.Cookies.Add(cookie);
//            var quotationViewModel = db.QuotationView.ToList();
//            quotationViewModel = quotationViewModel.Where(q => q.QuotationId == quotationId).ToList();
//            ViewBag.Model = quotationViewModel;
//            ViewBag.Quotation = quotationViewModel.Find(a => a.QuotationId == quotationId);
//            //var filePath = Server.MapPath("~/Quotation/Search/PrintQuotation.cshtml");
//            //var savePathdoc = Server.MapPath("~/Report/"+quotationId+".docx");
//            //var wordDoc = word.Documents.Open(FileName: filePath, ReadOnly: false);
//            //wordDoc.SaveAs2(FileName: savePathdoc, FileFormat: WdSaveFormat.wdFormatXMLDocument);
//            //return View();

//            ViewBag.WordDocumentFilename = "Print";
//            return new ActionAsPdf("PrintQuotation",quotationViewModel);
//}

        //[HttpPost]
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
