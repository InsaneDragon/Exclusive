using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheEndOfRestaurrant.Models;

namespace TheEndOfRestaurrant.Controllers
{
    public class toOne
    {
        public List<Item> items = new List<Item>();
        public List<BoughtItem> list = new List<BoughtItem>();
    }
}