using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class MembershipPaymentFeeDTO
    {
        /// <summary>
        /// Membership Payment Fee Id
        /// </summary>
        public int Id { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Payment Amount
        /// </summary>
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }

        /// <summary>
        /// For Share Capital
        /// </summary>
        public Nullable<bool> IsShareCapital { get; set; }
        /// <summary>
        /// Share Capital parameter for division or mutiplication 
        /// </summary>
        public Nullable<decimal> ShareCapitalParameter { get; set; }
    }
}