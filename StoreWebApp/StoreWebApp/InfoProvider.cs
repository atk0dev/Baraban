using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using StoreWebApp.Models;

namespace StoreWebApp
{
    public class InfoProvider
    {
        public InfoModel GetInfo()
        {
            var i = new InfoModel();

            
#if DEBUG
            i.Mode = "DEBUG";
#else
            i.Mode = "RELEASE";
#endif

            i.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return i;
        }
    }
}