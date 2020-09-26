using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheEndOfRestaurrant.Models;
using System.Web.Script.Serialization;
namespace TheEndOfRestaurrant.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mylist = Request.Cookies.Get("Orders");
            List<Order> list1 = new List<Order>();
            if (mylist == null)
            {
                HttpCookie Secured = new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list1));
                Response.Cookies.Set(Secured);
            }
            var currenrlist1 = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            return View(currenrlist1);
        }
        [HttpPost]
        public ActionResult AddProduct(string name)
        {
            bool ExitsOrder = false;
            var context = new RestaurantFilesEntities5();
            var Item = context.Items.Where(p => p.Name == name).FirstOrDefault();
            var mylist = Request.Cookies.Get("Orders");
            List<Order> list1 = new List<Order>();
            if (mylist == null)
            {
                HttpCookie Secured = new HttpCookie("Orders", new JavaScriptSerializer().Serialize(list1));
                Response.Cookies.Set(Secured);
            }
            var currenrlist1 = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
            foreach (var item in currenrlist1)
            {
                if (item.Name == name)
                {
                    ExitsOrder = true;
                }
            }
            if (ExitsOrder == false)
            {
                double FullPriceReal = 0;double Delivery = 0;
                int itemSeq = 0;
                if (currenrlist1.Count() > 0)
                {
                    itemSeq = currenrlist1.Select(p=> p.ID).Max() + 1;
                }
                else
                {
                    itemSeq = 1;
                }
                var NewOrder = new Order
                {
                    ID = itemSeq,
                    Name = Item.Name,
                    Amount = 1,
                    AmountPrice = Item.Price,
                    Price = Item.Price,
                    Pic = Item.Pic
                };
                var currenrlist = new JavaScriptSerializer().Deserialize<List<Order>>(Request.Cookies.Get("Orders").Value);
                currenrlist.Add(NewOrder);
                Response.Cookies.Set(new HttpCookie("Orders", new JavaScriptSerializer().Serialize(currenrlist)));
                foreach (var item in currenrlist)
                {
                    FullPriceReal += item.AmountPrice;
                }
                Delivery = FullPriceReal + (FullPriceReal*0.1);
                var Current = new CurrentOrder
                {
                    ID = itemSeq,
                    Amount = 1,
                    AmountPrice = Item.Price,
                    Price = Item.Price,
                    fullPrice = FullPriceReal,
                    Delivery = Delivery
                };
                return Json(Current, JsonRequestBehavior.AllowGet);
            }
            return Json("NULL", JsonRequestBehavior.AllowGet);
        }
        public ActionResult CattegoryFoods()
        {
            var context = new RestaurantFilesEntities5();
            var list = context.Items.ToList();
            return View(list);
        }
        public class CurrentOrder
        {
            public double Price { get; set; }
            public double Amount { get; set; }
            public double AmountPrice { get; set; }
            public double ID  { get; set; }
            public double fullPrice { get; set; }
            public double Delivery { get; set; }

        }
    }
}