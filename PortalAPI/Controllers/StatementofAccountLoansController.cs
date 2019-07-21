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
using System.Web.Management;
using AutoMapper;
using PortalAPI.DTO;
using PortalAPI.Models;

namespace PortalAPI.Controllers
{
    public class StatementofAccountLoansController : ApiController
    {
        private readonly Portal_DBEntities ctx;

        public StatementofAccountLoansController()
        {
            ctx = new Portal_DBEntities();
        }


        /// <summary>
        /// Return a list of Statement of Account 
        /// </summary>
        /// <returns></returns>
        // GET: api/StatementofAccountLoans
        [HttpGet]
        [Route("api/StatementofAccount/loan")]
        [ResponseType(typeof(List<LoanStatementofAccountDTO>))]
        public IHttpActionResult GetAllStatementofAccount()
        {
            return Ok(ctx.rptsrc_StatementofAccountLoans.ToList()
                .Select(Mapper.Map<rptsrc_StatementofAccountLoans, LoanStatementofAccountDTO>));
        }

        /// <summary>
        /// Find a statemt of account by Loan account id and Statement Date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statementDate"></param>
        /// <returns></returns>
       [HttpGet]
       [Route("api/statementofaccount/loan/{id}/{statementDate}")]
       [ResponseType(typeof(List<LoanStatementofAccountDTO>))]
        public IHttpActionResult GetStatementofAccount([FromUri] int id, [FromUri] DateTime statementDate)
        {

            List<rptsrc_StatementofAccountLoans> listofSA = ctx.rptsrc_StatementofAccountLoans.Where(x =>
                x.LoanAccountId == id
                && ( x.TransactionDate.Value.Year == statementDate.Year && x.TransactionDate.Value.Month == statementDate.Month)).ToList();

            if(listofSA == null || listofSA.Count <= 0)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<List<rptsrc_StatementofAccountLoans>, List<LoanStatementofAccountDTO>>(listofSA));
        }

        /// <summary>
        /// Bulk Posting of loan statement of account
        /// </summary>
        /// <param name="rptsrc_StatementofAccountLoans"></param>
        /// <returns></returns>
        // POST: api/StatementofAccountLoans
        [HttpPost]
        [Route("api/statementofaccount/loan")]
        public IHttpActionResult Postrptsrc_StatementofAccountLoans([FromBody] List<LoanStatementofAccountDTO> loanStatementofAccountDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listofStatementofAccount = Mapper.Map<List<rptsrc_StatementofAccountLoans>>(loanStatementofAccountDtos);
            if (listofStatementofAccount == null || listofStatementofAccount.Count <= 0)
            {
                return BadRequest();
            }

            ctx.rptsrc_StatementofAccountLoans.AddRange(listofStatementofAccount);
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

        private bool rptsrc_StatementofAccountLoansExists(int id)
        {
            return ctx.rptsrc_StatementofAccountLoans.Count(e => e.id == id) > 0;
        }
    }
}