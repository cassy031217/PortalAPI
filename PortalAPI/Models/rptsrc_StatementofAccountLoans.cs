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
    
    public partial class rptsrc_StatementofAccountLoans
    {
        public int id { get; set; }
        public Nullable<int> LoanAccountId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string LoanDue { get; set; }
        public Nullable<decimal> PrincipalDue { get; set; }
        public Nullable<decimal> InterestDue { get; set; }
        public Nullable<decimal> PenaltyDue { get; set; }
    
        public virtual LoanAccount LoanAccount { get; set; }
    }
}
