using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BloodDonationManagementSystem.Models
{
    public class Blood_Org
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Stock { get; set; }
        public int id { get; set; }
        public string Blood_Group { get; set; }
        public string state { get; set; }
        public string Mobile { get; set; }
    }
}