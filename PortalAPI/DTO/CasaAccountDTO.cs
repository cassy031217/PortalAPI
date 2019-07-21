using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class CasaAccountDTO
    {
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public string CIFkey { get; set; }
        public string AccountName { get; set; }
        public string TypeOfAccount { get; set; }
        public string AccountStatus { get; set; }
        public string ProductCode { get; set; }
        public Nullable<System.DateTime> DateOpen { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public string PassbookNumber { get; set; }
        public Nullable<decimal> AccountBalance { get; set; }
        public Nullable<decimal> HoldBalance { get; set; }
        public Nullable<System.DateTime> MaturityDate { get; set; }
        public Nullable<decimal> Principal { get; set; }
        public Nullable<int> Terms { get; set; }
        public Nullable<double> InterestRate { get; set; }
    }
}