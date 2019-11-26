using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationManagementSystem;


namespace BloodDonationManagementSystem.Controllers
{
    public class MessagesController : Controller
    {
        private Entities db = new Entities();

       
        
        // GET: Messages/Create
        public ActionResult Create(int idx)
        {
            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name");
            ViewBag.fk_orgid = new SelectList(db.Org_details.Where(r=>r.pk_orgid==idx), "pk_orgid", "org_name");
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonName,Email,Mobile_No,bid,message1,fk_stateid,fk_orgid,created_time")] Message message,int idx)
        {
            if (ModelState.IsValid)
            {
                
                db.Messages.Add(message).created_time = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.bid = new SelectList(db.Blood_Gp, "Id", "Name", message.bid);
            ViewBag.fk_orgid = new SelectList(db.Org_details.Where(r => r.pk_orgid == idx), "pk_orgid", "org_name", message.fk_orgid);
            ViewBag.fk_stateid = new SelectList(db.States, "stateid", "StateName", message.fk_stateid);
            return View(message);
            
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
