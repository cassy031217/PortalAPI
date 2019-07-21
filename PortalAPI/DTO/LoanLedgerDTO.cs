using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class LoanLedgerDTO
    {
        public int id { get; set; }
        public Nullable<int> LoanAccountId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public string PrincipalBeginning { get; set; }
        public Nullable<decimal> Principal { get; set; }
        public Nullable<decimal> PrincipalEnding { get; set; }
        public string InterestBeginning { get; set; }
        public Nullable<decimal> Interest { get; set; }
        public Nullable<decimal> InterestEnding { get; set; }
        public Nullable<decimal> Penalty { get; set; }
    }
}