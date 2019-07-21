using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class MemberFamilyDTO
    {
        public int Id { get; set; }
        public int MembershipID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Relationship { get; set; }
    }
}