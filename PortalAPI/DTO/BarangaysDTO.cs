using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class BarangaysDTO
    {
        public string Description { get; set; }

        /// <summary>
        /// City Municipality Code
        /// </summary>
        public string citymunCode { get; set; }

        /// <summary>
        /// Barangay Code
        /// </summary>
        public string brgyCode { get; set; }
    }
}