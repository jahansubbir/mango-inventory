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
            login.UserId = login.UserId.ToUpper();
            var employee = db.Employees.ToList().Find(a => a.EmpId == login.UserId && a.PasswordHash==login.Password);
            return employee;
        }


       
    }
}