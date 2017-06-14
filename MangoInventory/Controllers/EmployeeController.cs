using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MangoInventory.BLL;
using MangoInventory.Models;
using MangoInventory.DBContext;

namespace MangoInventory.Controllers
{
    public class EmployeeController : Controller
    {
        private MangoDbContext db = new MangoDbContext();

        // GET: /Employee/
        EmployeeManager employeeManager=new EmployeeManager();
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department).Include(e => e.Designation);
            return View(employees.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = db.Employees.Include(d=>d.Department).Include(d=>d.Designation).ToList();
            Employee employee = employees.Find(a => a.Id == id);
            
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Register
        public ActionResult Register()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name");
            return View();
        }

        // POST: /Employee/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include="Id,Name,Address,ContactNo,Email,EmpId,DesignationId,DepartmentId,PasswordHash,ConfirmPassword,Status")] Employee employee)
        {
            try
            {
                employee.EmpId = employee.EmpId.ToUpper();
                if (ModelState.IsValid)
                {
                    var employeeCreated = employeeManager.Create(employee);
                    if (employeeCreated != null)
                    {
                        return RedirectToAction("Details", new { id = employeeCreated.Id });
                    }
                    ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
                    ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
                    return View(employee);
                   // return RedirectToAction("Index");
                }

                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
                ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
                return View(employee);
            }
            catch (DbEntityValidationException exception)
            {
                foreach (var eve in exception.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                ViewBag.Message = exception.Message;
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
                ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
                return View(employee);
            }
           
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Address,ContactNo,Email,EmpId,DesignationId,DepartmentId,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employeeManager.Edit(employee) != null)
                {
                    return RedirectToAction("Details",new{id=employee.Id});
                }
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
                ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
                return View(employee);

            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", employee.DesignationId);
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
