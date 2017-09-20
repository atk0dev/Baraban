using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreWebApp.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int ShowOrder { get; set; }

        public string Body { get; set; }

        public bool Visible { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}