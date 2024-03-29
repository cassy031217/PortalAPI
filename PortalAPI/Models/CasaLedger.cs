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
    
    public partial class CasaLedger
    {
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public decimal SequenceNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string Description { get; set; }
        public string Mnemonic { get; set; }
        public Nullable<decimal> BeginningBalance { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> EndingBalance { get; set; }
    }
}
