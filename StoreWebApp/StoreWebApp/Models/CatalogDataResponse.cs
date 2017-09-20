using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreWebApp.Data;

namespace StoreWebApp.Models
{
    public class CatalogDataResponse
    {
        public Group Group { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}