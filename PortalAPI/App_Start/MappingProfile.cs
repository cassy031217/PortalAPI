using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PortalAPI.DTO;
using PortalAPI.Models;

namespace PortalAPI.App_Start
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping Profile
        /// </summary>
        public MappingProfile()
        {
            Mapper.CreateMap<Region, RegionDTO>();
            Mapper.CreateMap<RegionDTO, Region>();

            Mapper.CreateMap<Province, ProvinceDTO>();
            Mapper.CreateMap<ProvinceDTO, Province>();

            Mapper.CreateMap<CityMunicipality, CityMunicipalityDTO>();
            Mapper.CreateMap<CityMunicipalityDTO, CityMunicipality>();

            Mapper.CreateMap<Barangay, BarangaysDTO>();
            Mapper.CreateMap<BarangaysDTO, Barangay>();

            Mapper.CreateMap<LoanApplication, LoanApplicationDTO>();
            Mapper.CreateMap<LoanApplicationDTO, LoanApplication>()
                .ForMember(c => c.LAno, opt => opt.Ignore());

            Mapper.CreateMap<LoanProductsMaster, LoanProductsMasterDTO>();
            Mapper.CreateMap<LoanProductsMasterDTO, LoanProductsMaster>()
                .ForMember(x => x.ProductId, opt => opt.Ignore());

            Mapper.CreateMap<MembershipPaymentFee, MembershipPaymentFeeDTO>();
            Mapper.CreateMap<MembershipPaymentFeeDTO, MembershipPaymentFee>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            Mapper.CreateMap<MemberFamily, MemberFamilyDTO>();
            Mapper.CreateMap<MemberFamilyDTO, MemberFamily>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MemberDependent, MemberDependentDTO>();
            Mapper.CreateMap<MemberDependentDTO, MemberDependent>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MembershipPayment, MembershipPaymentDTO>();
            Mapper.CreateMap<MembershipPaymentDTO, MembershipPayment>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MemberAddress, MemberAddressDTO>();
            Mapper.CreateMap<MemberAddressDTO, MemberAddress>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<Membership, MembershipDTO>();
            Mapper.CreateMap<MembershipDTO, Membership>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<CIFOnlineUser, CIFOnlineUserDTO>();
            Mapper.CreateMap<CIFOnlineUserDTO, CIFOnlineUser>()
                .ForMember(m => m.ID, opt => opt.Ignore());

            Mapper.CreateMap<CasaProduct, CasaProductDTO>();
            Mapper.CreateMap<CasaProductDTO, CasaProduct>();

            Mapper.CreateMap<CasaAccount, CasaAccountDTO>();
            Mapper.CreateMap<CasaAccountDTO, CasaAccount>();

            Mapper.CreateMap<CasaLedger, CasaLedgerDTO>();
            Mapper.CreateMap<CasaLedgerDTO, CasaLedger>();

            Mapper.CreateMap<rptsrc_StatementofAccountLoans, LoanStatementofAccountDTO>();
            Mapper.CreateMap<LoanStatementofAccountDTO, rptsrc_StatementofAccountLoans>()
                .ForMember(x => x.id, opt => opt.Ignore());

            Mapper.CreateMap<rptsrc_StatementofAccountSavings, SavingStatementofAccountDTO>();
            Mapper.CreateMap<SavingStatementofAccountDTO, rptsrc_StatementofAccountSavings>()
                .ForMember(x => x.id, opt => opt.Ignore());

            Mapper.CreateMap<LoanAccount, LoanAccountDTO>();
            Mapper.CreateMap<LoanAccountDTO, LoanAccount>();

            Mapper.CreateMap<LoanLedger, LoanLedgerDTO>();
            Mapper.CreateMap<LoanLedgerDTO, LoanLedger>();

        }
    }
}