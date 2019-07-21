using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PortalAPI.DTO;
using PortalAPI.Models;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using WebGrease.Css.Ast.Selectors;

namespace PortalAPI.Controllers
{
    public class MembershipsController : ApiController
    {
        private Portal_DBEntities ctx;
        private MembershipFactory _membershipFactory;

        public MembershipsController()
        {
            ctx = new Portal_DBEntities();
            _membershipFactory = new MembershipFactory();
        }


        /// <summary>
        /// Return a list of membership including their address, dependents, family and payment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Membership")]
        [ResponseType(typeof(MembershipDTO))]
        public IHttpActionResult GetAllMembership()
        {
            var membership = ctx.Memberships.Include(a => a.MemberAddresses)
                .Include(d => d.MemberDependents).Include(f => f.MemberFamilies)
                .Include(p => p.MembershipPayments).ToList()
                .Select(a => _membershipFactory.Create(a));

            return Ok (membership);

        }

        /// <summary>
        /// Get uploaded binary file in database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/FileAPI/GetFile")]
        public HttpResponseMessage GetFile(int FileID, string SourceFile)
        {
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Fetch the file data from Database
            Membership file = new Membership();
            file = ctx.Memberships.Where(a => a.Id == FileID).FirstOrDefault();

            if (file == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (SourceFile == "Application")
            {
                //Set the Response Content
                response.Content = new ByteArrayContent(file.ApplicationContent);

                //set the response Content Length
                response.Content.Headers.ContentLength = file.ApplicationContent.LongLength;

                //set the content disposition header value and file name
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.ApplicationName;

                //Set the file content type
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ApplicationContentType);
            }
            else if (SourceFile == "PictureID")
            {
                //Set the Response Content
                response.Content = new ByteArrayContent(file.MemberIDPictureContent);

                //set the response Content Length
                response.Content.Headers.ContentLength = file.MemberIDPictureContent.LongLength;

                //set the content disposition header value and file name
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.MemberIDPictureName;

                //Set the file content type
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.MemberIDPictureContentType);
            }
            else if (SourceFile == "ValidID")
            {
                //Set the Response Content
                response.Content = new ByteArrayContent(file.MemberValidIdContent);

                //set the response Content Length
                response.Content.Headers.ContentLength = file.MemberValidIdContent.LongLength;

                //set the content disposition header value and file name
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.MemberValidIdName;

                //Set the file content type
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.MemberValidIdContentType);
            }
            else if (SourceFile == "Payment")
            {
                //Set the Response Content
                response.Content = new ByteArrayContent(file.PaymentContent);

                //set the response Content Length
                response.Content.Headers.ContentLength = file.PaymentContent.LongLength;

                //set the content disposition header value and file name
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.PaymentName;

                //Set the file content type
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.PaymentContentType);
            }

            return response;
        }

        /// <summary>
        /// Find a membership by Membership Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Membership/{Id}")]
        [ResponseType(typeof(MembershipDTO))]
        public IHttpActionResult GetMembershipApplication(int Id)
        {
            var membership = ctx.Memberships.Include(a => a.MemberAddresses)
                .Include(d => d.MemberDependents).Include(f => f.MemberFamilies).Include(p => p.MembershipPayments)
                .Where(p => p.Id == Id).ToList().Select(a => _membershipFactory.Create(a)).SingleOrDefault();

            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership);
        }

        /// <summary>
        /// Find a membership by Name, Birthdate and Gender
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MembershipStatus")]
        [ResponseType(typeof(MembershipStatus))]
        public IHttpActionResult GetMembershipStatus([FromUri] GetMembershipSatus statusRequest)
        {

            var membership = ctx.Memberships.Where(m =>
                    m.LastName == statusRequest.LastName &&
                    m.MiddleName == statusRequest.MiddleName &&
                    m.FirstName == statusRequest.FirstName &&
                    m.BirthDate == statusRequest.BirthDate.Date &&
                    m.Gender == statusRequest.Gender)
                .Select(m => new MembershipStatus()
                {
                    ApplicationStage = m.ApplicationStage,
                    ApplicationStatus = m.ApplicationStatus,
                    Id = m.Id
                }).SingleOrDefault();

            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership);
        }

        /// <summary>
        /// Search membership by Name, Birth Date and Gender
        /// </summary>
        /// <param name="statusRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MembershipDetailStatus")]
        [ResponseType(typeof(MembershipDTO))]
        public IHttpActionResult GetMembershipDetailStatus([FromUri] GetMembershipSatus statusRequest)
        {

            var membership = ctx.Memberships.Include(m => m.MembershipPayments).Where(m =>
                m.LastName == statusRequest.LastName &&
                m.MiddleName == statusRequest.MiddleName &&
                m.FirstName == statusRequest.FirstName &&
                m.BirthDate == statusRequest.BirthDate.Date &&
                m.Gender == statusRequest.Gender).ToList().Select(a => _membershipFactory.Create(a)).SingleOrDefault();

            if (membership == null)
            {
                return NotFound();
            }

            return Ok(membership);
        }

        /// <summary>
        /// Find a list of membership that not exist on local/plus database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ApprovedMembership")]
        [ResponseType(typeof(MembershipDTO))]
        public IHttpActionResult GetApprovedMembership()
        {
            var membership = ctx.Memberships
                .Where(p => p.ApplicationStage == "Approved" && p.ApplicationStatus == "Approved" && p.IsExists == false)
                .ToList();
            if (membership == null)
            {
                return NotFound();
            }
            if (membership.Count < 1)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<List<Membership>, List<MembershipDTO>>(membership));
        }



        /// <summary>
        /// Create a membership Application Entity
        /// </summary>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MembershipApplication")]
        [ResponseType(typeof(MembershipDTO))]
        [Authorize]
        public IHttpActionResult PostMembershiApplication([FromBody] MembershipDTO membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var membership = ctx.Memberships.SingleOrDefault(x => x.LastName == membershipDto.LastName && x.FirstName == membershipDto.FirstName &&
                                                                  x.MiddleName == membershipDto.MiddleName && x.BirthDate == membershipDto.BirthDate.Date && x.Gender == membershipDto.Gender);
            if (membership != null)
            {
                return Ok("Already exist");
            }

            var member = Mapper.Map<MembershipDTO, Membership>(membershipDto);
            ctx.Memberships.Add(member);
            ctx.SaveChanges();

            //Get list of personal Address

            var address = Mapper.Map<IList<MemberAddress>>(membershipDto.Address);
            if (address != null)
            {
                if (address.Count > 0)
                {
                    //ctx.PersonalAddreses.AddRange(address);

                    foreach (var a in address)
                    {
                        a.MembershipID = member.Id;
                        ctx.MemberAddresses.Add(a);
                    }
                }
            }

            //Get a list of personal dependents
            var dependents = Mapper.Map<IList<MemberDependent>>(membershipDto.Dependent);
            if (dependents != null)
            {
                if (dependents.Count > 0)
                {
                    foreach (var d in dependents)
                    {
                        d.MembershipID = member.Id;
                        ctx.MemberDependents.Add(d);
                    }
                }
            }
            // ctx.PersonalDependents.AddRange(dependents);


            //Get a lsit of personal family
            var families = Mapper.Map<IList<MemberFamily>>(membershipDto.Families);
            if (families != null)
            {
                if (families.Count() > 0)
                {
                    foreach (var f in families)
                    {
                        f.MembershipID = member.Id;
                        ctx.MemberFamilies.Add(f);
                    }
                }
            }
            // ctx.PersonalFamilies.AddRange(families);


            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Created(new Uri(Request.RequestUri + "/" + member.Id), Mapper.Map<MembershipDTO>(member));

        }

        /// <summary>
        ///  Modify membership Application
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/MembershipApplication/{id}")]
        [ResponseType(typeof(MembershipDTO))]
        [Authorize]
        public IHttpActionResult PutMembershipApplication([FromUri] int id, [FromBody] MembershipDTO membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membershipDto.Id)
            {
                return NotFound();
            }

            var membership = ctx.Memberships.SingleOrDefault(x => x.Id == id);
            if (membershipDto == null)
            {
                return NotFound();
            }


            var addressToRemove = ctx.MemberAddresses.Where(x => x.MembershipID == id).ToList();
            if (addressToRemove != null)
            {
                ctx.MemberAddresses.RemoveRange(addressToRemove);
            }


            var addressToAdd = Mapper.Map<IList<MemberAddress>>(membershipDto.Address);
            if (addressToAdd != null)
            {
                foreach (var a in addressToAdd)
                {
                    a.MembershipID = membership.Id;
                    ctx.MemberAddresses.Add(a);
                }
            }

            var dependentToRemove = ctx.MemberDependents.Where(d => d.MembershipID == id).ToList();
            if (dependentToRemove != null)
            {
                ctx.MemberDependents.RemoveRange(dependentToRemove);
            }


            var dependentToAdd = Mapper.Map<IList<MemberDependent>>(membershipDto.Dependent);
            if (dependentToAdd != null)
            {
                // ctx.PersonalDependents.AddRange(dependentToAdd);
                foreach (var d in dependentToAdd)
                {
                    d.MembershipID = membershipDto.Id;
                    ctx.MemberDependents.Add(d);
                }

            }


            var familiesToRemove = ctx.MemberFamilies.Where(x => x.MembershipID == id).ToList();
            if (familiesToRemove != null)
            {
                ctx.MemberFamilies.RemoveRange(familiesToRemove);
            }

            var familiesToAdd = Mapper.Map<IList<MemberFamily>>(membershipDto.Families);
            if (familiesToAdd != null)
            {
                //ctx.PersonalFamilies.AddRange(familiesToAdd);
                foreach (var n in familiesToAdd)
                {
                    n.MembershipID = membershipDto.Id;
                    ctx.MemberFamilies.Add(n);
                }
            }


            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembershipExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Mapper.Map<MembershipDTO>(membership));

        }

        /// <summary>
        /// Modify membership entity - mostly for uploading, PMES, Assessment Stage
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Membership/{id}")]
        [ResponseType(typeof(MembershipDTO))]
        [Authorize]
        public IHttpActionResult PutMembership([FromUri] int id, [FromBody] MembershipDTO membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membershipDto.Id)
            {
                return NotFound();
            }

            var membership = ctx.Memberships.SingleOrDefault(x => x.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            Mapper.Map<MembershipDTO, Membership>(membershipDto, membership);

            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembershipExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Mapper.Map<MembershipDTO>(membership));
        }

        /// <summary>
        /// Modify membership entity -  for uploading
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/MembershipUpload/{id}")]
        [ResponseType(typeof(MembershipDTO))]
        [Authorize]
        public IHttpActionResult PutMembershipUpload([FromUri] int id, [FromBody] MembershipDTO membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membershipDto.Id)
            {
                return NotFound();
            }

            var membership = ctx.Memberships.SingleOrDefault(x => x.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            membership.ApplicationStage = membershipDto.ApplicationStage;
            membership.ApplicationStatus = membershipDto.ApplicationStatus;

            if (membershipDto.ApplicationName != null && membershipDto.ApplicationName != "")
            {
                membership.ApplicationName = membershipDto.ApplicationName;
                membership.ApplicationContentType = membershipDto.ApplicationContentType;
                membership.ApplicationContent = membershipDto.ApplicationContent;
            }

            if (membershipDto.MemberIDPictureName != null && membershipDto.MemberIDPictureName != "")
            {
                membership.MemberIDPictureName = membershipDto.MemberIDPictureName;
                membership.MemberIDPictureContentType = membershipDto.MemberIDPictureContentType;
                membership.MemberIDPictureContent = membershipDto.MemberIDPictureContent;
            }

            if (membershipDto.MemberValidIdName != null && membershipDto.MemberValidIdName != "")
            {
                membership.MemberValidIdName = membershipDto.MemberValidIdName;
                membership.MemberValidIdContentType = membershipDto.MemberValidIdContentType;
                membership.MemberValidIdContent = membershipDto.MemberValidIdContent;
            }

            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembershipExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Mapper.Map<MembershipDTO>(membership));
        }

        /// <summary>
        /// Create a membership payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/PaymentOfMembership/{id}")]
        [ResponseType(typeof(MembershipDTO))]
        [Authorize]
        public IHttpActionResult PostMembershipPayment([FromUri] int id, [FromBody] MembershipDTO membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membershipDto.Id)
            {
                return NotFound();
            }

            var membership = ctx.Memberships.SingleOrDefault(x => x.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            membership.ApplicationStage = membershipDto.ApplicationStage;
            membership.ApplicationStatus = membershipDto.ApplicationStatus;
            if (membershipDto.PaymentName != null && membershipDto.PaymentName != "")
            {
                membership.PaymentName = membershipDto.PaymentName;
                membership.PaymentContent = membershipDto.PaymentContent;
                membership.PaymentContentType = membershipDto.PaymentContentType;
            }

            var PaymentToRemove = ctx.MembershipPayments.Where(x => x.MembershipID == id).ToList();
            if (PaymentToRemove != null)
            {
                ctx.MembershipPayments.RemoveRange(PaymentToRemove);
            }

            var payment = Mapper.Map<IList<MembershipPayment>>(membershipDto.Payment);
            if (payment != null)
            {
                foreach (var p in payment)
                {
                    p.MembershipID = membershipDto.Id;
                    ctx.MembershipPayments.Add(p);
                }
            }

            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembershipExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Mapper.Map<MembershipDTO>(membership));
        }

        /// <summary>
        /// Modify Membership payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/PaymentOfMembership/{id}")]
        [ResponseType(typeof(MembershipDTO))]
        [Authorize]
        public IHttpActionResult PutMembershipPayment([FromUri] int id, [FromBody] MembershipDTO membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membershipDto.Id)
            {
                return NotFound();
            }

            var membership = ctx.Memberships.SingleOrDefault(x => x.Id == id);
            if (membership == null)
            {
                return NotFound();
            }

            Mapper.Map<MembershipDTO, Membership>(membershipDto, membership);

            if (membershipDto.Payment != null)
            {
                foreach (var p in membershipDto.Payment)
                {
                    var paymentDB = ctx.MembershipPayments.SingleOrDefault(x => x.Id == p.Id);
                    Mapper.Map<MembershipPaymentDTO, MembershipPayment>(p, paymentDB);
                }
            }

            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembershipExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return Ok(Mapper.Map<MembershipDTO>(membership));
            return Ok();
        }

        /// <summary>
        /// Bulk update of Membership Status to tag as alreay exist in local/plus database
        /// </summary>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/MembershipExist-bulk")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutMembershipStatus( [FromBody] List<MembershipDTO> membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get reference  list of Membership
            var ListofAccountRef = Mapper.Map<IList<Membership>>(membershipDto);
            var listRef = ListofAccountRef.Select(x => x.Id).ToList();

            try
            {
                //update Membership already exist on database
                ctx.Memberships.Where(itemdb => itemdb.IsExists == false && listRef.Contains(itemdb.Id))
                .Update(m => new Membership {IsExists = true});
           
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
        /// Bulk posting of Membership entity
        /// </summary>
        /// <param name="membershipDto"></param>
        /// <returns></returns>
        // POST: api/CasaAccounts
        [HttpPost]
        [Route("api/Membership-bulk")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostMembershipBulk([FromBody] List<MembershipDTO> membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get reference  list of Membership Account
            var ListofRef = Mapper.Map<IList<Membership>>(membershipDto);
            ctx.Memberships.AddRange(ListofRef);
          
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MembershipExists(int id)
        {
            return ctx.Memberships.Count(e => e.Id == id) > 0;
        }


    }
}