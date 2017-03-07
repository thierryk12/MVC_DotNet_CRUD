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
    public class ActivityController : Controller
    {
        private MainDBContext db = new MainDBContext();

        //
        // GET: /Activity/

        public ActionResult Index()
        {


            if (Session["ID"]==null) { return RedirectToAction("Index", "Connection"); }
            else
            {
                if (Session["Admin"].ToString() != "Admin")
                {
                    int personid = int.Parse(Session["ID"].ToString());
                    var activity = db.Activity.Include(a => a.ActivityType).Include(a => a.ActivityStatus).Where(a => a.PersonID == personid);
                    ViewBag.name = Session["Name"];
                    return View(activity.ToList());
                }
                else {
                    var activity = db.Activity.Include(a => a.ActivityType).Include(a => a.ActivityStatus);

                    return View(activity.ToList());
                }
                
            }
        }

        //
        // GET: /Activity/Details/5

        public ActionResult Details(int id = 0)
        {
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // GET: /Activity/Create

        public ActionResult Create()
        {
            ViewBag.ActivityTypeID = new SelectList(db.ActivityType, "ID", "Type");
            ViewBag.ActivityStatusID = new SelectList(db.ActivityStatus, "ID", "Status");
            return View();
        }
        
        //
        // POST: /Activity/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {

                int personid = int.Parse(Session["ID"].ToString());
                activity.PersonID = personid;
                activity.PersonName = Session["Name"].ToString();
                activity.ActivityStatusID = 3;
                db.Activity.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityTypeID = new SelectList(db.ActivityType, "ID", "Type", activity.ActivityTypeID);
            ViewBag.ActivityStatusID = new SelectList(db.ActivityStatus, "ID", "Status", activity.ActivityStatusID);
            return View(activity);
        }

        //
        // GET: /Activity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityTypeID = new SelectList(db.ActivityType, "ID", "Type", activity.ActivityTypeID);
            ViewBag.ActivityStatusID = new SelectList(db.ActivityStatus, "ID", "Status", activity.ActivityStatusID);
            return View(activity);
        }

        //
        // POST: /Activity/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                int personid = int.Parse(Session["ID"].ToString());
                activity.PersonID = personid;
                activity.ActivityStatusID = 3;
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypeID = new SelectList(db.ActivityType, "ID", "Type", activity.ActivityTypeID);
            ViewBag.ActivityStatusID = new SelectList(db.ActivityStatus, "ID", "Status", activity.ActivityStatusID);
            return View(activity);
        }

        //
        // GET: /Activity/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activity/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Connection");
        }

        public ActionResult Friend(string FriendName)
        {

            if (!string.IsNullOrEmpty(FriendName)) { return View(db.Person.Include(m => m.PersonRole).Where(m => m.Name.Contains(FriendName)).ToList()); }
            return View(db.Person.Include(m => m.PersonRole).ToList());
        }

        public ActionResult AddFriend()
        {

            return View();
        }

        public ActionResult Contribution(int id=0)
        {
            var contributionlist = db.Contribution.Where(c => c.EventId==id);
            return View(contributionlist.ToList());
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}