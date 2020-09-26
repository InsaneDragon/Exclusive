using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TheEndOfRestaurrant.Models;
namespace TheEndOfRestaurrant.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        [HttpPost]
        public ActionResult FirstAjax(MyObj obj)
        {
            double FullPrice1 = 0; double Delivery = 0;
            string amount = ""; string amountprice = "";
            string newamount = ""; string newamountprice = "";
            var context = new RestaurantFilesEntities5();
            var list = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            foreach (var item in list)
            {
                if (item.ID == obj.ID)
                {
                    amount = item.Amount.ToString();
                    amountprice = item.AmountPrice.ToString();
                    item.Amount++;
                    item.AmountPrice = item.Amount * item.Price;
                    newamount = item.Amount.ToString();
                    newamountprice = item.AmountPrice.ToString();
                }
                FullPrice1 += item.AmountPrice;
            }
            Delivery = FullPrice1 + (FullPrice1 * 0.1);
            obj object1 = new obj { amount2 = amount, amountPrice2 = amountprice, amount3 = newamount, amountPrice3 = newamountprice, FullPrice = FullPrice1, DeliveryPrice = Delivery };
            Response.Cookies.Add(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list)));
            return Json(object1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SecondAjax(MyObj obj)
        {
            double FullPrice1 = 0;double Delivery=0;
            string amount = ""; string amountprice = "";
            string newamount = ""; string newamountprice = "";
            var context = new RestaurantFilesEntities5();
            var list = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            foreach (var item in list)
            {
                if (item.ID == obj.ID)
                {
                    amount = item.Amount.ToString();
                    amountprice = item.AmountPrice.ToString();
                    if (item.Amount > 1)
                    {
                        item.Amount--;
                        item.AmountPrice = item.Amount * item.Price;
                    }
                    newamount = item.Amount.ToString();
                    newamountprice = item.AmountPrice.ToString();
                }
                    FullPrice1 += item.AmountPrice;
            }
            Delivery = FullPrice1 + (FullPrice1 * 0.1);
            obj object1 = new obj { amount2 = amount, amountPrice2 = amountprice, amount3 = newamount, amountPrice3 = newamountprice,FullPrice=FullPrice1,DeliveryPrice=Delivery };
            Response.Cookies.Add(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list)));
            return Json(object1, JsonRequestBehavior.AllowGet);
        }
        public class MyObj
        {
            public int ID { get; set; }
        }
        public class obj
        {
            public string amount2 { get; set; }
            public string amountPrice2 { get; set; }
            public string amount3 { get; set; }
            public string amountPrice3 { get; set; }
            public double FullPrice { get; set; }
            public double DeliveryPrice { get; set; }




        }

    }
}