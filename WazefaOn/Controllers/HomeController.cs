using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WazefaOn.Models;


namespace WazefaOn.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View(db.Categories.ToList());
        }

        public ActionResult Details(int jobId) {

        var job = db.Jobs.Find(jobId);
            if (job ==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message )
        {
            var userId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var MaxApplications =
                (from F in db.Jobs 
                where F.Id == JobId 
                select F.MaxApplicants).First();

            var ApplicationinDayCount =
                (from F in db.ApplyForJobs
                 where F.UserId == userId & F.ApplyDate == DateTime.Now
                 select F.ApplyDate).First();


            var check = db.ApplyForJobs.Where( a=>a.JobId == JobId && a.UserId == userId ).ToList();
            var checkApplicationinDayCount = db.ApplyForJobs.Where(a => a.ApplyDate == DateTime.Now
                                             && a.UserId == userId).ToList().Count;

            var checkMaxApplications = db.ApplyForJobs.Where(a => a.JobId == JobId).ToList().Count() < MaxApplications;
            if(check.Count < 1)
            {
                ViewBag.Result = "You had applied for this job before ";
            }
            else if (checkMaxApplications)
            {
                ViewBag.Result = "This job reached Max Application";
            }
            else if (checkApplicationinDayCount == 0)
            {
                ViewBag.Result = "You applied for a job today";
            }
            else { 
                var job = new ApplyForJob();
                job.JobId = JobId;
                job.Message = Message;
                job.UserId = userId;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "Applying Done successfully";
            }
            return View();
        }

        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfJob(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.UserId == UserId
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          { 
                            JobTitle = gr.Key,
                            Items = gr
                          };


            return View(grouped.ToList());
        }

        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View (job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplyForJob job)
        {
            try
            {
                var myJob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myJob);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            catch
            {
                return View(job);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            try
            {
                var mail = new MailMessage();
                var loginInfo = new NetworkCredential("ia6969053@gmail.com", "Ibr@him123");
                mail.From = new MailAddress(contact.Email);
                mail.Subject = contact.Subject;
                mail.To.Add(new MailAddress("ia6969053@gmail.com"));
                mail.IsBodyHtml = true;
                string body = "Sender Name : " + contact.Name + "<br>" +
                              "Sender Mail: " + contact.Email + "<br>" +
                              "Subject : " + contact.Subject + "<br>" +
                              "Message : <br>" + contact.Message + "<br>";

                mail.Body = contact.Message;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(mail);

                return RedirectToAction("Index");
            }catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where( a=> a.JobTitle.Contains(searchName)
            || a.jobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }
    }
}