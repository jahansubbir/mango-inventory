using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class LoginGateway
    {
        MangoDbContext db=new MangoDbContext();

        public Employee Login(Login login)

        {
            var password=EmployeeGateway.HashPassword(login.Password);
            login.UserId = login.UserId.ToUpper();
            var employees = db.Employees.ToList();
            var employee = employees.Find(a => a.EmpId.Equals(login.UserId) && a.PasswordHash == password);
            return employee;
        }


       
    }
}