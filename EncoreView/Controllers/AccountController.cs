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
        UserActions userActionContext = new UserActions();

        //LOGIN AND REGISTRATION FORM
        public ActionResult Index()
        {
            return View();
        }

        //GET: USER AUTHENTICATION
        public ActionResult UserLogin(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Account");
            }

            UserLoginModel user = new UserLoginModel();
            user = userActionContext.Login(email, password);
            if(user == null)
            {
                TempData["WrongDetails"] = true;
                return RedirectToAction("Index", "Account");
            }
            HttpContext.Session["USER"] = user;
            HttpContext.Session["USEREMAIL"] = user.Email;
            HttpContext.Session["ROLE"] = user.RoleId;
            return RedirectToAction("RedirectTo", "Account");
        }

        //REROUTING METHOD
        public ActionResult RedirectTo()
        {
            int RoleId = Convert.ToInt32(HttpContext.Session["ROLE"]);
            if (RoleId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (RoleId == 1)
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (RoleId == 2)
            {
                return RedirectToAction("VendorIndex", "User");
            }
            else
                return RedirectToAction("Index", "User");
        }

        //LOGOUT METHOD
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}