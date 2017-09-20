using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreWebApp.Data;

namespace StoreWebApp.Controllers
{
    public class CatalogController : Controller
    {
        private IStoreDataRepository _repo;

        public CatalogController(IStoreDataRepository repo)
        {
            this._repo = repo;
        }

        public ActionResult Index()
        {
            var menu = _repo.GetGroups().Where(g => g.Visible == true).OrderBy(g => g.ShowOrder).ToList();
            return View(menu);
        }

    }
}
