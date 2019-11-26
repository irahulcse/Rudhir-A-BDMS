using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodDonationManagementSystem.Models;
using PagedList;

namespace BloodDonationManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            var x = db.Events.Select(r => r);
            return View(x.ToList());
        }


        [Authorize]
        [HttpGet]
        public ActionResult Contact()
        {
           return View();
        }
        


        [HttpGet]
        public ActionResult UserNeedBlood(string gp,int page=1)
        {
            
            var xy = (from r in db.Donors.Where(r => r.Blood_Gp.Name == gp||(gp==null))
                      group new { r } by new { r.Org_details.org_name,  r.Blood_Gp.Name,r.State.StateName,r.Org_details.AspNetUser.Email,r.fk_donor_orgid  }
                      into g
                      select new 
                      {
                          Name = g.Key.org_name,
                          email=g.Key.Email,
                          Blood = g.Key.Name,
                          state = g.Key.StateName,                                           
                          Phone="--",
                          STOCK = g.Count(),
                          Id = g.Key.fk_donor_orgid
                      }).ToList();

            var bp = (from p in db.Individual_Donor.Where(p => p.Blood_Gp.Name == gp || (gp == null))

                      group new {  p } by new { p.DonorName,p.Blood_Gp.Name,p.State.StateName,p.AspNetUser.Email,p.PhoneNo } into g
                      select new
                       {
                          Name = g.Key.DonorName,
                          email = g.Key.Email,
                          Blood = g.Key.Name,
                          state = g.Key.StateName,
                          Phone = g.Key.PhoneNo,
                          STOCK = g.Count()  ,
                          Id =0
                          
                        }).ToList();

            var z = xy.Union(bp);
            var mn = (from a in z
                      select new Blood_Org()

                      {
                          Name = a.Name,
                          Email=a.email,
                          Blood_Group = a.Blood,
                          state = a.state,
                          Mobile=a.Phone,
                          Stock = a.STOCK,
                          id=a.Id
                      }).OrderBy(r => r.Stock).ToPagedList(page, 3);
            return View(mn);
        }

        public ActionResult Count(int page = 1)
        {

            var ap = (from r in db.Donors
                      join p in db.Blood_Gp on r.bid equals p.Id
                      group new { r, p } by new { r.bid, p.Name }
                      into g
                      select new
                      { Name = g.Key.Name, STOCK = g.Count() }).ToList();

            var bp = (from r in db.Individual_Donor
                      join p in db.Blood_Gp on r.bid equals p.Id
                      group new { r, p } by new { r.bid, p.Name } into g
                      select new
                      { Name = g.Key.Name, STOCK = g.Count() }).ToList();


            var zx = ap.Union(bp);
            var bg = (from a in zx
                      group new { a } by new { a.Name } into g
                      select new Blood_Count()
                      {
                          Blood_Group = g.Key.Name,
                          Stock = (Int32)(g.Sum(c => c.a.STOCK))
                      }).OrderBy(r => r.Stock).ToPagedList(page, 3);



            return View(bg);
        }

    }
}