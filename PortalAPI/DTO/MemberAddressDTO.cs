using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class MemberAddressDTO
    {
        public int Id { get; set; }
        public int MembershipID { get; set; }
        public string ProvCode { get; set; }
        public string CityMunCode { get; set; }
        public string BrgyCode { get; set; }
        public string Zipcode { get; set; }
        public string BuildingName { get; set; }
        public string Street { get; set; }
        public Nullable<bool> IsOwned { get; set; }
        public Nullable<bool> IsRent { get; set; }
        public Nullable<decimal> NumberofRentYear { get; set; }
        public Nullable<bool> IsLivingwParent { get; set; }
        public Nullable<bool> IsOther { get; set; }
        public string Other { get; set; }
        public string AddressType { get; set; }
    }
}