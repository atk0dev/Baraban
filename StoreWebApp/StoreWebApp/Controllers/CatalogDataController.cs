using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreWebApp.Data;
using StoreWebApp.Models;

namespace StoreWebApp.Controllers
{
    public class CatalogDataController : ApiController
    {
        private IStoreDataRepository _repo;

        public CatalogDataController(IStoreDataRepository repo)
        {
            _repo = repo;
        }

        public CatalogDataResponse Get(int id)
        {
            var group = _repo.GetGroupById(id);
            var items = _repo.GetItems(id);
            return new CatalogDataResponse
                       {
                           Group = group,
                           Items = items
                       };
        }
    }
}
