using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreWebApp.Data;

namespace StoreWebApp.Controllers
{
    public class DetailsController : ApiController
    {
        private IStoreDataRepository _repo;

        public DetailsController(IStoreDataRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Detail> Get(int id)
        {
         
            return _repo.GetDetails(id);
        }

        public HttpResponseMessage Post([FromBody]Detail newDetail)
        {
            if (_repo.AddDetail(newDetail) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                  newDetail);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(int id)
        {
            if (_repo.DeleteDetail(id) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
