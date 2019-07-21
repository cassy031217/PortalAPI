using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class LoanAccountDTO
    {
        public int id { get; set; }
        public string CIFKey { get; set; }
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<decimal> PrincipalBalance { get; set; }
        public Nullable<decimal> InterestBalance { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> MaturityDate { get; set; }
        public string AccountStatus { get; set; }
        public string ModeOfPayment { get; set; }
        public string FrequencyOfPayment { get; set; }
        public Nullable<int> NumberOfPayments { get; set; }
        public Nullable<decimal> InterestRate { get; set; }
        public string InterestMOP { get; set; }
        public string InterestFrequency { get; set; }
        public string InterestComputationMethod { get; set; }
        public string InterestComputationFrequency { get; set; }
    }
}