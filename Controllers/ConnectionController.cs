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
    public class ConnectionController : Controller
    {
        private MainDBContext db = new MainDBContext();

        //
        // GET: /Connection/

        public ActionResult Index()
        {

            return View();
        }


        // POST: /Account/LogOn

        [HttpPost]
        
        public ActionResult Index(Person model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                string inputname = model.Name;
                string inputpassword = model.Password;
                var judgeperson = from o in db.Person where o.Name == inputname && o.Password == inputpassword select o;
                if (judgeperson.Count() == 1)
                {

                    Session["ID"] = judgeperson.First().ID;
                    Session["Name"] = judgeperson.First().Name;
                    Session["Admin"] = judgeperson.First().PersonRole.Role;
                    return RedirectToAction("Index", "Activity");
                }
                else { ModelState.AddModelError("", "The user login or password provided is incorrect."); }


            }
            return View(model);
        }



        // GET: /Connection/Signup
        public ActionResult Signup()
        {

            return View();
        }

        // POST: /Account/Signup

        [HttpPost]

        public ActionResult Signup(Person model)
        {
            if (ModelState.IsValid)
                {
                    model.PersonRoleID = 1;
                    db.Person.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return RedirectToAction("Index", "Connection");
        }

        // GET: /Connection/Admin
        public ActionResult Admin()
        {

            return View();
        }

        // POST: /Account/Admin

        [HttpPost]

        public ActionResult Admin(Person model)
        {
            if (ModelState.IsValid)
            {

                string inputname = model.Name;
                string inputpassword = model.Password;
                var judgeperson = from o in db.Person where o.Name == inputname && o.Password == inputpassword select o;
                if (judgeperson.Count() == 1)
                {
                    if (judgeperson.First().PersonRole.Role == "Admin")
                    {
                        Session["ID"] = judgeperson.First().ID;
                        Session["Name"] = judgeperson.First().Name;
                        Session["Admin"] = judgeperson.First().PersonRole.Role;
                        return RedirectToAction("Index", "Admin");
                    }
                    else { ModelState.AddModelError("", "You are not Administrator,please connect to Administrator"); }

                    
                }
                else { ModelState.AddModelError("", "The user login or password provided is incorrect."); }


            }
            return View(model);
            
        }




        




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}