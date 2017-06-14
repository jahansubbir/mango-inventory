using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;

namespace MangoInventory.BLL
{
    public class LoginManager
    {
        LoginGateway loginGateway=new LoginGateway();

        public Employee Login(Login login)
        {
            return loginGateway.Login(login);
        }

        public string IsUser(Login login)
        {
            int status = loginGateway.Login(login).Status;
            if (status == 1)
            {
                return "User";
            }else if (status==2)
            {
                return "Admin";
            }
            else
            {
                return "invalid";
            }
            
        }
    }
}