using EncoreBL.Repositories;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncoreView.Controllers
{
    public class UserController : Controller
    {
        UserActions ua = new UserActions();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterUser(string email, string password, string rpassword, int userType)
        {
            UserLoginModel user = new UserLoginModel();
            user.Email = email;
            user.Password = password;
            user.RoleId = Convert.ToInt32(userType);
            if(password != rpassword)
            {
                return RedirectToAction("Index", "User");
            }
            bool status = ua.AddUser(user);
            if (!status)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}