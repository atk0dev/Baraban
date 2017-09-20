using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreWebApp.Data
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public string Descr { get; set; }
    }
}