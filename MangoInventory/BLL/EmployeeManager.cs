using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;

namespace MangoInventory.BLL
{
    public class EmployeeManager
    {
        EmployeeGateway employeeGateway=new EmployeeGateway();

        public Employee Create(Employee employee)
        {
            if (!GetEmployees().Contains(employee))
            {
                if (employeeGateway.Create(employee) > 0)
                {
                    return employee;
                }
            }
            return null;
        }

        public Employee Edit(Employee employee)
        {
            if (employeeGateway.Edit(employee)>0)
            {
                return/* "Employee Information has been edited successfully"*/ employee;
            }
            return null;
        }

        public List<Employee> GetEmployees()
        {
            return employeeGateway.GetEmployees();
        } 
    }
}