using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class CityMunicipalityDTO
    {
        /// <summary>
        /// Philippine Standard Geographic Code
        /// </summary>
        public string psgcCode { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Province Code
        /// </summary>
        public string provCode { get; set; }

        /// <summary>
        /// City Municipality Code
        /// </summary>
        public string citymunCode { get; set; }
    }
}