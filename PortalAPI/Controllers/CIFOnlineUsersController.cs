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
    public class CIFOnlineUsersController : ApiController
    {

        private readonly Portal_DBEntities ctx;

        public CIFOnlineUsersController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Return a list of CIF Online User
        /// </summary>
        /// <returns></returns>
        // GET: api/CIFOnlineUsers
        [HttpGet]
        [Route("api/CIFOnlineUser")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        public IHttpActionResult GetCIFOnlineUsers()
        {
            return Ok(ctx.CIFOnlineUsers.ToList()
                .Select(Mapper.DynamicMap<CIFOnlineUser, CIFOnlineUserDTO>));
        }

        /// <summary>
        /// Return a list of CIF Online User preactivated
        /// </summary>
        /// <returns></returns>
        // GET: api/CIFOnlineUsers
        [HttpGet]
        [Route("api/CIFOnlineUser/PreActivate")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        public IHttpActionResult GetCIFOnlineUsersPreActivated()
        {
            return Ok(ctx.CIFOnlineUsers.Where(x => x.AccountStatus.Contains("InActive")).ToList()
                .Select(Mapper.DynamicMap<CIFOnlineUser, CIFOnlineUserDTO>));
        }

        /// <summary>
        /// Find a cif online user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/CIFOnlineUsers/5
        [HttpGet]
        [Route("api/CIFOnlineUser/{id}")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        public IHttpActionResult GetCIFOnlineUser([FromUri] int id)
        {
            CIFOnlineUser cIFOnlineUser = ctx.CIFOnlineUsers.SingleOrDefault(x => x.ID == id);
            if (cIFOnlineUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CIFOnlineUser, CIFOnlineUserDTO>(cIFOnlineUser));
        }

        /// <summary>
        /// Find a cif online user by username and password
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/CIFOnlineUsers/5
        [HttpGet]
        [Route("api/CIFOnlineUser/{username}/{password}")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        public IHttpActionResult GetCIFOnlineUserbyUserNamePassword([FromUri] string username, [FromUri] string password)
        {
            #region  Password Hashing 
            string hashPassword = Crypto.Hash(password);
            #endregion

            CIFOnlineUser cIFOnlineUser = ctx.CIFOnlineUsers.SingleOrDefault(x => x.Username == username && x.Password == hashPassword);
            if (cIFOnlineUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CIFOnlineUser, CIFOnlineUserDTO>(cIFOnlineUser));
        }

        /// <summary>
        /// Modify Cif online User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cifOnlineUserDto"></param>
        /// <returns></returns>
        // PUT: api/CIFOnlineUsers/5
        [HttpPut]
        [Route("api/CIFOnlineUser/{id}")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        [Authorize]
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutCIFOnlineUser([FromUri] int id, [FromBody] CIFOnlineUserDTO cifOnlineUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cifOnlineUserDto.ID)
            {
                return BadRequest();
            }

            var cifuser = ctx.CIFOnlineUsers.SingleOrDefault(x => x.ID == id);
            if (cifuser == null)
            {
                return NotFound();
            }

            Mapper.Map<CIFOnlineUserDTO, CIFOnlineUser>(cifOnlineUserDto, cifuser);

            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CIFOnlineUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cifuser);
        }


        /// <summary>
        /// CifOnlineUser Registration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cifOnlineUserDto"></param>
        /// <returns></returns>
        // PUT: api/CIFOnlineUsers/5
        [HttpPut]
        [Route("api/CIFOnlineUser/Registration")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        [Authorize]
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutCIFOnlineUserRegistration( [FromBody] CIFOnlineUserDTO cifOnlineUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cifuser = ctx.CIFOnlineUsers.SingleOrDefault(x => x.Birthdate.Value.Date == cifOnlineUserDto.Birthdate.Value.Date && x.FirstName == cifOnlineUserDto.FirstName && x.LastName == cifOnlineUserDto.LastName && x.MiddleName == cifOnlineUserDto.MiddleName
                                                                  && x.EmailAddress == cifOnlineUserDto.EmailAddress);
            if (cifuser == null)
            {
                return NotFound();
            }

            //Mapper.Map<CIFOnlineUserDTO, CIFOnlineUser>(cifOnlineUserDto, cifuser);
            cifuser.Password = cifOnlineUserDto.Password;
            cifuser.Username = cifOnlineUserDto.Username;
            cifuser.AccountStatus = cifOnlineUserDto.AccountStatus;
            cifuser.ConfirmedEmail = cifOnlineUserDto.ConfirmedEmail;

            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
               Console.WriteLine(e);
               throw;
            }

            return Ok(cifuser);
        }

        /// <summary>
        /// Bulk posting of CifOnlineUser
        /// </summary>
        /// <param name="cIFOnlineUser"></param>
        /// <returns></returns>
        // POST: api/CIFOnlineUsers
        [HttpPost]
        [Route("api/CIFOnlineUser-bulk")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        [Authorize]
        public IHttpActionResult PostCIFOnlineUser([FromBody] List<CIFOnlineUserDTO>  cifOnlineUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cifuserAdded = Mapper.Map<IList<CIFOnlineUser>>(cifOnlineUserDto);
            if (cifuserAdded.Count > 0)
            {
                ctx.CIFOnlineUsers.AddRange(cifuserAdded);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
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
        /// Create CIF Online User entity
        /// </summary>
        /// <param name="cIFOnlineUser"></param>
        /// <returns></returns>
        // POST: api/CIFOnlineUsers
        [HttpPost]
        [Route("api/CIFOnlineUser")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        //[Authorize]
        public IHttpActionResult PostCIFOnlineUser([FromBody] CIFOnlineUserDTO cifOnlineUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cifuser = Mapper.Map<CIFOnlineUserDTO, CIFOnlineUser>(cifOnlineUserDto);
            ctx.CIFOnlineUsers.Add(cifuser);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Created(new Uri(Request.RequestUri + "/" + cifuser.ID), cifuser);
        }

        /// <summary>
        /// Remove Cif online user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/CIFOnlineUsers/5
        [HttpDelete]
        [Route("api/CIFOnlineUser/{id}")]
        [ResponseType(typeof(CIFOnlineUserDTO))]
        [Authorize]
        public IHttpActionResult DeleteCIFOnlineUser([FromUri] int id)
        {
            CIFOnlineUser cIFOnlineUser = ctx.CIFOnlineUsers.SingleOrDefault(x => x.ID == id);
            if (cIFOnlineUser == null)
            {
                return NotFound();
            }

            ctx.CIFOnlineUsers.Remove(cIFOnlineUser);
            ctx.SaveChanges();

            return Ok(cIFOnlineUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CIFOnlineUserExists(int id)
        {
            return ctx.CIFOnlineUsers.Count(e => e.ID == id) > 0;
        }
    }
}