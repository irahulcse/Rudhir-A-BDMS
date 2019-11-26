using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationManagementSystem;
using PagedList;
using BloodDonationManagementSystem.Areas.ADMIN.Models;

namespace BloodDonationManagementSystem.Areas.ADMIN.Controllers
{
    public class CreateRolesController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/CreateRoles
        public ActionResult Index()
        {
            return View(db.RoleTypes.ToList());
        }

        // GET: ADMIN/CreateRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADMIN/CreateRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,type")] RoleType roleType)
        {
            if (ModelState.IsValid)
            {
                db.RoleTypes.Add(roleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roleType);
        }

        // GET: ADMIN/CreateRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleType roleType = db.RoleTypes.Find(id);
            if (roleType == null)
            {
                return HttpNotFound();
            }
            return View(roleType);
        }

        // POST: ADMIN/CreateRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,type")] RoleType roleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roleType);
        }

        // GET: ADMIN/CreateRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleType roleType = db.RoleTypes.Find(id);
            if (roleType == null)
            {
                return HttpNotFound();
            }
            return View(roleType);
        }

        // POST: ADMIN/CreateRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoleType roleType = db.RoleTypes.Find(id);
            db.RoleTypes.Remove(roleType);
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
