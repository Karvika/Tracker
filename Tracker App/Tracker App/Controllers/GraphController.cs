using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Tracker_App.Models;

namespace Tracker_App.Controllers
{
    public class GraphController : Controller
    {
        // GET: Graph
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getData()
        {

            TrackerDBEntities context = new TrackerDBEntities();
            string user = HttpContext.User.Identity.Name;
           
            ViewBag.Name  = user;
            var query = context.AddExpenses.Where(e => e.userEmail == user);
            var query1 = query.GroupBy(p => p.date)
                        .Select(g => new { date = g.Key, count = g.Sum(w => w.expenseAmount) }).ToList();



            return Json(query1,JsonRequestBehavior.AllowGet);
        }
    }
}