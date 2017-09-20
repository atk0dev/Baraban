using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreWebApp.Data;

namespace StoreWebApp.Controllers
{
    public class ItemsController : ApiController
    {
        private IStoreDataRepository _repo;

        public ItemsController(IStoreDataRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Item> Get(int id)
        {
           // System.Threading.Thread.Sleep(3000);
            return _repo.GetItems(id);
        }

        public HttpResponseMessage Post([FromBody]Item newItem)
        {
            if (_repo.AddItem(newItem) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                  newItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(int id)
        {
            if (_repo.DeleteItem(id) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Put([FromBody]Item updItem)
        {
            if (_repo.EditItem(updItem) != null &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                  updItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
