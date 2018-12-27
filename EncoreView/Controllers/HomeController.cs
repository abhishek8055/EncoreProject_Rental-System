using EncoreBL.Repositories;
using EncoreML;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//@AUTHOR ABHISHEK DWIVEDI


namespace EncoreView.Controllers
{
    public class HomeController : Controller
    {
        //INITIALIZING LOGGER
        readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //INITIALIZING USERACTIONS CLASS INSTANCE OF BUSINESS LAYER
        UserActions userActionDbContext = new UserActions();

        //HOME PAGE
        public ActionResult Index()
        {
            try
            {
                ViewBag.Email = Convert.ToString(Session["USEREMAIL"]);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Error("Index() of Home Controller : ", e);
            }
           
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
            {
                TempData["FeedbackFail"] = false;
            }
            return RedirectToAction("Contact", "Home");
        }
    }
}