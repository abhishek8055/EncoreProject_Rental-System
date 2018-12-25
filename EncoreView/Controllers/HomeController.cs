using EncoreBL.Repositories;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncoreView.Controllers
{
    public class HomeController : Controller
    {
        UserActions userActionDbContext = new UserActions();

        //HOME PAGE
        public ActionResult Index()
        {
            ViewBag.Email = Convert.ToString(Session["USEREMAIL"]);
            return View();
        }

        //ABOUT PAGE
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //CONTACT PAGE
        public ActionResult Contact()
        {
            return View();
        }

        //POST: ADD FEEDBACK
        public ActionResult AddFeedback(FeedbackModel feedback)
        {
            bool status = userActionDbContext.FeedbackBL(feedback);
            if (status == false)
            {
                TempData["FeedbackFail"] = true;              
            }
            else
                TempData["FeedbackFail"] = false;
            return RedirectToAction("Contact", "Home");
        }
    }
}