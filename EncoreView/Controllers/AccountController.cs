using EncoreBL.Repositories;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncoreView.Controllers
{
    public class AccountController : Controller
    {
        UserActions ua = new UserActions();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogin(string email, string password)
        {
            UserLoginModel user = new UserLoginModel();
            user = ua.Login(email, password);
            if(user == null)
            {
                throw new Exception("User Not Found");
            }
            HttpContext.Session["USER"] = user;
            HttpContext.Session["USEREMAIL"] = user.Email;
            return RedirectToAction("Index", "Product");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}