//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberFamily
    {
        public int Id { get; set; }
        public int MembershipID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Relationship { get; set; }
    
        public virtual Membership Membership { get; set; }
    }
}
