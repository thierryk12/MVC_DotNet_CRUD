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
    public class ContributionController : Controller
    {
        private MainDBContext db = new MainDBContext();

        //
        // GET: /Contribution/

        public ActionResult Index()
        {
            var activity = db.Activity.Include(a => a.ActivityType).Include(a => a.ActivityStatus);
            
            return View(activity.ToList());
        }


        
        public ActionResult Viewall()
        {

            var contribution = db.Contribution.Include(c => c.ContributionType);
            return View(contribution.ToList());
        }

        //
        // GET: /Contribution/Details/5

        public ActionResult Details(int id = 0)
        {
            Contribution contribution = db.Contribution.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        //
        // GET: /Contribution/Create

        public ActionResult Create(int id = 0)
        {
            Session["EventID"] = id;
            ViewBag.ContributionTypeID = new SelectList(db.ContributionType, "ID", "Type");
            return View();
        }

        //
        // POST: /Contribution/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                contribution.EventId = int.Parse(@Session["EventID"].ToString());
                db.Contribution.Add(contribution);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ContributionTypeID = new SelectList(db.ContributionType, "ID", "Type", contribution.ContributionTypeID);
            return View(contribution);
        }

        //
        // GET: /Contribution/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Contribution contribution = db.Contribution.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContributionTypeID = new SelectList(db.ContributionType, "ID", "Type", contribution.ContributionTypeID);
            return View(contribution);
        }

        //
        // POST: /Contribution/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContributionTypeID = new SelectList(db.ContributionType, "ID", "Type", contribution.ContributionTypeID);
            return View(contribution);
        }

        //
        // GET: /Contribution/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Contribution contribution = db.Contribution.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        //
        // POST: /Contribution/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contribution contribution = db.Contribution.Find(id);
            db.Contribution.Remove(contribution);
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