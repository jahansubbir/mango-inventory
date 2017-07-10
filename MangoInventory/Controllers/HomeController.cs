using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MangoInventory.BLL;
using MangoInventory.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace MangoInventory.Controllers
{
    public class HomeController : Controller
    {
        public static int eId, LoginStatus, AdminId;
        LoginManager loginManager = new LoginManager();
        //EmployeeManager employeeManager = new EmployeeManager();
        //DepartmentManager departmentManager = new DepartmentManager();
        //DesignationManager designationManager = new DesignationManager();
        CompanyManager companyManager = new CompanyManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
         
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string returnUrl)
        {
            try
            {
                login.Date = DateTime.Now;
                var employee = loginManager.Login(login);
                HttpCookie cookie = new HttpCookie("loginCookie");
                if (employee != null)
                {
                   
                  cookie["loginStatus"] = 1.ToString();
                    cookie["eId"] = employee.Id.ToString();
                    cookie["role"] = employee.Status;
                    cookie.Expires = DateTime.Now.AddDays(2);

                    Response.Cookies.Add(cookie);


                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Invalid UserId or Password";
                    //Response.Write("<script>alert("+TempData["message"]+");</script>");
                    return RedirectToAction("Index");
                }


            }
            catch (Exception e)
            {

               TempData["message"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout(int? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logout(int? id, Login login)
        {
            HttpCookie cookie = Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                cookie["loginStatus"] = null;
                cookie["eId"] = null;
                cookie["adminId"] = null;
                cookie["role"] = null;
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index");
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        

      
    }
}