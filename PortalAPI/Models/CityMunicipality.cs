//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CityMunicipality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CityMunicipality()
        {
            this.MemberAddresses = new HashSet<MemberAddress>();
        }
    
        public int id { get; set; }
        public string psgcCode { get; set; }
        public string Description { get; set; }
        public string provCode { get; set; }
        public string citymunCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberAddress> MemberAddresses { get; set; }
    }
}
