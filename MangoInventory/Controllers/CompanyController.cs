using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
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
    public class CompanyController : Controller
    {
        private MangoDbContext db = new MangoDbContext();
        CompanyManager companyManager=new CompanyManager();

        // GET: /Company/
        public ActionResult Index()
        {
            var companies = companyManager.GetCompanies();
            return View(companies);
        }

        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Company company = companyManager.GetCompanies().Find(a=>a.Id==id);
            //Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: /Company/CreateCategory
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Company/CreateCategory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Address,ContactNo,Email,Website,Image,Photo")] Company company)
        {
            try
            {
            //    var extension = Path.GetExtension(company.Photo.FileName);
            //    var path = Path.Combine(Server.MapPath("~/Images"));
            //    if (!Directory.Exists(path))
            //        Directory.CreateDirectory(path);
            //    Guid fileName = Guid.NewGuid();
            //   company.Photo.SaveAs(path + fileName + extension);
            //    Debug.Assert(extension != null, "extension != null");
            //    company.Image = fileName+extension.ToString();
                if (company.Photo!=null)
                {
                   // Guid fileName=Guid.NewGuid();
                    string pic = "companylogo";/*fileName.ToString();*/ /*System.IO.Path.GetFileName(company.Photo.FileName);*/
                    string ext = System.IO.Path.GetExtension(company.Photo.FileName);
                    string path = Server.MapPath("~/Images/"+pic+ext);

                    // file is uploaded
                    company.Photo.SaveAs(path);
                    company.Image = pic+ext;
                }

                if (ModelState.IsValid)
                {

                    //var extension = Path.GetExtension(company.Photo.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Images"));
                    //if (!Directory.Exists(path))
                    //    Directory.CreateDirectory(path);
                    //Guid fileName=new Guid();
                    //company.Photo.SaveAs(path + fileName + extension);
                    ViewBag.Message = companyManager.CreateCompany(company);
                    return RedirectToAction("Index");
                }

                return View(company);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
                throw;
            }
           
        }

        // GET: /Company/EditCategory/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = companyManager.GetCompanies().Find(a=>a.Id==id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: /Company/EditCategory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Name,Address,ContactNo,Email,Website,Image")] Company company)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = companyManager.Edit(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: /Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = companyManager.GetCompanies().Find(a=>a.Id==id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: /Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Company company = companyManager.GetCompanies().Find(a=>a.Id==id);
            ViewBag.Message = companyManager.Delete(company);
            
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
