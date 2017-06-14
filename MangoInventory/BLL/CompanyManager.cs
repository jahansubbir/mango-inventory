using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;

namespace MangoInventory.BLL
{
    public class CompanyManager
    {
        CompanyGateway companyGateway=new CompanyGateway();

        public string CreateCompany(Company company)
        {
            if (companyGateway.GetCompanies().Count==0)
            {
                if (companyGateway.CreateCompany(company) > 0)
                {
                    return "Company has been created";
                }
                else return "Failed attempt";
            }
            else
            {
                return "A company is already exist. Try buying another software to use for another company";
            }
        }

        public List<Company> GetCompanies()
        {
            return companyGateway.GetCompanies();
            //   throw new NotImplementedException();
        }

        public string Edit(Company company)
        {
            if (companyGateway.Edit(company)>0)
            {
                return "Edited Successfully";
            }
            else
            {
                return "Failed";
            }
        }

        public string Delete(Company company)
        {
            if (companyGateway.Delete(company)>0)
            {
                return "Deleted";
            }
            else
            {
                return "Failed";
            }
        }
    }
}