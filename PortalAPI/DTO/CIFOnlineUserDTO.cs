using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class CIFOnlineUserDTO
    {
        public string MemberCode { get; set; }
        public string CIFkey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Sex { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<bool> ConfirmedEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BranchCode { get; set; }
        public Nullable<bool> IsGlobalAdministrator { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<bool> IsOnLine { get; set; }
        public string AccountName { get; set; }
        public Nullable<System.Guid> GUID { get; set; }
        public string AccountStatus { get; set; }
        public Nullable<System.Guid> ActivationCode { get; set; }
        public int ID { get; set; }
        public Nullable<bool> EmailTagging { get; set; }
    }
}