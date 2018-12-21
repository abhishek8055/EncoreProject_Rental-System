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
        public ActionResult Index()
        {
            ViewBag.Email = Convert.ToString(Session["USEREMAIL"]);
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
            bool status = userActionDbContext.FeedbackBL(feedback);
            if (status == false)
            {
                throw new Exception("Something went wrong");
            }
            else
                return View("Index");

        }
    }
}