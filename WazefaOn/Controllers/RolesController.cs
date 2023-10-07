using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WazefaOn.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WazefaOn.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            try
            { 
            if (role == null)
            {
                return  HttpNotFound();
            }
            return View(role);
              }
            catch (Exception ex)
            {
                return View(role);
            }
}

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            }
            catch (Exception ex)
            {
                return View(role);
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,Name")]IdentityRole role)
        {
            try { 
            if(ModelState.IsValid)
            {
                db.Entry(role).State =  EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
            }
            catch (Exception ex)
            {
                return View(role);
            }
}

        // GET: Categories/Delete/5
        public ActionResult Delete(string id)
        {
           var role =db.Roles.Find(id);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(IdentityRole role)
        {
            try
            {
                var myRole = db.Roles.Find(role.Id);
                db.Roles.Remove(myRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(role);
            }
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
