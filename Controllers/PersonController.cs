using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class PersonController : Controller
    {
        private MainDBContext db = new MainDBContext();


        

        //
        // GET: /Person/

        public ActionResult Index()
        {
            if ((Session["Admin"]==null) || (Session["Admin"]!=null) &&(Session["Admin"].ToString()!="Admin")) { return RedirectToAction("Admin", "Connection"); }
            return View(db.Person.Include(m => m.PersonRole).ToList());
        }

        //
        // GET: /Person/Details/5

        public ActionResult Details(int id = 0)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // GET: /Person/Create
        
        public ActionResult Create()
        {
            ViewBag.PersonRoleID = new SelectList(db.PersonRole, "ID", "Role");
            return View();
        }

        //
        // POST: /Person/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonRoleID = new SelectList(db.PersonRole, "ID", "Role", person.PersonRoleID);
            return View(person);
        }

        //
        // GET: /Person/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonRoleID = new SelectList(db.PersonRole, "ID", "Role");
            return View(person);
        }

        //
        // POST: /Person/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //
        // GET: /Person/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Person/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}