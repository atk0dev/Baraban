using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreWebApp.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Descr { get; set; }

        public int GroupId { get; set; }

        public decimal Price { get; set; }

        public ICollection<Detail> Details { get; set; }

    }
}