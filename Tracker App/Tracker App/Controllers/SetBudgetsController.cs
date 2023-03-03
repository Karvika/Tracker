using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tracker_App.Models;

namespace Tracker_App.Controllers
{
    public class SetBudgetsController : Controller
    {
        private TrackerDBContext db = new TrackerDBContext();

        // GET: SetBudgets
        public ActionResult Index()
        {
            var setBudgets = db.SetBudgets.Include(s => s.expenseCategory);
            return View(setBudgets.ToList());
        }

        // GET: SetBudgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetBudget setBudget = db.SetBudgets.Find(id);
            if (setBudget == null)
            {
                return HttpNotFound();
            }
            return View(setBudget);
        }

        // GET: SetBudgets/Create
        public ActionResult Create()
        {
            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName");
            return View();
        }

        // POST: SetBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,budgetAmount,categId,userEmail")] SetBudget setBudget)
        {
            if (ModelState.IsValid)
            {
                db.SetBudgets.Add(setBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName", setBudget.categId);
            return View(setBudget);
        }

        // GET: SetBudgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetBudget setBudget = db.SetBudgets.Find(id);
            if (setBudget == null)
            {
                return HttpNotFound();
            }
            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName", setBudget.categId);
            return View(setBudget);
        }

        // POST: SetBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,budgetAmount,categId,userEmail")] SetBudget setBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categId = new SelectList(db.ExpenseCategories, "id", "categoryName", setBudget.categId);
            return View(setBudget);
        }

        // GET: SetBudgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetBudget setBudget = db.SetBudgets.Find(id);
            if (setBudget == null)
            {
                return HttpNotFound();
            }
            return View(setBudget);
        }

        // POST: SetBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetBudget setBudget = db.SetBudgets.Find(id);
            db.SetBudgets.Remove(setBudget);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
