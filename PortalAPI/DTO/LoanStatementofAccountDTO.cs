using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class LoanStatementofAccountDTO
    {
        public int id { get; set; }
        public Nullable<int> LoanAccountId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string LoanDue { get; set; }
        public Nullable<decimal> PrincipalDue { get; set; }
        public Nullable<decimal> InterestDue { get; set; }
        public Nullable<decimal> PenaltyDue { get; set; }
    }
}