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
                LastName = personal.LastName,
                FirstName = personal.FirstName,
                MiddleName = personal.MiddleName == null ? "" : personal.MiddleName,
                BirthDate = (DateTime) personal.BirthDate,
                BirthPlace = personal.BirthPlace == null ? "" : personal.BirthPlace,
                Gender = personal.Gender == null ? "" : personal.Gender,
                Citizenship = personal.Citizenship == null ? "" : personal.Citizenship,
                CivilStatus = personal.CivilStatus == null ? "" : personal.CivilStatus ,
                Religion = personal.Religion == null ? "" : personal.Religion,
                EducAttainment = personal.EducAttainment == null ? "" : personal.EducAttainment,
                Degree = personal.Degree == null ? "" : personal.Degree,
                ContactNumber = personal.ContactNumber == null ? "" : personal.ContactNumber,
                EmailAddress = personal.EmailAddress == null ? "" : personal.EmailAddress,
                Occupation = personal.Occupation == null ? "" : personal.Occupation,
                Height = personal.Height,
                Weight = personal.Weight,
                TIN = personal.TIN == null ? "" : personal.TIN,
                SSS = personal.SSS == null ? "" : personal.SSS,
                EmergencyContactPerson = personal.EmergencyContactPerson == null ? "" : personal.EmergencyContactPerson,
                EmergencyContactNo = personal.EmergencyContactNo == null ? "" : personal.EmergencyContactNo,
                ApplicationStage = personal.ApplicationStage == null ? "" : personal.ApplicationStage,
                ApplicationStatus = personal.ApplicationStatus == null ? "" : personal.ApplicationStatus,
                PMESDate = personal.PMESDate,
                PMESType = personal.PMESType,
                Remarks = personal.Remarks == null ? "" : personal.Remarks,
                ApplicationName = personal.ApplicationName == null ? "" : personal.ApplicationName,
                ApplicationContentType = personal.ApplicationContent == null ? "" : personal.ApplicationContentType,
                ApplicationContent = personal.ApplicationContent,
                MemberIDPictureName = personal.MemberIDPictureName == null ? "" : personal.MemberIDPictureName,
                MemberIDPictureContentType = personal.MemberIDPictureContentType == null ? "" : personal.MemberIDPictureContentType,
                MemberIDPictureContent = personal.MemberIDPictureContent,
                MemberValidIdContent = personal.MemberIDPictureContent,
                MemberValidIdName = personal.MemberValidIdName == null ? "" : personal.MemberValidIdName,
                MemberValidIdContentType = personal.MemberValidIdContentType == null ? "" : personal.MemberValidIdContentType,
                PaymentName = personal.PaymentName == null ? "" : personal.PaymentName,
                PaymentContentType = personal.PaymentContentType == null ? "" : personal.PaymentContentType,
                PaymentContent = personal.PaymentContent,
                IsExists = personal.IsExists,
                ApplicationDate = personal.ApplicationDate,
                Address = personal.MemberAddresses == null ? null : personal.MemberAddresses.Select(a => Create(a)).ToList(),
                Families = personal.MemberFamilies == null ? null : personal.MemberFamilies.Select(x => Create(x)).ToList(),
                Dependent = personal.MemberDependents == null ? null : personal.MemberDependents.Select(d => Create(d)).ToList(),
                Payment = personal.MembershipPayments == null ? null : personal.MembershipPayments.Select(m => Create(m)).ToList()
            };
        }

        public MemberDependentDTO Create(MemberDependent personalDependent)
        {
            return new MemberDependentDTO()
            {
                Id = personalDependent.Id,
                MembershipID = personalDependent.MembershipID,
                Name = personalDependent.Name,
                BirthDate = personalDependent.BirthDate,
                EducAttainment = personalDependent.EducAttainment
            };
        }
        public MemberAddressDTO Create(MemberAddress personalAddres)
        {
            return new MemberAddressDTO()
            {
                Id = personalAddres.Id,
                MembershipID = personalAddres.MembershipID,
                ProvCode = personalAddres.ProvCode,
                CityMunCode = personalAddres.CityMunCode,
                BrgyCode = personalAddres.BrgyCode,
                Zipcode = personalAddres.Zipcode,
                BuildingName = personalAddres.BuildingName,
                Street = personalAddres.Street,
                IsOwned = personalAddres.IsOwned == null ? false : personalAddres.IsOwned,
                IsRent = personalAddres.IsRent == null ? false : personalAddres.IsRent,
                NumberofRentYear = personalAddres.NumberofRentYear == null ? 0 : personalAddres.NumberofRentYear,
                IsLivingwParent = personalAddres.IsLivingwParent == null ? false : personalAddres.IsLivingwParent,
                IsOther = personalAddres.IsOther == null ? false : personalAddres.IsOther,
                Other = personalAddres.Other,
                AddressType = personalAddres.AddressType
            };
        }
        public MemberFamilyDTO Create(MemberFamily personalFamily)
        {
            return new MemberFamilyDTO()
            {
                Id = personalFamily.Id,
                MembershipID = personalFamily.MembershipID,
                Name = personalFamily.Name,
                BirthDate = personalFamily.BirthDate,
                ContactNumber = personalFamily.ContactNumber,
                Address = personalFamily.Address,
                Relationship = personalFamily.Relationship
            };
        }

        public MembershipPaymentDTO Create(MembershipPayment membership)
        {
            return new MembershipPaymentDTO()
            {
                Id = membership.Id,
                MembershipID = membership.MembershipID,
                Description = membership.Description,
                Amount = membership.Amount,
                IsShareCapital = membership.IsShareCapital,
                ShareCapitalParameter = membership.ShareCapitalParameter,
                Remarks =  membership.Remarks
            };
        }
    }
}