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
    
    public partial class MembershipPaymentFee
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsShareCapital { get; set; }
        public Nullable<decimal> ShareCapitalParameter { get; set; }
    }
}
