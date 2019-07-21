using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class CasaLedgerDTO
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