using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationManagementSystem;

namespace BloodDonationManagementSystem.Areas.ADMIN.Controllers
{[Authorize(Roles ="ADMIN")]
    public class EventsController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }        

        // GET: ADMIN/Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADMIN/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventName,Place,Image,Date")] Event @event,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                if (Image != null && Image.ContentLength>0 )
                {
                    string ImageName = System.IO.Path.GetFileName(Image.FileName); //file2 to store path and url  
                    string physicalPath = Server.MapPath("~/Content/Uploads/" + ImageName);
                    
                    // store path in database  
                    @event.Image = "Content/Uploads/" + ImageName;

                    ////var fileName = Path.GetFileName(Image.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Content/UploadedFiles"), fileName);
                    if (!Directory.Exists(physicalPath))
                    {
                       Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/"));
                    }
                    Image.SaveAs(physicalPath);
                    //Image.SaveAs(path);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: ADMIN/Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: ADMIN/Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventName,Place,Image,Date")] Event @event,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                if (Image != null && Image.ContentLength>0 )
                {
                    string ImageName = System.IO.Path.GetFileName(Image.FileName); //file2 to store path and url  
                    string physicalPath = Server.MapPath("~/Content/Uploads/" + ImageName);
                    
                    // store path in database  
                    @event.Image = "Content/Uploads/" + ImageName;

                    ////var fileName = Path.GetFileName(Image.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Content/UploadedFiles"), fileName);
                    if (!Directory.Exists(physicalPath))
                    {
                       Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/"));
                    }
                    Image.SaveAs(physicalPath);
                    //Image.SaveAs(path);
                }
                         
                
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: ADMIN/Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: ADMIN/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
