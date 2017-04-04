using GameOfBreak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Areas.Admin.Controllers
{

    [Authorize(Roles = "Administrador, Gerente, Subgerente, Vendedor")]
    public class AdminHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

    }

}