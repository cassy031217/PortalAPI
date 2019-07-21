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
    public class StatementofAccountSavingsController : ApiController
    {
        private readonly Portal_DBEntities ctx;

        public StatementofAccountSavingsController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Return a list of statement of account
        /// </summary>
        /// <returns></returns>
        // GET: api/StatementofAccountSavings
        [HttpGet]
        [Route("api/statementofaccount/savings")]
        [ResponseType(typeof(List<SavingStatementofAccountDTO>))]
        public IHttpActionResult GetStatementofAccount()
        {
            return Ok(ctx.rptsrc_StatementofAccountSavings.ToList()
                .Select(Mapper.Map<rptsrc_StatementofAccountSavings, SavingStatementofAccountDTO>));
        }

        /// <summary>
        /// Find a saving statement of accounts by Branch Code, Group Code, Account Number and Start & Ending date
        /// </summary>
        /// <param name="statementofAccountParameter"></param>
        /// <returns></returns>
        // GET: api/StatementofAccountSavings/5
        [HttpGet]
        [Route("api/statementofaccount/saving")]
        [ResponseType(typeof(SavingStatementofAccountDTO))]
        public IHttpActionResult Getrptsrc_StatementofAccountSavings([FromBody] StatementofAccountParameter statementofAccountParameter)
        {
            var statementofAccount = ctx.rptsrc_StatementofAccountSavings.SingleOrDefault(x =>
                x.BranchCode == statementofAccountParameter.BranchCode && x.GroupCode ==
                                                                       statementofAccountParameter.GroupCode
                                                                       && x.AccountNumber == statementofAccountParameter
                                                                           .AccountNumber
                                                                       && (x.TransactionDate >=
                                                                           statementofAccountParameter.StartDate &&
                                                                           x.TransactionDate <=
                                                                           statementofAccountParameter.EndDate));

            if (statementofAccount == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<rptsrc_StatementofAccountSavings, SavingStatementofAccountDTO>(statementofAccount));
        }

        // POST: api/StatementofAccountSavings
        [HttpPost]
        [Route("api/statementofaccount/savings-bulk")]
        [ResponseType(typeof(rptsrc_StatementofAccountSavings))]
        public IHttpActionResult Postrptsrc_StatementofAccountSavings([FromBody] List<SavingStatementofAccountDTO> statementofAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listSA = Mapper.Map<List<rptsrc_StatementofAccountSavings>>(statementofAccount);
            if (listSA == null || listSA.Count <= 0)
            {
                return BadRequest();
            }

            try
            {
                ctx.rptsrc_StatementofAccountSavings.AddRange(listSA);
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

        private bool rptsrc_StatementofAccountSavingsExists(string id)
        {
            return ctx.rptsrc_StatementofAccountSavings.Count(e => e.BranchCode == id) > 0;
        }
    }
}