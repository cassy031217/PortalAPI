using System;
using System.Linq;
using PortalAPI.Models;

namespace PortalAPI.DTO
{
    public class MembershipFactory
    {
        public MembershipDTO Create(Membership personal)
        {
            
            return new MembershipDTO()
            {
                Id = personal.Id,
                LastName = personal.LastName ?? "",
                FirstName = personal.FirstName ?? "",
                MiddleName = personal.MiddleName ?? "",
                BirthDate = (DateTime) personal.BirthDate,
                BirthPlace = personal.BirthPlace ?? "",
                Gender = personal.Gender ?? "",
                Citizenship = personal.Citizenship ?? "",
                CivilStatus = personal.CivilStatus ?? "" ,
                Religion = personal.Religion ?? "",
                EducAttainment = personal.EducAttainment ?? "",
                Degree = personal.Degree ?? "",
                ContactNumber = personal.ContactNumber ?? "",
                EmailAddress = personal.EmailAddress ?? "",
                Occupation = personal.Occupation ?? "",
                Height = personal.Height,
                Weight = personal.Weight,
                TIN = personal.TIN ?? "",
                SSS = personal.SSS ?? "",
                EmergencyContactPerson = personal.EmergencyContactPerson ?? "",
                EmergencyContactNo = personal.EmergencyContactNo ?? "",
                ApplicationStage = personal.ApplicationStage ?? "",
                ApplicationStatus = personal.ApplicationStatus ?? "",
                PMESDate = personal.PMESDate,
                PMESType = personal.PMESType,
                Remarks = personal.Remarks ?? "",
                ApplicationName = personal.ApplicationName ?? "",
                ApplicationContentType = personal.ApplicationContent == null ? "" : personal.ApplicationContentType,
                ApplicationContent = personal.ApplicationContent,
                MemberIDPictureName = personal.MemberIDPictureName ?? "",
                MemberIDPictureContentType = personal.MemberIDPictureContentType ?? "",
                MemberIDPictureContent = personal.MemberIDPictureContent,
                MemberValidIdContent = personal.MemberIDPictureContent,
                MemberValidIdName = personal.MemberValidIdName ?? "",
                MemberValidIdContentType = personal.MemberValidIdContentType ?? "",
                PaymentName = personal.PaymentName ?? "",
                PaymentContentType = personal.PaymentContentType ?? "",
                PaymentContent = personal.PaymentContent,
                IsExists = personal.IsExists,
                ApplicationDate = personal.ApplicationDate,
                Address = personal.MemberAddresses?.Select(Create).ToList(),
                Families = personal.MemberFamilies?.Select(Create).ToList(),
                Dependent = personal.MemberDependents?.Select(Create).ToList(),
                Payment = personal.MembershipPayments?.Select(Create).ToList()
            };
        }

        public MemberDependentDTO Create(MemberDependent personalDependent)
        {
            return new MemberDependentDTO()
            {
                Id = personalDependent.Id,
                MembershipID = personalDependent.MembershipID,
                Name = personalDependent.Name ?? "",
                BirthDate = personalDependent.BirthDate,
                EducAttainment = personalDependent.EducAttainment ?? ""
            };
        }
        public MemberAddressDTO Create(MemberAddress personalAddres)
        {
            return new MemberAddressDTO()
            {
                Id = personalAddres.Id,
                MembershipID = personalAddres.MembershipID,
                ProvCode = personalAddres.ProvCode ?? "",
                CityMunCode = personalAddres.CityMunCode ?? "",
                BrgyCode = personalAddres.BrgyCode ?? "",
                Zipcode = personalAddres.Zipcode ?? "",
                BuildingName = personalAddres.BuildingName ?? "",
                Street = personalAddres.Street ?? "",
                IsOwned = personalAddres.IsOwned ?? false,
                IsRent = personalAddres.IsRent ?? false,
                NumberofRentYear = personalAddres.NumberofRentYear ?? 0,
                IsLivingwParent = personalAddres.IsLivingwParent ?? false,
                IsOther = personalAddres.IsOther ?? false,
                Other = personalAddres.Other ?? "",
                AddressType = personalAddres.AddressType ?? ""
            };
        }
        public MemberFamilyDTO Create(MemberFamily personalFamily)
        {
            return new MemberFamilyDTO()
            {
                Id = personalFamily.Id,
                MembershipID = personalFamily.MembershipID,
                Name = personalFamily.Name ?? "",
                BirthDate = personalFamily.BirthDate,
                ContactNumber = personalFamily.ContactNumber ?? "",
                Address = personalFamily.Address ?? "",
                Relationship = personalFamily.Relationship ?? ""
            };
        }

        public MembershipPaymentDTO Create(MembershipPayment membership)
        {
            return new MembershipPaymentDTO()
            {
                Id = membership.Id,
                MembershipID = membership.MembershipID,
                Description = membership.Description ?? "",
                Amount = membership.Amount,
                IsShareCapital = membership.IsShareCapital,
                ShareCapitalParameter = membership.ShareCapitalParameter,
                Remarks =  membership.Remarks ?? ""
            };
        }
    }
}