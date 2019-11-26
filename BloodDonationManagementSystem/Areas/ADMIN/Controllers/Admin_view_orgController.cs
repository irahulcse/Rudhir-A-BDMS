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
using BloodDonationManagementSystem.Areas.ADMIN.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BloodDonationManagementSystem.Areas.ADMIN.Controllers
{
    public class Admin_view_orgController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/Admin_view_org
        public ActionResult Index(int page=1)
        {
            var org_details = (from r in db.Org_details
                               join x in db.Donors on r.pk_orgid equals x.fk_donor_orgid into a from x in a.DefaultIfEmpty()
                               group new { r, x } by new { r.pk_orgid, r.org_name, r.created_date,r.IsActive,r.AspNetUser.Email,r.fk_id ,r.State.StateName} into g
                               select new CountViewModel()
                               { org_id = g.Key.pk_orgid,
                               fk_id=g.Key.fk_id,
                                  org_name =g.Key.org_name,
                                  State=g.Key.StateName,
                                   isactive=g.Key.IsActive,
                                  Date=g.Key.created_date,
                                  email=g.Key.Email,
                                Donorcount= g.Count(p=>p.x.fk_donor_orgid==p.r.pk_orgid)

                              }).OrderByDescending(r=>r.Donorcount).ToPagedList(page,3);
            
            return View(org_details);
        }
      
        // GET: ADMIN/Admin_view_org/Create
        public ActionResult Create()
        {
            ViewBag.fk_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName");

            return View();
        }

        // POST: ADMIN/Admin_view_org/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pk_orgid,org_name,org_descp,fk_id,fk_stateid,org_image,IsActive,created_date,fk_roleid")] Org_details org_details, HttpPostedFileBase org_image)
        {
            if (ModelState.IsValid)
            {
                db.Org_details.Add(org_details).created_date = DateTime.Now;
                org_details.fk_roleid = "2"; if (org_image != null && org_image.ContentLength > 0)
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
                return RedirectToAction("Index");
            }

            ViewBag.fk_id = new SelectList(db.AspNetUsers, "Id", "Email", org_details.fk_id);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName",org_details.fk_stateid);

            return View(org_details);
        }

        // GET: ADMIN/Admin_view_org/Edit/5
        public ActionResult Edit(int? id,String idx)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org_details org_details = db.Org_details.Find(id);
            if (org_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_id = new SelectList(db.AspNetUsers.Where(r=>r.Id==idx), "Id", "Email", org_details.fk_id);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", org_details.fk_stateid);
            return View(org_details);
        }

        // POST: ADMIN/Admin_view_org/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pk_orgid,org_name,org_descp,fk_id,fk_stateid,org_image,IsActive,created_date,fk_roleid")] Org_details org_details,string idx, HttpPostedFileBase org_image)
        {
            if (ModelState.IsValid)
            {
                org_details.created_date = DateTime.Now;
                org_details.fk_roleid = "2";
                db.Entry(org_details).State = EntityState.Modified;
                if (org_image != null && org_image.ContentLength > 0)
                {
                    string ImageName = System.IO.Path.GetFileName(org_image.FileName); //file2 to store path and url  
                    string physicalPath = Server.MapPath("~/Content/Uploads/" + ImageName);

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
           
            ViewBag.fk_id = new SelectList(db.AspNetUsers.Where(r => r.Id == idx), "Id", "Email", org_details.fk_id);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", org_details.fk_stateid);
            return View(org_details);
        }
        public ActionResult CreateDonor(int ?id,int idt)
        {           
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name");
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1");
            ViewBag.cid = new SelectList(db.Org_roles.Where(r => r.fk_orgid == id), "pk_roleId", "PersonName");
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r=>r.pk_orgid==id), "pk_orgid", "org_name");
            ViewBag.fk_stateid = new SelectList(db.States.Where(r=>r.stateid==idt), "stateid", "StateName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDonor([Bind(Include = "pk_id,fk_donor_orgid,DonorName,DonorEmail,bid,gid,DOB,ContactNo,fk_stateid,Aadhar_Card_No,IsActive,cid,CreatedTime")] Donor donor, int idt,int ?id)
        {
           if (ModelState.IsValid)
            {

                db.Donors.Add(donor).CreatedTime = DateTime.Now ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", donor.gid);
            ViewBag.cid = new SelectList(db.Org_roles.Where(r=>r.fk_orgid == id), "pk_roleId", "PersonName", donor.cid);
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r => r.pk_orgid == id), "pk_orgid", "org_name", donor.fk_donor_orgid);
            ViewBag.fk_stateid= new SelectList(db.States.Where(r => r.stateid == idt), "stateid", "StateName", donor.fk_stateid);

            return View(donor);
        }
       
        // GET: ADMIN/Admin_view_org/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org_details org_details = db.Org_details.Find(id);
            if (org_details == null)
            {
                return HttpNotFound();
            }
            return View(org_details);
        }

        [HttpGet]
        public ActionResult DisplayDonor(string gp,int? id,int page = 1)
        {
            ViewBag.gp = db.Blood_Gp.Select(r => r.Name).Distinct();
            var donors = db.Donors.Include(d => d.Blood_Gp).Include(d => d.Gender).Include(r=>r.State)
                .Include(d => d.Org_roles).Include(d => d.Org_details).Where(r => r.Org_details.pk_orgid == id).Where(r => r.Blood_Gp.Name == gp || (gp == null))
                .Select(r => r).OrderBy(r => r.DonorName).ToPagedList(page,3);
                        return View(donors);
        }

        // POST: ADMIN/Admin_view_org/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Org_details org_details = db.Org_details.Find(id);
            db.Org_details.Remove(org_details);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteDonor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: Donors/Delete/5
        [HttpPost, ActionName("DeleteDonor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDonorConfirmed(int id)
        {
            Donor donor = db.Donors.Find(id);
            db.Donors.Remove(donor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditDonor(int? id,int? idx)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }

            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name");
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1");
            ViewBag.cid = new SelectList(db.Org_roles.Where(r=>r.fk_orgid==idx), "pk_roleId", "PersonName");
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r => r.pk_orgid == idx), "pk_orgid", "org_name",donor.fk_donor_orgid);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", donor.fk_stateid);
            return View(donor);

          }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDonor([Bind(Include = "pk_id,fk_donor_orgid,DonorName,DonorEmail,bid,gid,DOB,ContactNo,fk_stateid,Aadhar_Card_No,IsActive,cid,CreatedTime")] Donor donor,int id,int idx)
        {          

            if (ModelState.IsValid)
            {
                donor.CreatedTime = DateTime.Now;
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", donor.gid);
            ViewBag.cid = new SelectList(db.Org_roles.Distinct().Where(r => r.fk_orgid == idx), "pk_roleId", "PersonName", donor.cid);
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r => r.pk_orgid == idx), "pk_orgid", "org_name", donor.fk_donor_orgid);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", donor.fk_stateid);

            return View(donor);
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
