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
    
    public partial class Donor
    {
        public int pk_id { get; set; }
        [Display(Name = "Organization Name")]
        public int fk_donor_orgid { get; set; }
        public string DonorName { get; set; }
        [Display(Name = "Email Id")]
        public string DonorEmail { get; set; }
        [Display(Name = "Blood Group")]
        public int bid { get; set; }
        [Display(Name = "Gender")]
        public int gid { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DOB { get; set; }
        public string ContactNo { get; set; }
        [Display(Name = "State")]
        public int fk_stateid { get; set; }
        [Display(Name = "Aadhar Card No")]
        public string Aadhar_Card_No { get; set; }
        public bool IsActive { get; set; }
        [Display(Name ="Created By")]
        public int cid { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime CreatedTime { get; set; }
    
        public virtual Blood_Gp Blood_Gp { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Org_roles Org_roles { get; set; }
        public virtual State State { get; set; }
        public virtual Org_details Org_details { get; set; }
    }
}
