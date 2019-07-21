using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.Models
{
    public class Parameters
    {
    }

    public class StatementofAccountParameter
    {
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class CasaAccountParameter
    {
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public string cifkey { get; set; }
    }

    /// <summary>
    /// Membership Controller Parameters
    /// </summary>
    public class GetMembershipSatus
    {
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }


    }


}