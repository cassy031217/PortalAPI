using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalAPI.DTO
{
    public class MembershipDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string CivilStatus { get; set; }
        public string Religion { get; set; }
        public string EducAttainment { get; set; }
        public string Degree { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Occupation { get; set; }
        public Nullable<decimal> Height { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string EmergencyContactNo { get; set; }
        public string ApplicationStage { get; set; }
        public string ApplicationStatus { get; set; }
        public Nullable<System.DateTime> PMESDate { get; set; }
        public Nullable<bool> PMESType { get; set; }
        public string Remarks { get; set; }
        public string ApplicationName { get; set; }
        public byte[] ApplicationContent { get; set; }
        public string MemberIDPictureName { get; set; }
        public byte[] MemberIDPictureContent { get; set; }
        public string MemberValidIdName { get; set; }
        public byte[] MemberValidIdContent { get; set; }
        public string PaymentName { get; set; }
        public byte[] PaymentContent { get; set; }
        public string ApplicationContentType { get; set; }
        public string MemberIDPictureContentType { get; set; }
        public string MemberValidIdContentType { get; set; }
        public string PaymentContentType { get; set; }
        public Nullable<bool> IsExists { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public List<MembershipPaymentDTO> Payment { get; set; }
        public virtual List<MemberAddressDTO> Address { get; set; }

        public virtual List<MemberDependentDTO> Dependent { get; set; }

        public virtual List<MemberFamilyDTO> Families { get; set; }
    }
}