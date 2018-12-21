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
        UserActions ua = new UserActions();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult AddFeedback(FeedbackModel feedback)
        {
            bool status = ua.FeedbackBL(feedback);
            if (status == false)
            {
                throw new Exception("Something went wrong");
            }
            else
                return View("Index");

        }
    }
}