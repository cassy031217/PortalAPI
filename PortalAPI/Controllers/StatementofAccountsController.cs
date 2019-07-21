using PortalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PortalAPI.DTO;
using PortalAPI.Models;
using System.Web.Http.Description;

namespace PortalAPI.Controllers
{
    public class StatementofAccountsController : ApiController
    {
        private readonly Portal_DBEntities ctx;

        public StatementofAccountsController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Return a list of savings statement of account
        /// </summary>
        /// <returns></returns>
        // GET: api/StatementofAccountSavings
        [HttpGet]
        [Route("api/statementofaccount/savings")]
        [ResponseType(typeof(List<SavingStatementofAccountDTO>))]
        public IHttpActionResult GetStatementofAccountSavings()
        {
            return Ok(ctx.rptsrc_StatementofAccountSavings.ToList()
                .Select(Mapper.Map<rptsrc_StatementofAccountSavings, SavingStatementofAccountDTO>));
        }

        /// <summary>
        /// Find a saving statement of accounts by Branch Code, Group Code, Account Number and Start, Ending date
        /// </summary>
        /// <param name="statementofAccountParameter"></param>
        /// <returns></returns>
        // GET: api/StatementofAccountSavings/5
        [HttpGet]
        [Route("api/statementofaccount/saving")]
        [ResponseType(typeof(SavingStatementofAccountDTO))]
        public IHttpActionResult Getrptsrc_StatementofAccountSavings([FromUri] StatementofAccountParameter statementofAccountParameter)
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

        /// <summary>
        /// bulk posting of savings statement of account
        /// </summary>
        /// <param name="statementofAccount"></param>
        /// <returns></returns>
        // POST: api/StatementofAccountSavings
        [HttpPost]
        [Route("api/statementofaccount/savings-bulk")]
        [ResponseType(typeof(rptsrc_StatementofAccountSavings))]
        [Authorize(Roles = "Admin")]
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


        /// <summary>
        /// Return a list of loans Statement of Account 
        /// </summary>
        /// <returns></returns>
        // GET: api/StatementofAccountLoans
        [HttpGet]
        [Route("api/StatementofAccount/loan")]
        [ResponseType(typeof(List<LoanStatementofAccountDTO>))]
        public IHttpActionResult GetAllStatementofAccountloans()
        {
            return Ok(ctx.rptsrc_StatementofAccountLoans.ToList()
                .Select(Mapper.Map<rptsrc_StatementofAccountLoans, LoanStatementofAccountDTO>));
        }

        /// <summary>
        /// Find a statement of account by Loan account id and Statement Date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statementDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/statementofaccount/loan/{id}/{statementDate}")]
        [ResponseType(typeof(List<LoanStatementofAccountDTO>))]
        public IHttpActionResult GetStatementofAccountLoans([FromUri] int id, [FromUri] DateTime statementDate)
        {

            List<rptsrc_StatementofAccountLoans> listofSA = ctx.rptsrc_StatementofAccountLoans.Where(x =>
                x.LoanAccountId == id
                && (x.TransactionDate.Value.Year == statementDate.Year && x.TransactionDate.Value.Month == statementDate.Month)).ToList();

            if (listofSA == null || listofSA.Count <= 0)
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
        [Authorize(Roles = "Admin")]
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
    }
}
