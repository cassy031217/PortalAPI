using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using PortalAPI.DTO;
using PortalAPI.Models;
using AutoMapper;
using System.Data.Entity.Infrastructure;

namespace PortalAPI.Controllers
{
    public class LoanApplicationsController : ApiController
    {
        private readonly Portal_DBEntities _context;

        public LoanApplicationsController()
        {
            _context = new Portal_DBEntities();
        }

        /// <summary>
        /// Returns a list of Loan Application
        /// </summary>
        /// <returns></returns>
        // GET: api/LoanApplications
        [HttpGet]
        [ResponseType(typeof(LoanApplicationDTO))]
        [Route("api/LoanApplication")]
        public IHttpActionResult GetLoanApplication()
        {
            return Ok(_context.LoanApplications.ToList()
                .Select(Mapper.Map<LoanApplication, LoanApplicationDTO>));
        }

        /// <summary>
        /// Find a Loan Application by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/LoanApplications/5
        [HttpGet]
        [Route("api/LoanApplication/{id}")]
        [ResponseType(typeof(LoanApplicationDTO))]
        public IHttpActionResult GetLoanApplication(int id)
        {
            LoanApplication loanApplication = _context.LoanApplications.SingleOrDefault(x => x.LAno == id);

            //This is part of the RESTful Convention
            if (loanApplication == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<LoanApplication,LoanApplicationDTO>(loanApplication));
        }

        /// <summary>
        /// Creates a new loan application entity
        /// </summary>
        /// <param name="loanApplicationDto"></param>
        /// <returns></returns>
        // POST: api/LoanApplications
        [HttpPost]
        [Route("api/LoanApplication")]
        [ResponseType(typeof(LoanApplicationDTO))]
        [Authorize]
        public IHttpActionResult PostLoanApplication([FromBody] LoanApplicationDTO loanApplicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loanapplication = Mapper.Map<LoanApplicationDTO, LoanApplication>(loanApplicationDto);
            _context.LoanApplications.Add(loanapplication);
            _context.SaveChanges();

            loanApplicationDto.LAno = loanapplication.LAno;

            return Created(new Uri(Request.RequestUri + "/" + loanapplication.LAno), loanApplicationDto);
        }

        /// <summary>
        /// Modify the loan application
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loanApplicationDto"></param>
        /// <returns></returns>
        // PUT: api/LoanApplications/5
        [HttpPut]
        [Route("api/LoanApplication/{id}")]
        [Authorize]
        public IHttpActionResult PutLoanApplication([FromUri] int id, [FromBody] LoanApplicationDTO loanApplicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loanApplicationDto.LAno)
            {
                return BadRequest();
            }

            var loanapplication = _context.LoanApplications.SingleOrDefault(x => x.LAno == id);


            if (loanapplication == null)
            {
                return NotFound();
            }
            Mapper.Map<LoanApplicationDTO, LoanApplication> (loanApplicationDto, loanapplication);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LoanApplicationExists(loanapplication.LAno))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return Ok(loanApplicationDto);
        }

        /// <summary>
        /// Remove the loan application 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/LoanApplications/5
        [HttpDelete]
        [Route("api/LoanApplication/{id}")]
        [ResponseType(typeof(LoanApplicationDTO))]
        [Authorize]
        public IHttpActionResult DeleteLoanApplication(int id)
        {
            var loanapplication = _context.LoanApplications.SingleOrDefault(x => x.LAno == id);

            if (loanapplication == null)
            {
                return NotFound();
            }

            _context.LoanApplications.Remove(loanapplication);
            _context.SaveChanges();

            return Ok(loanapplication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoanApplicationExists(int id)
        {
            return _context.LoanApplications.Count(e => e.LAno == id) > 0;
        }
    }
}