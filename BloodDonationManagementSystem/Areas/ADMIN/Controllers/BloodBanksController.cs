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
using PagedList;

namespace BloodDonationManagementSystem.Areas.ADMIN.Controllers
{
    public class BloodBanksController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/BloodBanks
        public ActionResult Index(int page = 1)
        {
            var bloodBanks = db.BloodBanks.Include(b => b.State).OrderBy(r=>r.State.StateName).ToPagedList(page, 8);
            return View(bloodBanks);
        }

        // GET: ADMIN/BloodBanks/Create
        public ActionResult Create()
        {
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName");
            return View();
        }

        // POST: ADMIN/BloodBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,fk_stateid")] BloodBank bloodBank)
        {
            if (ModelState.IsValid)
            {
                db.BloodBanks.Add(bloodBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", bloodBank.fk_stateid);
            return View(bloodBank);
        }

        // GET: ADMIN/BloodBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", bloodBank.fk_stateid);
            return View(bloodBank);
        }

        // POST: ADMIN/BloodBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,fk_stateid")] BloodBank bloodBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloodBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", bloodBank.fk_stateid);
            return View(bloodBank);
        }

        // GET: ADMIN/BloodBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodBank bloodBank = db.BloodBanks.Find(id);
            if (bloodBank == null)
            {
                return HttpNotFound();
            }
            return View(bloodBank);
        }

        // POST: ADMIN/BloodBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloodBank bloodBank = db.BloodBanks.Find(id);
            db.BloodBanks.Remove(bloodBank);
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
