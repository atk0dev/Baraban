using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreWebApp.Data;
using StoreWebApp.Models;
using StoreWebApp.Services;

namespace StoreWebApp.Controllers
{
  public class ForumController : Controller
  {
    private IMailService _mail;
    private IStoreDataRepository _repo;

    public ForumController(IMailService mail, IStoreDataRepository repo)
    {
      _mail = mail;
      _repo = repo;
    }

    public ActionResult Index()
    {
      ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

      var topics = _repo.GetTopics()
                        .OrderByDescending(t => t.Created)
                        .Take(25)
                        .ToList();

      return View(topics);
    }

    
  }
}
