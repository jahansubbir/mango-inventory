using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class EmployeeGateway
    {
        MangoDbContext db=new MangoDbContext();
        public int Create(Employee employee)
        {
            
            employee.PasswordHash = HashPassword(employee.PasswordHash);
            employee.ConfirmPassword = HashPassword(employee.ConfirmPassword);
            
            db.Employees.Add(employee);
            return db.SaveChanges();
        }

        public int Edit(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
          return  db.SaveChanges();
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            return GetMd5Hash(md5, password);

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}