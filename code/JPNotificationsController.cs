using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using JobPlacementDashboard.Models;
using Microsoft.AspNet.Identity;

namespace JobPlacementDashboard.Controllers
{
    public class JPNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: JPNotifications
        public ActionResult Update(string sortOrder, string searchString)
        {
            ViewBag.GraduateSortParm = sortOrder == "Graduate" ? "graduate_desc" : "Graduate";
            ViewBag.HireSortParm = sortOrder == "Hire" ? "hire_desc" : "Hire";
            ViewBag.DateSortParm = sortOrder == "Notification Date" ? "date_desc" : "Notification Date";

            var notifications = from n in db.JPNotifications
                                select n;         

            var studentNoti = from n in db.JPNotifications.Include(j => j.JPStudent)
                        select n;


            if (!String.IsNullOrEmpty(searchString))
            {
                studentNoti = studentNoti.Where(n => n.JPStudent.JPName.Contains(searchString));
            }

            switch (sortOrder)
            {
                default:
                case "Graduate":
                    studentNoti = studentNoti.OrderBy(n => n.Graduate ? 0 : 1);
                    break;
                case "graduate_desc":
                    studentNoti = studentNoti.OrderBy(n => n.Graduate ? 0 : 1);
                    break;
                case "Hire":
                    studentNoti = studentNoti.OrderBy(n => n.Hire ? 0 : 1);
                    break;
                case "hire_desc":
                    studentNoti = studentNoti.OrderBy(n => n.Hire ? 0 : 1);
                    break;
                case "Notification Date":
                    studentNoti = studentNoti.OrderBy(n => n.NotificationDate);
                    break;
                case "date_desc":
                    studentNoti = studentNoti.OrderByDescending(n => n.NotificationDate);
                    break;
            }

            return View(studentNoti.ToList());
        }
        // GET: JPNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPNotification jPNotification = db.JPNotifications.Find(id);
            if (jPNotification == null)
            {
                return HttpNotFound();
            }
            return View(jPNotification);
        }

        // POST: JPNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPNotification jPNotification = db.JPNotifications.Find(id);
            db.JPNotifications.Remove(jPNotification);
            db.SaveChanges();
            return RedirectToAction("Update");
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