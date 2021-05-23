using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabaseClient.Controllers
{
    public class TransactionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Transaction Page";

            return View();
        }
    }
}