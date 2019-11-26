using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.Areas.ADMIN.Models
{
    public class CountViewModel
    {
        [Display(Name = "Organization Name")]
        public string org_name { get; set; }
        public int Donorcount { get; set; }
        public int fk_donor_orgid { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int org_id { get; set; }
        public bool isactive { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Display(Name = "Email")]
        public string fk_id { get; set; }
        public string State { get; set; }
    }
}