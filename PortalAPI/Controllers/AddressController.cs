using AutoMapper;
using PortalAPI.DTO;
using PortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PortalAPI.Controllers
{
    public class AddressController : ApiController
    {
        private readonly Portal_DBEntities ctx;

        public AddressController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Returns a list of Region
        /// </summary>
        /// <returns></returns>
        // GET: api/Regions
        [HttpGet]
        [Route("api/Region")]
        [ResponseType(typeof(RegionDTO))]
        public IHttpActionResult GetRegions()
        {
            return Ok(ctx.Regions.ToList()
                .Select(Mapper.Map<Region, RegionDTO>));
        }

        /// <summary>
        /// Find a region by code
        /// </summary>
        /// <param name="regCode"></param>
        /// <returns></returns>
        // GET: api/Regions/5
        [HttpGet]
        [Route("api/Region/{regCode}")]
        [ResponseType(typeof(RegionDTO))]
        public IHttpActionResult GetRegion(string regCode)
        {
            Region region = ctx.Regions.SingleOrDefault(x => x.regCode == regCode);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Region, RegionDTO>(region));
        }

        /// <summary>
        ///  Return a list of province by region code
        /// </summary>
        /// <param name="regCode"></param>
        /// <returns></returns>
        // GET: api/Regions/5/Province
        [HttpGet]
        [Route("api/Region/{regCode}/Province")]
        [ResponseType(typeof(ProvinceDTO))]
        public IHttpActionResult GetProvincebyRegCode(string regCode)
        {
            var province = ctx.Provinces.Where(x => x.regCode == regCode).ToList();
            if (province == null)
            {
                return NotFound();
            }

            return Ok(province.ToList().Select(Mapper.Map<Province, ProvinceDTO>));
        }

        /// <summary>
        /// Return a list of province
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Province")]
        [ResponseType(typeof(ProvinceDTO))]
        public IHttpActionResult GetProvinces()
        {
            return Ok(ctx.Provinces.ToList()
                .Select(Mapper.Map<Province, ProvinceDTO>));
        }

        /// <summary>
        /// Find a province by code
        /// </summary>
        /// <param name="provCode"></param>
        /// <returns></returns>
        // GET: api/Provinces/5
        [HttpGet]
        [Route("api/Province/{provCode}")]
        [ResponseType(typeof(ProvinceDTO))]
        public IHttpActionResult GetProvince(string provCode)
        {
            Province province = ctx.Provinces.SingleOrDefault(x => x.provCode == provCode);
            if (province == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Province, ProvinceDTO>(province));
        }

        /// <summary>
        /// Return a list of city/municipality by province code
        /// </summary>
        /// <param name="provCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Province/{provCode}/CityMunicipality")]
        [ResponseType(typeof(CityMunicipalityDTO))]
        public IHttpActionResult GetCityMunicipalitybyProvCode(string provCode)
        {
            var citymun = ctx.CityMunicipalities.Where(c => c.provCode == provCode).ToList();
            if (citymun == null)
            {
                return NotFound();
            }

            return Ok(citymun.ToList().Select(Mapper.Map<CityMunicipality, CityMunicipalityDTO>));
        }

        /// <summary>
        ///  Returns a list of City Municipality
        /// </summary>
        /// <returns></returns>
        // GET: api/CityMunicipality
        [HttpGet]
        [Route("api/CityMunicipality")]
        [ResponseType(typeof(CityMunicipalityDTO))]
        public IHttpActionResult GetCityMunicipality()
        {
            return Ok(ctx.CityMunicipalities.ToList()
                .Select(Mapper.Map<CityMunicipality, CityMunicipalityDTO>));
        }

        /// <summary>
        /// Find City Municipality by code
        /// </summary>
        /// <param name="citymunCode"></param>
        /// <returns></returns>
        // GET: api/CityMunicipality/5
        [HttpGet]
        [Route("api/CityMunicipality/{citymunCode}")]
        [ResponseType(typeof(CityMunicipalityDTO))]
        public IHttpActionResult GetCityMunicipality(string citymunCode)
        {
            CityMunicipality citymun = ctx.CityMunicipalities.SingleOrDefault(x => x.citymunCode == citymunCode);
            if (citymun == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CityMunicipality, CityMunicipalityDTO>(citymun));
        }

        /// <summary>
        /// Return a list of barangay by city/municipality code
        /// </summary>
        /// <param name="citymunCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/CityMunicipality/{citymunCode}/Barangay")]
        [ResponseType(typeof(BarangaysDTO))]
        public IHttpActionResult GetBarangaybyCityMunCode(string citymunCode)
        {
            var barangay = ctx.Barangays.Where(b => b.citymunCode == citymunCode).ToList();
            if(barangay == null)
            {
                return NotFound();
            }

            return Ok(barangay.ToList().Select(Mapper.Map<Barangay, BarangaysDTO>));
        }

        /// <summary>
        /// Returns a list of barangays
        /// </summary>
        // GET: api/Barangays
        [HttpGet]
        [Route("api/Barangay")]
        [ResponseType(typeof(BarangaysDTO))]
        public IHttpActionResult GetBarangay()
        {
            return Ok(ctx.Barangays.ToList()
                .Select(Mapper.Map<Barangay, BarangaysDTO>));
        }

        /// <summary>
        /// Find a barangay by barangay code
        /// </summary>
        // GET: api/Barangays/5
        [HttpGet]
        [Route("api/Barangay/{brgyCode}")]
        [ResponseType(typeof(BarangaysDTO))]
        public IHttpActionResult GetBarangay(string brgyCode)
        {
            Barangay brgy = ctx.Barangays.SingleOrDefault(x => x.brgyCode == brgyCode);

            if (brgy == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Barangay, BarangaysDTO>(brgy));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
