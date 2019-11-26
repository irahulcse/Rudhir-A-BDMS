//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodDonationManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Message
    {
        public int Id { get; set; }
        [Display(Name ="Your Name")]
        [Required(ErrorMessage = "Name is required")]
        public string PersonName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile No is required")]
        [Display(Name = "Mobile No")]
        public long Mobile_No { get; set; }
        [Display(Name = "Blood Group")]
        public int bid { get; set; }
        [Display(Name = "Message")]
        [Required(ErrorMessage = "Message is required")]
        public string message1 { get; set; }
        [Display(Name = "State")]
        public int fk_stateid { get; set; }
        [Display(Name = "Contacted Organization")]
        public int fk_orgid { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime created_time { get; set; }
    
        public virtual Blood_Gp Blood_Gp { get; set; }
        public virtual State State { get; set; }
        public virtual Org_details Org_details { get; set; }
    }
}