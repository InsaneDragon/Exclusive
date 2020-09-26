using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheEndOfRestaurrant.Models;
using System.Web.Script.Serialization;

namespace TheEndOfRestaurrant.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var list = Request.Cookies.Get("Orders");
            if (list == null)
            {
                var list1 = new List<Order>();
                HttpCookie orders = new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list1));
                Response.Cookies.Set(orders);

            }
            var listorders = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            return View(listorders);
        }
        public ActionResult AddFish()
        {
            int id = 0;
            string result = "";
            var list = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            foreach (var item in list)
            {
                if (item.Name == "Классический стейк из сёмги")
                {
                    result = "No";
                }
            }
            if (result != "No")
            {
                int itemSeq = 0;
                if (list.Count() > 0)
                {
                    itemSeq = list.Select(p => p.ID).Max() + 1;
                }
                else
                {
                    itemSeq = 1;
                }
                Order OrderFish = new Order
                {
                    Name = "Классический стейк из сёмги",
                    Amount = 1,
                    AmountPrice = 85,
                    Pic = " ",
                    Description = "200гр",
                    Price = 85,
                    ID = itemSeq
                };
                list.Add(OrderFish);
                HttpCookie cookie = new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list));
                Response.Cookies.Set(cookie);
                result = "Yes";
                id = itemSeq;
            }
            var Current = new RealCurrent {ID=id,result1=result};
            return Json(Current, JsonRequestBehavior.AllowGet);
        }
        public class RealCurrent
        {
            public int ID { get; set; }
            public string result1 { get; set; }
        }
        [HttpPost]
        public ActionResult AddAmount(int id)
        {
            var list = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            foreach (var item in list)
            {
                if (item.ID == id)
                {
                    item.Amount++;
                    item.AmountPrice = item.Amount * item.Price;
                }
            }
            Response.Cookies.Set(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list)));
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeleteAmount(int id)
        {
            var list = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id && list[i].Amount == 1)
                {
                    list.Remove(list[i]);
                    Response.Cookies.Set(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list)));
                }
            }
            foreach (var item in list)
            {
                if (item.ID == id)
                {
                    item.Amount--;
                    item.AmountPrice = item.Amount * item.Price;
                    Response.Cookies.Set(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list)));
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult RemoveItem(int id)
        {
            var list = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id)
                {
                    list.Remove(list[i]);
                }
            }
            double Price1 = 0;
            double Count = 0;
            double delivery = 0;

            for (int i = 0; i < list.Count; i++)
            {
                Price1 += list[i].AmountPrice;
                Count += list[i].Amount;
            }
            delivery = Price1 + (Price1 * 0.1);
            var Delete = new DeleteOrder { AmountPrice = Price1, Count = Count, Delivery = delivery };
            Response.Cookies.Set(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list)));
            return Json(Delete, JsonRequestBehavior.AllowGet);
        }
        public class DeleteOrder
        {
            public double AmountPrice { get; set; }
            public double Count { get; set; }
            public double Delivery { get; set; }
        }

    }
}