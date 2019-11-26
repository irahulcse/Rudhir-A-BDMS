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
    public class OrgRolesController : Controller
    {
        private Entities db = new Entities();

        // GET: OrgRoles
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();

            var org_roles = from r in (db.Org_roles.Include(o => o.Org_details).Include(o => o.RoleType).Where(r => r.Org_details.fk_id == userid))
                       select(r) ;
            return View(org_roles.ToList());
        }

        // GET: OrgRoles/Create
        public ActionResult Create()
        {
            var userx = User.Identity.GetUserId();
            ViewBag.fk_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userx), "pk_orgid", "org_name");
            ViewBag.tid = new SelectList(db.RoleTypes, "Id", "type");
            return View();
        }

        // POST: OrgRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pk_roleId,tid,PersonName,fk_orgid")] Org_roles org_roles)
        {
            var userid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Org_roles.Add(org_roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userid), "pk_orgid", "org_name", org_roles.fk_orgid);
            ViewBag.tid = new SelectList(db.RoleTypes, "Id", "type", org_roles.tid);
            return View(org_roles);
        }

        // GET: OrgRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            var userid = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org_roles org_roles = db.Org_roles.Find(id);
            if (org_roles == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userid), "pk_orgid", "org_name", org_roles.fk_orgid);
            ViewBag.tid = new SelectList(db.RoleTypes, "Id", "type", org_roles.tid);
            return View(org_roles);
        }

        // POST: OrgRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pk_roleId,tid,PersonName,fk_orgid")] Org_roles org_roles)
        {
            var userid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(org_roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_orgid = new SelectList(db.Org_details.Where(r => r.fk_id == userid), "pk_orgid", "org_name", org_roles.fk_orgid);
            ViewBag.tid = new SelectList(db.RoleTypes, "Id", "type", org_roles.tid);
            return View(org_roles);
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
