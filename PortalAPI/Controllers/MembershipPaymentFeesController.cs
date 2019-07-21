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
using PortalAPI.Models;
using PortalAPI.DTO;

namespace PortalAPI.Controllers
{

    public class MembershipPaymentFeesController : ApiController
    {
        private readonly Portal_DBEntities _context;

        public MembershipPaymentFeesController()
        {
            _context = new Portal_DBEntities();
        }

        /// <summary>
        /// Returns a list of Membership payment Fees
        /// </summary>
        /// <returns></returns>
        // GET: api/MembershipPaymentFees
        [HttpGet]
        [Route("api/MembershipPaymentFee")]
        [ResponseType(typeof(MembershipPaymentFeeDTO))]
        public IHttpActionResult GetMembershipPaymentFees()
        {
            return Ok(_context.MembershipPaymentFees.ToList()
                .Select(Mapper.Map<MembershipPaymentFee, MembershipPaymentFeeDTO>));
        }

        /// <summary>
        /// Finds a Membership Payment Fee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/MembershipPaymentFees/5
        [HttpGet]
        [Route("api/MembershipPaymentFee/{id}")]
        [ResponseType(typeof(MembershipPaymentFeeDTO))]
        public IHttpActionResult GetMembershipPaymentFee(int id)
        {
            MembershipPaymentFee membershipPaymentFee = _context.MembershipPaymentFees.SingleOrDefault(x => x.Id == id);
            if (membershipPaymentFee == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<MembershipPaymentFee, MembershipPaymentFeeDTO>(membershipPaymentFee));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipPaymentFeeDto"></param>
        /// <returns></returns>
        // PUT: api/MembershipPaymentFees/5
        [HttpPut]
        [Route("api/MembershipPaymentFee/{id}")]
        [ResponseType(typeof(MembershipPaymentFeeDTO))]
        [Authorize]
        public IHttpActionResult PutMembershipPaymentFee([FromUri] int id,[FromBody] MembershipPaymentFeeDTO membershipPaymentFeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membershipPaymentFeeDto.Id)
            {
                return BadRequest();
            }

            var membershipPaymentFee = _context.MembershipPaymentFees.SingleOrDefault(x => x.Id == id);
            if (membershipPaymentFee == null)
            {
                return NotFound();
            }

            Mapper.Map<MembershipPaymentFeeDTO, MembershipPaymentFee>(membershipPaymentFeeDto, membershipPaymentFee);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembershipPaymentFeeExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(membershipPaymentFeeDto);
        }

        /// <summary>
        /// Create memebrship payment fee entity
        /// </summary>
        /// <param name="membershipPaymentFeeDto"></param>
        /// <returns></returns>
        // POST: api/MembershipPaymentFees
        [HttpPost]
        [Route("api/MembershipPaymentFee")]
        [ResponseType(typeof(MembershipPaymentFeeDTO))]
        [Authorize]
        public IHttpActionResult PostMembershipPaymentFee([FromBody] MembershipPaymentFeeDTO membershipPaymentFeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var membershipPaymentFee =
                Mapper.Map<MembershipPaymentFeeDTO, MembershipPaymentFee>(membershipPaymentFeeDto);
            _context.MembershipPaymentFees.Add(membershipPaymentFee);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + membershipPaymentFee.Id), membershipPaymentFeeDto);
        }

        /// <summary>
        /// Remove membership payment fees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/MembershipPaymentFees/5
        [HttpDelete]
        [Route("api/MembershipPaymentFee/{id}")]
        [ResponseType(typeof(MembershipPaymentFeeDTO))]
        [Authorize]
        public IHttpActionResult DeleteMembershipPaymentFee(int id)
        {
            MembershipPaymentFee membershipPaymentFee = _context.MembershipPaymentFees.SingleOrDefault(x => x.Id == id);
            if (membershipPaymentFee == null)
            {
                return NotFound();
            }

            _context.MembershipPaymentFees.Remove(membershipPaymentFee);
            _context.SaveChanges();

            return Ok(Mapper.Map<MembershipPaymentFee, MembershipPaymentFeeDTO>(membershipPaymentFee));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MembershipPaymentFeeExists(int id)
        {
            return _context.MembershipPaymentFees.Count(e => e.Id == id) > 0;
        }
    }
}