using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TheEndOfRestaurrant.Models;

namespace TheEndOfRestaurrant.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult CheckOutView()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            return View();
        }
    }
}