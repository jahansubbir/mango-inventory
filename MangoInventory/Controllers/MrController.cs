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
using Microsoft.Ajax.Utilities;
using EntityState = System.Data.Entity.EntityState;

namespace MangoInventory.Controllers
{
    public class MrController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        MrManager mrManager=new MrManager();
        //private List<MrViewModel> mrList;
        private List<MR> mrs;
        public List<MrView> mrList;
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;
        private int loggedIn;
        private string userRole;

        public MrController()
        {
            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                loggedIn = Convert.ToInt32(cookie["loginStatus"]);
                userRole = cookie["role"];
            }

        }

        // GET: /Mr/
        public void Message()
        {
            TempData["message"] = "Please Login to continue";
        }
        public ActionResult Index()
        {
            ViewBag.Company = db.Companies.FirstOrDefault();
            
            List<MrView> mrs = db.MrView.ToList();

            return View(mrs);
        }

        // GET: /Mr/Details/5
        public ActionResult Details(string mrNo)
        {
            if (mrNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mrs = db.MrView.ToList();
            ViewBag.Mr = mrs.Find(a => a.MRNo == mrNo);
            /*if (mr == null)
            {
                return HttpNotFound();
            }
            return View(mr);*/
            mrs = mrs.Where(a => a.MRNo == mrNo).ToList();
            if (mrs.Contains(null))
            {
                return HttpNotFound();
            }
            return View(mrs);
        }

        void DropdownLists()
        {
              ViewBag.ProductName = new SelectList(db.Products.DistinctBy(a=>a.Name), "Name", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Model");
        }

        void DropdownLists(MR mr)
        {
            ViewBag.ProductName = new SelectList(db.Products.DistinctBy(a=>a.Name), "Name", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Model",mr.ProductId);
        }
        // GET: /Mr/Create
        public ActionResult Create()
        {
            if (loggedIn!=0)
            {
                DropdownLists();
                return View();
            }
            else
            {
                Message();//["message"] = "Please Login to continue";
                return RedirectToAction("Index", "Home");
            }
         
        }

        // POST: /Mr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ReceiveDate,Supplier,ProductId,Quantity,UnitPrice,Status")] MR mr)
        {
           
            //if (ModelState.IsValid)
            //{
                try
                {
                    // TODO: Add insert logic here
                   // ViewBag.Products = new SelectList(productManager.GetProducts(), "Id", "Model");
                    //ViewBag.ProductList = productManager.GetProducts();
                    string entryNo = mrManager.GetMrNo(mr.Supplier, mr.ReceiveDate);
                    mr.MRNo = "MR-" + mr.ReceiveDate.ToString("yy-MM-dd") + mr.Supplier.Substring(0, 3) + entryNo;
                    mrs = (List<MR>)Session["createMr"];
                    mrList = (List<MrView>)Session["mr"];

                    ViewBag.Message = mrManager.CreateMR(mrs);


                    ViewBag.Mrs = mrManager.GetMrs(mr.MRNo);
                    ViewBag.MrObject = mrManager.GetMrs(mr.MRNo).Find(a => a.MRNo == mr.MRNo);
                    if (mr.Status == 0)
                    {
                        DropdownLists();
                        //ViewBag.ProductId = new SelectList(db.Products, "Id", "Model");
                        return View();
                    }
                    else
                    {
                        Session["createMr"] = null;
                        Session["mr"] = null;
                       DropdownLists(mr); //ViewBag.ProductId = new SelectList(db.Products, "Id", "Model");
                        return RedirectToAction("Details", new { mrNo = mr.MRNo });
                    }

                    //return RedirectToAction("Index", new { mrNo = mr.MRNo });
                    //return View();
                }
                catch (Exception e)  
              {
                 DropdownLists(mr);// ViewBag.ProductId = new SelectList(db.Products, "Id", "Model");
                    ViewBag.Message = e.Message;
                    return View();
                }
            

            //ViewBag.ProductId = new SelectList(db.Products, "Id", "Model", mr.ProductId);
            //return View(mr);
        }

        // GET: /Mr/Edit/5
        public ActionResult Edit(int? id)
        {
            if (loggedIn!=0)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MR mr = db.Mrs.Find(id);
                if (mr == null)
                {
                    return HttpNotFound();
                }
                DropdownLists(mr);// ViewBag.ProductId = new SelectList(db.Products, "Id", "Model", mr.ProductId);
                return View(mr);
            }
            Message();
            return RedirectToAction("Index", "Home");

        }

        // POST: /Mr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ReceiveDate,MRNo,Supplier,ProductId,Quantity,UnitPrice,Status")] MR mr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            DropdownLists(mr);//ViewBag.ProductId = new SelectList(db.Products, "Id", "Model", mr.ProductId);
            return View(mr);
        }

        // GET: /Mr/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MR mr = db.Mrs.Find(id);
            if (mr == null)
            {
                return HttpNotFound();
            }
            return View(mr);
        }

        // POST: /Mr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MR mr = db.Mrs.Find(id);
            db.Mrs.Remove(mr);
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
        //public JsonResult Receive(MR mr)
        //{
        //    
        //    if (Session["mr"] == null)
        //    {
        //        mrList = new List<MrViewModel>();
        //    }
        //    else
        //    {
        //        mrList = (List<MrViewModel>)Session["mr"];
        //    }
        //    mrList.Add(mrManager.SessionReceive(mr));
        //    Session["mr"] = mrList;
        //    var receive = (List<MrViewModel>)Session["mr"];
        //    return Json(receive, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult Receive(MR mr)
        {
            string entryNo = mrManager.GetMrNo(mr.Supplier, mr.ReceiveDate);
            mr.MRNo = "MR-" + mr.ReceiveDate.ToString("yy-MM-dd") + mr.Supplier.Substring(0, 3) + entryNo;
             if (Session["createMr"] == null)
            {
                mrs = new List<MR>();
            }
            else
            {
                mrs = (List<MR>)Session["createMr"];
            }
            mrs.Add(mr);
            Session["createMr"] = mrs;
           if (Session["mr"] == null)
           {
               mrList = new List<MrView>();
           }
           else
           {
               mrList = (List<MrView>)Session["mr"];
           }
           mrList.Add(mrManager.SessionReceive(mr));
           Session["mr"] = mrList;
           var receive = (List<MrView>)Session["mr"];
           return Json(receive, JsonRequestBehavior.AllowGet);
        }
    }
}
