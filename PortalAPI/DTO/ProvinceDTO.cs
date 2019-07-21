using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class ProvinceDTO
    {
        /// <summary>
        /// Philippine Standard Geographic Code
        /// </summary>
        public string psgcCode { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Region Code
        /// </summary>
        public string regCode { get; set; }

        /// <summary>
        /// Province Code
        /// </summary>
        public string provCode { get; set; }
    }
}