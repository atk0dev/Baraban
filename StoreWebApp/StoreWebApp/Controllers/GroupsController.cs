using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreWebApp.Data;

namespace StoreWebApp.Controllers
{
  public class GroupsController : ApiController
  {
    private IStoreDataRepository _repo;
    public GroupsController(IStoreDataRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Group> Get(bool includeItems = false)
    {
      IQueryable<Group> results;

      if (includeItems == true)
      {
          results = _repo.GetGroupsIncludingItems();
      }
      else
      {
          results = _repo.GetGroups();
      }
     
      var groups = results.OrderBy(g => g.ShowOrder)
                          .ToList();

      return groups;
    }

    public HttpResponseMessage Get(int id)
    {
      IQueryable<Group> results;

      results = _repo.GetGroups();
     
      var group = results.Where(g => g.Id == id).FirstOrDefault();

      if (group != null) return Request.CreateResponse(HttpStatusCode.OK, group);

      return Request.CreateResponse(HttpStatusCode.NotFound);
    }

    public HttpResponseMessage Post([FromBody]Group newGroup)
    {
      if (_repo.AddGroup(newGroup) &&
          _repo.Save())
      {
        return Request.CreateResponse(HttpStatusCode.Created,
          newGroup);
      }

      return Request.CreateResponse(HttpStatusCode.BadRequest);
    }

    public HttpResponseMessage Put([FromBody]Group updGroup)
    {
        if (_repo.EditGroup(updGroup) != null &&
            _repo.Save())
        {
            return Request.CreateResponse(HttpStatusCode.Created,
              updGroup);
        }

        return Request.CreateResponse(HttpStatusCode.BadRequest);
    }

    public HttpResponseMessage Delete(int id)
    {
        if (_repo.DeleteGroup(id) &&
            _repo.Save())
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        return Request.CreateResponse(HttpStatusCode.BadRequest);
    }
  }
}
