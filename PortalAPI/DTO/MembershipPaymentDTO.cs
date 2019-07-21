using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class MembershipPaymentDTO
    {
        public int Id { get; set; }
        public int? MembershipID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }

        public Nullable<bool> IsShareCapital { get; set; }
        public Nullable<decimal> ShareCapitalParameter { get; set; }

        public string Remarks { get; set; }

    }
}