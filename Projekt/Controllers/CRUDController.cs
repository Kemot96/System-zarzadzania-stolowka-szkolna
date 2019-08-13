using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}