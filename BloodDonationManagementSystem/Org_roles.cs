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

    public partial class Org_roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Org_roles()
        {
            this.Donors = new HashSet<Donor>();
        }
    
        public int pk_roleId { get; set; }
        [Display(Name = "Role Type")]
        public int tid { get; set; }
        public string PersonName { get; set; }
        [Display(Name = "Organization Name")]
        public int fk_orgid { get; set; }
    
        public virtual RoleType RoleType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donor> Donors { get; set; }
        public virtual Org_details Org_details { get; set; }
    }
}
