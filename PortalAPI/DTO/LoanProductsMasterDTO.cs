using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class LoanProductsMasterDTO
    {
        /// <summary>
        /// Product Code
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }
        public string ModeOfPayment { get; set; }
        public string FrequencyOfPayment { get; set; }
        public Nullable<int> NumberOfPayments { get; set; }
        public Nullable<decimal> InterestRate { get; set; }
        public string InterestModeofPayment { get; set; }
        public string InterestComputationMethod { get; set; }
        public string InterestFrequency { get; set; }
        public string InterestComputationFrequency { get; set; }
        public Nullable<int> AdvancedInterest { get; set; }
    }
}