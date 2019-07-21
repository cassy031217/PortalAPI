using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PortalAPI.DTO;
using PortalAPI.Models;

namespace PortalAPI.Controllers
{
    public class LoanProductsMastersController : ApiController
    {
        private readonly Portal_DBEntities _context;

        public LoanProductsMastersController()
        {
            _context = new Portal_DBEntities();
        }

        /// <summary>
        /// Returns a list of Loan Product
        /// </summary>
        /// <returns></returns>
        // GET: api/LoanProduct
        [HttpGet]
        [Route("api/LoanProduct")]
        [ResponseType(typeof(LoanProductsMasterDTO))]
        public IHttpActionResult GetLoanProduct()
        {
            return Ok(_context.LoanProductsMasters.ToList()
                .Select(Mapper.Map<LoanProductsMaster, LoanProductsMasterDTO>));
        }

        /// <summary>
        /// Finds a loan product by product code
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <returns></returns>
        // GET: api/LoanProductsMasters/5
        [HttpGet]
        [Route("api/LoanProduct/{ProductCode}")]
        [ResponseType(typeof(LoanProductsMasterDTO))]
        public IHttpActionResult GetLoanProduct(string ProductCode)
        {
            LoanProductsMaster loanProductsMaster =
                _context.LoanProductsMasters.SingleOrDefault(x => x.ProductCode == ProductCode);
            if (loanProductsMaster == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<LoanProductsMaster, LoanProductsMasterDTO>(loanProductsMaster));
        }
        /// <summary>
        /// Modify the loan product
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <param name="loanProductsMasterDto"></param>
        /// <returns></returns>
        // PUT: api/LoanProductsMasters/5
        [HttpPut]
        [Route("api/LoanProduct/{ProductCode}")]
        [ResponseType(typeof(LoanProductsMasterDTO))]
        [Authorize]
        public IHttpActionResult PutLoanProduct([FromUri] string ProductCode,[FromBody]  LoanProductsMasterDTO loanProductsMasterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ProductCode != loanProductsMasterDto.ProductCode)
            {
                return BadRequest();
            }

            var loanproduct = _context.LoanProductsMasters.SingleOrDefault(x => x.ProductCode == ProductCode);

            if (loanproduct == null)
            {
                return NotFound();
            }

            Mapper.Map<LoanProductsMasterDTO, LoanProductsMaster>(loanProductsMasterDto, loanproduct);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (LoanProductsMasterExists(loanproduct.ProductCode))
                {
                    return Conflict();
                }
                else
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            return Ok(loanProductsMasterDto);
        }

        /// <summary>
        /// Create Loan Product entity
        /// </summary>
        /// <param name="loanProductsMaster"></param>
        /// <returns></returns>
        // POST: api/LoanProductsMasters
        [HttpPost]
        [Route("api/LoanProduct")]
        [ResponseType(typeof(LoanProductsMasterDTO))]
        [Authorize]
        public IHttpActionResult PostLoanProductsMaster([FromBody] LoanProductsMasterDTO loanProductsMasterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loanproduct = Mapper.Map<LoanProductsMasterDTO, LoanProductsMaster>(loanProductsMasterDto);
            _context.LoanProductsMasters.Add(loanproduct);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                if (LoanProductsMasterExists(loanproduct.ProductCode))
                {
                    return Conflict();
                }
                else
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
          

            return Created(new Uri(Request.RequestUri + "/" + loanProductsMasterDto.ProductCode),
                loanProductsMasterDto);
        }

        [HttpPost]
        [Route("api/LoanProduct")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostLoanProductsMaster([FromBody] List<LoanProductsMasterDTO> loanProductsMasterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ListofProductsrc = Mapper.Map<List<LoanProductsMaster>>(loanProductsMasterDto);
            var listp = ListofProductsrc.Select(x => x.ProductCode).ToList();

            var ListofProductExistonDB =
                _context.LoanProductsMasters.Where(i => listp.Contains(i.ProductCode)).ToList();

            if (ListofProductExistonDB.Count   > 0)
            {
                var matchAcctList = ListofProductExistonDB.Select(x => x.ProductCode).ToList();

                var matchedAcctlistsrc = ListofProductsrc.Where(i => matchAcctList.Contains(i.ProductCode)).ToList();
                if (matchedAcctlistsrc.Count > 0)
                {
                    foreach (var item in matchedAcctlistsrc)
                    {
                        var singleAcct = ListofProductExistonDB.SingleOrDefault(x => x.ProductCode == item.ProductCode);
                        if (singleAcct != null)
                        {
                            singleAcct.ModeOfPayment = item.ModeOfPayment;
                            singleAcct.FrequencyOfPayment = item.FrequencyOfPayment;
                            singleAcct.NumberOfPayments = item.NumberOfPayments;
                            singleAcct.InterestRate = item.InterestRate;
                            singleAcct.InterestModeofPayment = item.InterestModeofPayment;
                            singleAcct.InterestComputationMethod = item.InterestComputationMethod;
                            singleAcct.InterestFrequency = item.InterestFrequency;
                            singleAcct.InterestComputationFrequency = item.InterestComputationFrequency;
                            singleAcct.AdvancedInterest = item.AdvancedInterest;
                        }
                    }
                }

                var listofProductNew = ListofProductsrc.Where(i => !matchAcctList.Contains(i.ProductCode)).ToList();
                if (listofProductNew.Count > 0)
                {
                    _context.LoanProductsMasters.AddRange(listofProductNew);
                }
            }
            else
            {
                _context.LoanProductsMasters.AddRange(ListofProductsrc);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                    Console.WriteLine(e);
                    throw;
            }


            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <returns></returns>
        // DELETE: api/LoanProductsMasters/5
        [HttpDelete]
        [Route("api/LoanProduct/{ProductCode}")]
        [ResponseType(typeof(LoanProductsMasterDTO))]
        [Authorize]
        public IHttpActionResult DeleteLoanProductsMaster([FromUri] string ProductCode)
        {
            LoanProductsMaster loanProductsMaster =
                _context.LoanProductsMasters.Single(x => x.ProductCode == ProductCode);
            if (loanProductsMaster == null)
            {
                return NotFound();
            }

            _context.LoanProductsMasters.Remove(loanProductsMaster);
            _context.SaveChanges();

            return Ok(Mapper.Map<LoanProductsMaster, LoanProductsMasterDTO>(loanProductsMaster));
        }
        /// <summary>
        /// Dispose 
        /// </summary>
        /// <param name="disposing"></param>

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoanProductsMasterExists(string ProductCode)
        {
            return _context.LoanProductsMasters.Count(e => e.ProductCode == ProductCode) > 0;
        }
    }
}