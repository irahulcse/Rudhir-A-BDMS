using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationManagementSystem;
using Microsoft.AspNet.Identity;

namespace BloodDonationManagementSystem.Controllers
{
    [Authorize]
    public class Indv_DonorController : Controller
    {
        private Entities db = new Entities();

        // GET: Indv_Donor
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var individual_Donor = from r in (db.Individual_Donor.Include(i => i.AspNetUser).Include(i => i.Blood_Gp).Include(i => i.Gender).Where(r => r.fk_id == userid))
                                   select r;
            return View(individual_Donor.ToList());
        }


        // GET: Indv_Donor/Create
        public ActionResult Create()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.fk_id = userid;
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name");
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1");
            ViewBag.fkstateid = new SelectList(db.States, "stateid", "StateName");
            return View();
        }

        // POST: Indv_Donor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DonorName,bid,gid,DOB,fkstateid,PhoneNo,AadharCardNo,IsActive,CreatedTime,fk_id")] Individual_Donor individual_Donor)
        {
            var userid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Individual_Donor.Add(individual_Donor).fk_id = userid;
                db.Individual_Donor.Add(individual_Donor).CreatedTime = DateTime.Now;
                db.Individual_Donor.Add(individual_Donor).fk_roleid = "3";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_id = userid;
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", individual_Donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", individual_Donor.gid);
            ViewBag.fkstateid = new SelectList(db.States, "stateid", "StateName", individual_Donor.fkstateid);
            return View(individual_Donor);
        }

        // GET: Indv_Donor/Edit/5
        public ActionResult Edit(int? id)
        {
            var userx = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Individual_Donor individual_Donor = db.Individual_Donor.Find(id);
            if (individual_Donor == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_id = userx;
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", individual_Donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", individual_Donor.gid);
            ViewBag.fkstateid = new SelectList(db.States, "stateid", "StateName", individual_Donor.fkstateid);
            return View(individual_Donor);
        }

        // POST: Indv_Donor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DonorName,bid,gid,DOB,fkstateid,PhoneNo,AadharCardNo,IsActive,CreatedTime,fk_id")] Individual_Donor individual_Donor)
        {
            var userx = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                individual_Donor.fk_roleid = "3";
                individual_Donor.CreatedTime = DateTime.Now;
                db.Entry(individual_Donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_id = userx;
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", individual_Donor.bid);
            ViewBag.gid = new SelectList(db.Genders, "Id", "gender1", individual_Donor.gid);
            ViewBag.fkstateid = new SelectList(db.States, "stateid", "StateName", individual_Donor.fkstateid);
            return View(individual_Donor);
        }

     
        public ActionResult BloodBankIndex(string id)
        {
            var bloodBanks = db.BloodBanks.Include(b => b.State).Where(r=>r.State.StateName==id).ToList();
            
            return View(bloodBanks);
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
