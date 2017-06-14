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
using MangoInventory.Models;
using MangoInventory.DBContext;
using EntityState = System.Data.Entity.EntityState;

namespace MangoInventory.Controllers
{
    public class StockController : Controller
    {
        private MangoDbContext db = new MangoDbContext();

        // GET: /Stock/
        public ActionResult Index()
        {
            //var stock = db.StockView.ToList();
            //return View(stock);
            //return View(db.Stocks.Include(a=>a.Product).ToList());
            try
            {
                List<StockView> stock = db.StockView.ToList();
                return View(stock);
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException e)
            {
                ViewBag.Message = e.Message;
                return View();
                throw;
            }
            
        }

        // GET: /Stock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: /Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProductId,MovementDate,Movement,Debit,Credit,Balance,Remarks")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: /Stock/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProductId,MovementDate,Movement,Debit,Credit,Balance,Remarks")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: /Stock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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

        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Report(DateTime From, DateTime To)
        {
            var stock = db.StockView.ToList();
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Report"), "ReportStock.rpt"));
            reportDocument.SetDataSource(stock);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.CrystalReport);
            // Stream stream1 = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "" + "stock-report-" + DateTime.Today.ToString("yy-MM-dd") + ".rpt");
        }
    }
}
