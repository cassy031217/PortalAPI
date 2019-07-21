using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class RegionDTO
    {
        /// <summary>
        /// Philippine Standard Geographic Code
        /// </summary>
        public string psgcCode { get; set; }

        /// <summary>
        /// Short name
        /// </summary>
        public string shortname { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Region Code
        /// </summary>
        public string regCode { get; set; }
    }
}