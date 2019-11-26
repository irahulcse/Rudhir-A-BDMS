using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BloodDonationManagementSystem.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using Newtonsoft.Json;

namespace BloodDonationManagementSystem.Controllers
{
    [Authorize]
    public class DonorsController : Controller
    {
        private Entities db = new Entities();

        
        // GET: Donors
        public ActionResult Index(string gp)
        { 
            var userid = User.Identity.GetUserId();
            ViewBag.gp = db.Blood_Gp.Select(r => r.Name).Distinct();
            var donors = db.Donors.Include(d => d.Blood_Gp).Include(d => d.Gender).Include(r=>r.State)
                .Include(d => d.Org_roles).Include(d => d.Org_details).Where(r => r.Org_details.fk_id == userid)
                .Where(r => r.Blood_Gp.Name == gp || (gp == null)).Select(r => r).OrderBy(r => r.DonorName).ToList();

            return View(donors);
        }
        
        // GET: Donors/Create
        public ActionResult Create(int? id)
        {
            var userid = User.Identity.GetUserId();           
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name");
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1");
            ViewBag.cid = new SelectList(db.Org_roles.Distinct().Where(r=>r.Org_details.fk_id==userid), "pk_roleId", "PersonName");
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userid), "pk_orgid", "org_name");
            ViewBag.fk_stateid = new SelectList(db.States.Where(r=>r.stateid==id), "stateid", "StateName");
            return View();
        }

        // POST: Donors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pk_id,fk_donor_orgid,DonorName,DonorEmail,bid,gid,DOB,ContactNo,fk_stateid,Aadhar_Card_No,IsActive,cid,CreatedTime")] Donor donor,int id)
        { 
            var userid = User.Identity.GetUserId();
                 if (ModelState.IsValid)
            {
               
                db.Donors.Add(donor).CreatedTime=DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", donor.gid);
            ViewBag.cid = new SelectList(db.Org_roles.Distinct().Where(r => r.Org_details.fk_id == userid), "pk_roleId", "PersonName", donor.cid);
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userid), "pk_orgid", "org_name", donor.fk_donor_orgid);
            ViewBag.fk_stateid = new SelectList(db.States.Where(r=>r.stateid==id), "stateid", "StateName",donor.fk_stateid);
            return View(donor);
        }

        // GET: Donors/Edit/5
        public ActionResult Edit(int? id)
        {
            var userid = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", donor.gid);
            ViewBag.cid = new SelectList(db.Org_roles.Distinct().Where(r => r.Org_details.fk_id == userid), "pk_roleId", "PersonName", donor.cid);
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r=>r.fk_id==userid), "pk_orgid", "org_name",donor.fk_donor_orgid);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", donor.fk_stateid);
            return View(donor);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pk_id,fk_donor_orgid,DonorName,DonorEmail,bid,gid,DOB,ContactNo,fk_stateid,Aadhar_Card_No,IsActive,cid,CreatedTime")] Donor donor)
        {
            var userid = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                donor.CreatedTime = DateTime.Now;
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", donor.gid);
            ViewBag.cid = new SelectList(db.Org_roles.Distinct().Where(r => r.Org_details.fk_id == userid), "pk_roleId", "PersonName", donor.cid);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", donor.fk_stateid);
            ViewBag.fk_donor_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userid), "pk_orgid", "org_name", donor.fk_donor_orgid);
            return View(donor);
        }



        [HttpPost]        
        public JsonResult Contact(string[] EmailList)
        {
           
            var userid = User.Identity.GetUserId();
            var username = db.Org_details.Where(r => r.AspNetUser.Id == userid).Select(r => r.org_name).FirstOrDefault();
            int result = 0;

            if (ModelState.IsValid)
            {
                foreach (var email in EmailList)
                {
                    var Email = email.Trim().Replace("\n","");
                    var body = "<h4>Email From:"+username+"</h4><h4>Message:Your Blood Group is urgently required<br/>" +
                        "If you are available kindly contact us quickly. As Your step can save someone's life.</h4>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(Email)); //replace with valid value
                    message.Subject = "Urgently Require Blood";                   
                    message.Body = string.Format(body);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);

                    }
                    result +=1;
                }

                
            }
            return Json(result);


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
