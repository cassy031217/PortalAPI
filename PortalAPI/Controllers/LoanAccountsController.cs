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
    public class LoanAccountsController : ApiController
    {
        private Portal_DBEntities ctx;

        public LoanAccountsController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Return a list of all loan account
        /// </summary>
        /// <returns></returns>
        // GET: api/LoanAccounts
        [HttpGet]
        [Route("api/loanaccount")]
        [ResponseType(typeof(LoanAccountDTO))]
        public IHttpActionResult GetLoanAccounts()
        {
            return Ok(ctx.LoanAccounts.ToList()
                .Select(Mapper.Map<LoanAccount, LoanAccountDTO>));
        }

        /// <summary>
        /// Find a list of loan account by cifkey
        /// </summary>
        /// <param name="cifkey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/loanaccount/{cifkey}")]
        public IHttpActionResult GetLoanAccounts([FromUri] string cifkey)
        {
            var loans = ctx.LoanAccounts.Where(x => x.CIFKey == cifkey && x.AccountStatus.Contains("active")).ToList();

            if (loans == null || loans.Count <= 0)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<List<LoanAccount>, List<LoanAccountDTO>>(loans));
        }
        /// <summary>
        /// Find a loan account by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/LoanAccounts/5
        [HttpGet]
        [Route("api/loanaccount/{id}")]
        [ResponseType(typeof(LoanAccountDTO))]
        public IHttpActionResult GetLoanAccount([FromUri] int id)
        {
            LoanAccount loan = ctx.LoanAccounts.SingleOrDefault(x => x.id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<LoanAccount, LoanAccountDTO>(loan));
        }

        /// <summary>
        /// Return a list of loan ledger by Loan account id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/loanaccount/{id}/ledger")]
        public IHttpActionResult GetLoanLedger([FromUri] int id)
        {
            var ledger = ctx.LoanLedgers.Where(x => x.LoanAccountId == id).ToList();

            if (ledger == null || ledger.Count <= 0)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<List<LoanLedger>, List<LoanLedgerDTO>>(ledger));
        }

        /// <summary>
        /// Bulk posting of loan account
        /// </summary>
        /// <param name="loanAccount"></param>
        /// <returns></returns>
        // POST: api/LoanAccounts
        [HttpPost]
        [Route("api/loanaccount-bulk")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostLoanAccount([FromBody] List<LoanAccountDTO> loanAccountDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get reference list of account
            var listofAccountsrc = Mapper.Map<List<LoanAccount>>(loanAccountDtos);
            var listRef = listofAccountsrc.Select(x => x.id);

            // Get a list of loan account that already exist in database
            var ListofAccountExistonDB = ctx.LoanAccounts.Where(itemdb => listRef.Contains(itemdb.id)).ToList();

            if ( ListofAccountExistonDB.Count > 0)
            {
                var matchedAcctList = ListofAccountExistonDB.Select(x => x.id).ToList();

                var matchedAcctlistsrc = listofAccountsrc.Where(itemsrc =>
                    matchedAcctList.Contains(itemsrc.id)).ToList();

                if ( matchedAcctlistsrc.Count > 0)
                {
                    foreach (var item in matchedAcctlistsrc)
                    {
                        var singleAcct = ListofAccountExistonDB.SingleOrDefault(x => x.id == item.id);

                        if (singleAcct != null)
                        {
                            singleAcct.AccountName = item.AccountName;
                            singleAcct.LoanAmount = item.LoanAmount;
                            singleAcct.PrincipalBalance = item.PrincipalBalance;
                            singleAcct.InterestBalance = item.InterestBalance;
                            singleAcct.ReleaseDate = item.ReleaseDate;
                            singleAcct.MaturityDate = item.MaturityDate;
                            singleAcct.AccountStatus = item.AccountStatus;
                            singleAcct.ModeOfPayment = item.ModeOfPayment;
                            singleAcct.FrequencyOfPayment = item.FrequencyOfPayment;
                            singleAcct.NumberOfPayments = item.NumberOfPayments;
                            singleAcct.InterestRate = item.InterestRate;
                            singleAcct.InterestMOP = item.InterestMOP;
                            singleAcct.InterestFrequency = item.InterestFrequency;
                            singleAcct.InterestComputationMethod = item.InterestComputationMethod;
                            singleAcct.InterestComputationFrequency = item.InterestComputationFrequency;

                        }
                    }
                }

                var listofAccountNew = listofAccountsrc.Where(itemsrc => !matchedAcctList.Contains(itemsrc.id));
                if (listofAccountNew.Count() > 0)
                {
                    ctx.LoanAccounts.AddRange(listofAccountNew);
                }

            }
            else
            {
                ctx.LoanAccounts.AddRange(listofAccountsrc);
            }
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

        [HttpPost]
        [Route("api/loanaccount/ledger-bulk")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostLoanLedger([FromBody] List<LoanLedgerDTO> loanLedgerDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ledger = Mapper.Map<List<LoanLedger>>(loanLedgerDtos);
            if (ledger == null || ledger.Count < 1)
            {
                return BadRequest();
            }

            try
            {
                ctx.LoanLedgers.AddRange(ledger);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        private bool LoanAccountExists(int id)
        {
            return ctx.LoanAccounts.Count(e => e.id == id) > 0;
        }
    }
}