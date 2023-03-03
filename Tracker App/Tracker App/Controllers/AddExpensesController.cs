using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tracker_App.Models;

namespace Tracker_App.Controllers
{
    public class AddExpensesController : Controller
    {
        private TrackerDBContext db = new TrackerDBContext();

        // GET: AddExpenses
        [Authorize]
        public ActionResult Index()
        {
            string name = HttpContext.User.Identity.Name;
            try
            {
               
                ViewBag.Max = db.AddExpenses.Where(e => e.userEmail == name).Select(e => e.expenseAmount ).Max();
               

            }
            catch
            {
                ViewBag.Max = "login to see the expenses ";
            }
           
            var addExpenses = db.AddExpenses.Where(e => e.userEmail == name).Include(a => a.expenseCategory);
            var list1 = addExpenses.OrderByDescending(a => a.expenseAmount);
            return View(list1.ToList());
            
        }
        [Authorize]
        public ActionResult Trans()
        {
            string name = HttpContext.User.Identity.Name;

            return View(db.AddExpenses.Where(e => e.userEmail == name).Include(e => e.expenseCategory).OrderByDescending(e => e.date).Take(4));
        }

        // GET: AddExpenses/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddExpense addExpense = db.AddExpenses.Find(id);
            if (addExpense == null)
            {
                return HttpNotFound();
            }
            return View(addExpense);
        }

        // GET: AddExpenses/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName");
            return View();
        }

        // POST: AddExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,categId,expenseAmount,date")] AddExpense addExpense)
        {
            addExpense.userEmail = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.AddExpenses.Add(addExpense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName", addExpense.categId);
            return View(addExpense);
        }

        // GET: AddExpenses/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddExpense addExpense = db.AddExpenses.Find(id);
            if (addExpense == null)
            {
                return HttpNotFound();
            }
            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName", addExpense.categId);
            return View(addExpense);
        }

        // POST: AddExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,categId,expenseAmount,date,userEmail")] AddExpense addExpense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addExpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName", addExpense.categId);
            return View(addExpense);
        }

        // GET: AddExpenses/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddExpense addExpense = db.AddExpenses.Find(id);
            if (addExpense == null)
            {
                return HttpNotFound();
            }
            return View(addExpense);
        }

        // POST: AddExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            AddExpense addExpense = db.AddExpenses.Find(id);
            db.AddExpenses.Remove(addExpense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       

    }
}
