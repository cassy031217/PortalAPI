using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PortalAPI.DTO;
using PortalAPI.Models;

namespace PortalAPI.Controllers
{
    public class CasaProductsController : ApiController
    {
        private readonly Portal_DBEntities ctx;

        public CasaProductsController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Return a list of casa product
        /// </summary>
        /// <returns></returns>
        // GET: api/CasaProducts
        [HttpGet]
        [Route("api/casaproduct")]
        [ResponseType(typeof(CasaProductDTO))]
        public IHttpActionResult GetCasaProducts()
        {
            return Ok(ctx.CasaProducts.ToList()
                .Select(Mapper.Map<CasaProduct, CasaProductDTO>));
        }

        /// <summary>
        /// Find a casa product by code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/CasaProducts/5
        [HttpGet]
        [Route("api/casaproduct/{id}")]
        [ResponseType(typeof(CasaProductDTO))]
        public IHttpActionResult GetCasaProduct(string id)
        {
            CasaProduct casaProduct = ctx.CasaProducts.SingleOrDefault(x => x.ProductCode == id);
            if (casaProduct == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CasaProduct, CasaProductDTO>(casaProduct));
        }

        /// <summary>
        /// Modify a casa product by code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="casaProduct"></param>
        /// <returns></returns>
        // PUT: api/CasaProducts/5
        [HttpPut]
        [Route("api/casaproduct/{id}")]
        [ResponseType(typeof(CasaProductDTO))]
        [Authorize]
        public IHttpActionResult PutCasaProduct([FromUri] string id,[FromBody] CasaProductDTO casaProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != casaProductDto.ProductCode)
            {
                return BadRequest();
            }

            var cp = ctx.CasaProducts.SingleOrDefault(x => x.ProductCode == id);

            if (cp == null)
            {
                return NotFound();
            }

            Mapper.Map<CasaProductDTO, CasaProduct>(casaProductDto, cp);

            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasaProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Mapper.Map<CasaProduct,CasaProductDTO>(cp));
        }

        /// <summary>
        /// Create a casa product entity
        /// </summary>
        /// <param name="casaProductDto"></param>
        /// <returns></returns>
        // POST: api/CasaProducts
        [HttpPost]
        [Route("api/casaproduct")]
        [ResponseType(typeof(CasaProductDTO))]
        [Authorize]
        public IHttpActionResult PostCasaProduct([FromBody] CasaProductDTO casaProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cp = Mapper.Map<CasaProductDTO, CasaProduct>(casaProductDto);
            ctx.CasaProducts.Add(cp);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                if (CasaProductExists(cp.ProductCode))
                {
                    return Conflict();
                }
                else
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
           

            return Created(new Uri(Request.RequestUri + "/" + cp.ProductCode),
                Mapper.Map<CasaProduct, CasaProductDTO>(cp));


        }

        /// <summary>
        ///  bulk posting of casa product
        /// </summary>
        /// <param name="casaProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/casaproduct-bulk")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostCasaProduct([FromBody] List<CasaProductDTO> casaProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get reference list of product
            var listofProductsrc = Mapper.Map<List<CasaProductDTO>, List<CasaProduct>>(casaProductDto);
            var listp = listofProductsrc.Select(x => x.ProductCode);

            // Get a list of product that already exist in database
            var listofProductExistonDB = ctx.CasaProducts.Where(itemdb => listp.Contains(itemdb.ProductCode)).ToList();

            if (listofProductExistonDB.Count > 0)
            {
                var matchedAcclist = listofProductExistonDB.Select(x => x.ProductCode).ToList();
                var matchedAcctlistsrc = listofProductsrc.Where(itemsrc =>
                    matchedAcclist.Contains(itemsrc.ProductCode)).ToList();

                if (matchedAcctlistsrc.Count > 0)
                {
                    foreach (var item in matchedAcctlistsrc)
                    {
                        var singleAcct = listofProductExistonDB.SingleOrDefault(x => x.ProductCode == item.ProductCode);
                        if (singleAcct != null)
                        {
                            singleAcct.Description = item.Description;
                            singleAcct.TypeOfAccount = item.TypeOfAccount;
                            singleAcct.MinimumDeposit = item.MinimumDeposit;
                            singleAcct.MinimumBalance = item.MinimumBalance;
                            singleAcct.HoldAmount = item.HoldAmount;
                            singleAcct.Divisor = item.Divisor;
                            singleAcct.TaxFactor = item.TaxFactor;
                            singleAcct.ComputeFrequency = item.ComputeFrequency;
                            singleAcct.ServiceChargeActive = item.ServiceChargeActive;
                            singleAcct.ServiceChargeDormant = item.ServiceChargeDormant;
                            singleAcct.DaysDormant = item.DaysDormant;
                            singleAcct.Terms = item.Terms;
                        }
                    }

                }

                var listofProductNew = listofProductsrc.Where(i => !matchedAcclist.Contains(i.ProductCode)).ToList();
                if (listofProductNew.Count > 0)
                {
                    ctx.CasaProducts.AddRange(listofProductNew);
                }
            }
            else
            {
                ctx.CasaProducts.AddRange(listofProductsrc);
            }

            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
               
                    Console.WriteLine(e);
                    throw;
            }


            return Ok();
        }

        /// <summary>
        /// Remove a casa product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/CasaProducts/5
        [HttpDelete]
        [Route("api/casaproduct/{id}")]
        [ResponseType(typeof(CasaProductDTO))]
        [Authorize]
        public IHttpActionResult DeleteCasaProduct(string id)
        {
            CasaProduct casaProduct = ctx.CasaProducts.SingleOrDefault(x => x.ProductCode == id);
            if (casaProduct == null)
            {
                return NotFound();
            }

            ctx.CasaProducts.Remove(casaProduct);
            ctx.SaveChanges();

            return Ok(Mapper.Map<CasaProduct, CasaProductDTO>(casaProduct));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CasaProductExists(string id)
        {
            return ctx.CasaProducts.Count(e => e.ProductCode == id) > 0;
        }
    }
}