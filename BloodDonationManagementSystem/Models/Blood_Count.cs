using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.Models
{
    public class Blood_Count
    {
        [Display(Name ="Blood Group")]
        public string Blood_Group { get; set; }
        public int Stock { get; set; }
  
    }
}