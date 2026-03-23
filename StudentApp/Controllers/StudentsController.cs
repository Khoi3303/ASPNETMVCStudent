using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentApp.Models;

namespace StudentApp.Controllers
{
    public class StudentsController : Controller
    {
        private StudentsDBEntities db = new StudentsDBEntities();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.tblStudents.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudents tblStudents = db.tblStudents.Find(id);
            if (tblStudents == null)
            {
                return HttpNotFound();
            }
            return View(tblStudents);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Email,EnrollmentDate")] tblStudents tblStudents)
        {
            if (ModelState.IsValid)
            {
                db.tblStudents.Add(tblStudents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblStudents);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudents tblStudents = db.tblStudents.Find(id);
            if (tblStudents == null)
            {
                return HttpNotFound();
            }
            return View(tblStudents);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,Email,EnrollmentDate")] tblStudents tblStudents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStudents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblStudents);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudents tblStudents = db.tblStudents.Find(id);
            if (tblStudents == null)
            {
                return HttpNotFound();
            }
            return View(tblStudents);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStudents tblStudents = db.tblStudents.Find(id);
            db.tblStudents.Remove(tblStudents);
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
