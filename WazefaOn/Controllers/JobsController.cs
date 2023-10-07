using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WazefaOn.Models;

namespace WazefaOn.Controllers
{
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Category);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            Job job = db.Jobs.Find(id);
            try
            {   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
            }
            catch (Exception ex)
            {
                return View(job);
             }
}

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Job job)
        {
            try
            {
                job.UserId = User.Identity.GetUserId();
                job.CategoryId = 2;
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            { ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
                return View(job); 
            }

}



        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            Job job = db.Jobs.Find(id);
            try
            { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
            }
            catch (Exception ex)
            {
                return View(job);
    }
}

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job,DateTime ExpiryDate)
        {
            try { 
            if (ModelState.IsValid)
            {
                job.ExpiryDate = ExpiryDate;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
            }
            catch (Exception ex)
            {
                return View(job);
            }
}

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {            
            Job job = db.Jobs.Find(id);
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
            }
            catch (Exception ex)
            {
                return View(job);
            }

}

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
