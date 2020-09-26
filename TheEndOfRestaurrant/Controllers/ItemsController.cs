using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TheEndOfRestaurrant.Models;
using System.Web.Script.Serialization;
namespace TheEndOfRestaurrant.Controllers
{
    public class ItemsController : Controller
    {
        [HttpPost]
        public JsonResult DBCHECK()
        {
            string word = "Nothing";
            var context = new RestaurantFilesEntities5();
            var listDB = context.BoughtItems.ToList();
            var listCoockie = new JavaScriptSerializer().Deserialize<List<BoughtItem>>(Request.Cookies.Get("Bought").Value);
            if (listCoockie.Count != listDB.Count)
            {
                HttpCookie cookie = new HttpCookie("Bought", new JavaScriptSerializer().Serialize(listDB));
                Response.Cookies.Set(cookie);
                return Json(listDB, JsonRequestBehavior.AllowGet);

            }
            return Json(word, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Items(string Name, string Price, string Description, string ItemCategory)
        {
            var context = new RestaurantFilesEntities5();
            List<BoughtItem> list = context.BoughtItems.ToList();
            HttpCookie cookie = new HttpCookie("Bought", new JavaScriptSerializer().Serialize(list));
            Response.Cookies.Set(cookie);
            ViewBag.ItemCategory = new SelectList(context.Categories, "id", "Name").ToList();
            var listoforders = context.BoughtItems.ToList();
            var items = context.Items.ToList();
            toOne t1 = new toOne();
            foreach (var item in items)
            {
                t1.items.Add(new Item()
                {
                    Name = item.Name,
                    Category = item.Category,
                    Description = item.Description,
                    ID = item.ID,
                    Pic = item.Pic,
                    Price = item.Price
                });
            }
            foreach (var item in listoforders)
            {
                t1.list.Add(new BoughtItem()
                {
                    Name = item.Name,
                    Price = item.Price,
                    ID = item.ID,
                    Adress = item.Adress,
                    Person = item.Person,
                    PhoneNumber = item.PhoneNumber
                });
            }
            return View(t1);
        }
        [HttpPost]
        public ActionResult AddItem(string Name, string Price, string Description, string ItemCategory, HttpPostedFileBase pic)
        {
            var context = new RestaurantFilesEntities5();
            var Item = new Item
            {
                Name = Name,
                Price = double.Parse(Price),
                Categories = int.Parse(ItemCategory),
                Pic = " ",
                Description = Description
            };
            context.Items.Add(Item);
            context.SaveChanges();
            return RedirectToAction("Items");
        }
        public ActionResult DeleteItem(string product)
        {

            var context = new RestaurantFilesEntities5();
            var itemdelete = context.BoughtItems.Where(p => p.Name == product).FirstOrDefault();
            if (itemdelete != null)
            {
                context.BoughtItems.Remove(itemdelete);
                context.SaveChanges();
            }
            int deleteProductID = itemdelete.ID;
            var list = context.BoughtItems;
            HttpCookie cookie = new HttpCookie("Bought", new JavaScriptSerializer().Serialize(list));
            Response.Cookies.Set(cookie);
            return Json(deleteProductID, JsonRequestBehavior.AllowGet);
        }
    }
}