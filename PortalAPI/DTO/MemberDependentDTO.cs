using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class MemberDependentDTO
    {
        public int Id { get; set; }
        public Nullable<int> MembershipID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string EducAttainment { get; set; }
    }
}