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

namespace BloodDonationManagementSystem.Areas.ADMIN.Controllers
{
    public class Admin_view_indvController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/Admin_view_indv
        public ActionResult Index(string gp,string name,int page=1)
        {
            ViewBag.gp = db.Blood_Gp.Select(r => r.Name).Distinct();
            var individual_Donor = db.Individual_Donor.Include(i => i.AspNetUser).Include(i => i.Blood_Gp)
                .Include(i => i.Gender).Include(i=>i.State).Where(r => r.Blood_Gp.Name == gp || (gp == null))
                .Where(r=>r.DonorName==name||(name==null))
                .OrderBy(r=>r.DonorName).ToPagedList(page,3);
            return View(individual_Donor);
        }

        public ActionResult Org_donor(string gp, int page = 1)
        {
            
            ViewBag.gp = db.Blood_Gp.Select(r => r.Name).Distinct();
            var donors = db.Donors.Include(d => d.Blood_Gp).Include(d => d.Gender)
                .Include(d => d.Org_roles).Include(d => d.Org_details).Include(d=>d.State)
                .Where(r => r.Blood_Gp.Name == gp || (gp == null)).Select(r => r).OrderBy(r => r.DonorName).ToPagedList(page, 3);

            return View(donors);
        }
           

        public ActionResult MessageIndex(string gp,int page = 1)
        {
            ViewBag.gp = db.Blood_Gp.Select(r => r.Name).Distinct();
            var messages = db.Messages.Include(m => m.Blood_Gp).Where(m => m.Blood_Gp.Name == gp || (gp == null))
                .Include(m => m.Org_details).Include(m => m.State).OrderBy(r=>r.PersonName).ToPagedList(page,3);
              return View(messages);
           
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
