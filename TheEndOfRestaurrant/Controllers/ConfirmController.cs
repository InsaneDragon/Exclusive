using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheEndOfRestaurrant.Models;
using System.Web.Script.Serialization;

namespace TheEndOfRestaurrant.Controllers
{
    public class ConfirmController : Controller
    {
        [HttpPost]
        public ActionResult AddProduct(string adress, string Phonenumber, string realname, string surname)
        {
            var listorders = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            var context = new RestaurantFilesEntities5();
            string order = "";
            double Price = 0;
            foreach (var item in listorders)
            {
                order += item.Name + "(" + item.Amount + ")" + "(" + item.AmountPrice + "c)     ";
                Price += item.AmountPrice;
            }
            var RealItem = new BoughtItem
            {
                Name = order,
                Price = Price,
                Adress = adress,
                PhoneNumber = int.Parse(Phonenumber),
                Person = surname + " " + realname
            };
            context.BoughtItems.Add(RealItem);
            listorders = new List<Order>();
            Response.Cookies.Set(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(listorders)));
            context.SaveChanges();
            return RedirectToAction("Confirm","CheckOut");
        }
        public ActionResult Eror()
        {
            return View();
        }
    }
}
