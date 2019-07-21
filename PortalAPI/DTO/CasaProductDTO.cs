using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class CasaProductDTO
    {
        public string ProductCode { get; set; }
        public string TypeOfAccount { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> MinimumDeposit { get; set; }
        public Nullable<decimal> MinimumBalance { get; set; }
        public Nullable<decimal> HoldAmount { get; set; }
        public Nullable<decimal> Divisor { get; set; }
        public Nullable<decimal> TaxFactor { get; set; }
        public string ComputeFrequency { get; set; }
        public Nullable<decimal> ServiceChargeActive { get; set; }
        public Nullable<decimal> ServiceChargeDormant { get; set; }
        public Nullable<int> DaysDormant { get; set; }
        public Nullable<int> Terms { get; set; }
    }
}