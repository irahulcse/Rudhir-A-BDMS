using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BloodDonationManagementSystem.Models;
using PagedList;


namespace BloodDonationManagementSystem.Controllers
{
    [Authorize]
    public class Org_DetailsController : Controller
    {
        private Entities db = new Entities();

        // GET: Org_Details
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();

            var org_details = from r in (db.Org_details.Include(o => o.AspNetUser).Include(r => r.State).Where(r => r.AspNetUser.Id == userid))
                              select (r);
            return View(org_details.ToList());
        }


        // GET: Org_Details/Create
        public ActionResult Create()
        {
            var userx = User.Identity.GetUserId();
            ViewBag.fk_id = userx;
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName");
            return View();
        }

        // POST: Org_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Org_details org_details, HttpPostedFileBase org_image)
        {
            var userx = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Org_details.Add(org_details).fk_id = userx;
                org_details.created_date = DateTime.Now;
                org_details.fk_roleid = "2";

                if (org_image != null && org_image.ContentLength > 0)
                {
                    string ImageName = System.IO.Path.GetFileName(org_image.FileName); //file2 to store path and url  
                    string physicalPath = Server.MapPath("~/Content/Uploads/" + ImageName);
                    
                    // store path in database  
                    org_details.org_image = "Content/Uploads/" + ImageName;


                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/"));
                    }
                    org_image.SaveAs(physicalPath);

                }

                db.SaveChanges();
                return RedirectToAction("Create","OrgRoles");

            }

            ViewBag.fk_id = userx;
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", org_details.fk_stateid);

            return View(org_details);

        }
        // GET: Org_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            var userx = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org_details org_details = db.Org_details.Find(id);
            if (org_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_id = userx;
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", org_details.fk_stateid);
            return View(org_details);
        }

        // POST: Org_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pk_orgid,org_name,org_descp,fk_id,fk_stateid,org_image,IsActive,created_date,fk_roleid")] Org_details org_details, HttpPostedFileBase org_image)
        {
            var userx = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                org_details.created_date = DateTime.Now;
                org_details.fk_roleid = "2";
                db.Entry(org_details).State = EntityState.Modified;
                if (org_image != null && org_image.ContentLength > 0)
                {
                    string ImageName = System.IO.Path.GetFileName(org_image.FileName); //file2 to store path and url  
                    string physicalPath = Server.MapPath("~/Content/Uploads/" + ImageName);
                    //string physicalPath = HttpContext.Server.MapPath("https://entrepreneurslives.com/Uploads/" + ImageName);


                    org_details.org_image = "Content/Uploads/" + ImageName;

                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/"));
                    }
                    org_image.SaveAs(physicalPath);

                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_id = userx;
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", org_details.fk_stateid);
            return View(org_details);
        }

       

        public ActionResult MessageIndex(int page = 1)
        {
            var userid = User.Identity.GetUserId();
            
            var msg = db.Messages.Include(d => d.Blood_Gp).Include(d => d.Org_details).Include(r => r.State)
                .Where(r => r.Org_details.fk_id == userid)
                .Select(r => r).OrderBy(r => r.PersonName).ToPagedList(page, 5);

            return View(msg);
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
