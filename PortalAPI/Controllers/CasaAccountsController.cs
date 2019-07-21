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
using EntityFramework.Extensions;
using PortalAPI.DTO;
using PortalAPI.Models;

namespace PortalAPI.Controllers
{
    public class CasaAccountsController : ApiController
    {
        private readonly Portal_DBEntities ctx;

        public CasaAccountsController()
        {
            ctx = new Portal_DBEntities();
        }

        /// <summary>
        /// Return a list of casa account
        /// </summary>
        /// <returns></returns>
        // GET: api/CasaAccounts
        [HttpGet]
        [Route("api/casaaccount")]
        [ResponseType(typeof(CasaAccountDTO))]
        [AllowAnonymous]
        public IHttpActionResult GetCasaAccounts()
        {
            return Ok(ctx.CasaAccounts.ToList()
                .Select(Mapper.Map<CasaAccount, CasaAccountDTO>));
        }

        /// <summary>
        /// return a list of casa account by cif key
        /// </summary>
        /// <param name="cifkey"></param>
        /// <returns></returns>
        // GET: api/CasaAccounts/5
        [HttpGet]
        [Route("api/casaaccount/{cifkey}")]
        [ResponseType(typeof(CasaAccountDTO))]
        public IHttpActionResult GetCasaAccount(string cifkey)
        {
            List<CasaAccount> casaAccount = ctx.CasaAccounts.Where(x => x.CIFkey == cifkey && x.AccountStatus.Contains("Active")).ToList();
            if (casaAccount == null || casaAccount.Count < 1)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IList<CasaAccount>, IList<CasaAccountDTO>>(casaAccount));
        }

        ///// <summary>
        ///// Find a casa account by Branch Code, Group Code, Account Number and cifkey
        ///// </summary>
        ///// <param name="branchCode"></param>
        ///// <param name="groupCode"></param>
        ///// <param name="accountNumber"></param>
        ///// <param name="cifkey"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("api/casaaccount/{branchCode}/{groupCode}/{accountNumber}/{cifkey}")]
        //[ResponseType(typeof(CasaAccountDTO))]
        //public IHttpActionResult GetCasaAccount(string branchCode, string groupCode, string accountNumber, string cifkey)
        //{
        //    var casaAccount = ctx.CasaAccounts.SingleOrDefault(x =>
        //        x.BranchCode == branchCode && x.GroupCode == groupCode && x.AccountNumber == accountNumber
        //        && x.CIFkey == cifkey && x.AccountStatus.Contains("Active"));

        //    if (casaAccount == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Mapper.Map<CasaAccount, CasaAccountDTO>(casaAccount));
        //}

        /// <summary>
        /// Find a casa account by Branch Code, Group Code, Account Number and cifkey
        /// </summary>
        /// <param name="casaAccountParameter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/casaaccount/header")]
        [ResponseType(typeof(CasaAccountDTO))]
        public IHttpActionResult GetCasaAccount([FromBody] CasaAccountParameter casaAccountParameter)
        {
            var casaAccount = ctx.CasaAccounts.SingleOrDefault(x =>
                x.BranchCode == casaAccountParameter.BranchCode && x.GroupCode == casaAccountParameter.GroupCode && x.AccountNumber == casaAccountParameter.AccountNumber
                && x.CIFkey == casaAccountParameter.cifkey && x.AccountStatus.Contains("Active"));

            if (casaAccount == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CasaAccount, CasaAccountDTO>(casaAccount));
        }

        /// <summary>
        /// Find a casa ledger by Branch Code, Group Code, Account Number and cifkey
        /// </summary>
        /// <param name="casaAccountParameter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/casaaccount/ledger")]
        [ResponseType(typeof(List<CasaLedgerDTO>))]
        public IHttpActionResult GetCasaLedger([FromBody] CasaAccountParameter casaAccountParameter)
        {
            DateTime now = DateTime.Now;
            DateTime twoMonthAgo = now.AddMonths(-2);
            var ledger = ctx.CasaLedgers.Where(x =>
                x.BranchCode == casaAccountParameter.BranchCode && x.GroupCode == casaAccountParameter.GroupCode 
                                                                && x.AccountNumber == casaAccountParameter.AccountNumber
                                                                && x.TransactionDate >= twoMonthAgo).ToList();

            if (ledger == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IList<CasaLedger>, IList<CasaLedgerDTO>>(ledger));
        }

        /// <summary>
        /// Bulk update or create of casa account enity
        /// </summary>
        /// <param name="casaAccountDto"></param>
        /// <returns></returns>
        // POST: api/CasaAccounts
        [HttpPost]
        [Route("api/casaaccount/header-bulk")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostCasaAccount([FromBody] List<CasaAccountDTO> casaAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get reference  list of Casa Account
           var ListofAccountRef = Mapper.Map<IList<CasaAccount>>(casaAccountDto);
           var listRef = ListofAccountRef.Select(x => x.BranchCode + x.GroupCode + x.AccountNumber + x.CIFkey).ToList();


            // Get  list of Casa Account already exist on database
            var ListofAccountToUpdate = ctx.CasaAccounts.Where(itemdb =>
               listRef.Contains(itemdb.BranchCode + itemdb.GroupCode + itemdb.AccountNumber + itemdb.CIFkey)
           ).ToList();

           if (ListofAccountToUpdate.Count > 0)
           {

                var matchetAccList = ListofAccountToUpdate.Select(x => x.BranchCode + x.GroupCode + x.AccountNumber + x.CIFkey).ToList();

                var matchedAccountListSource = ListofAccountRef.Where(itemref =>
                        matchetAccList.Contains(itemref.BranchCode + itemref.GroupCode + itemref.AccountNumber +
                                                 itemref.CIFkey)).ToList();
                if (matchedAccountListSource.Count > 0)
                {
                    foreach (var item in matchedAccountListSource)
                    {
                        var caupdate = ListofAccountToUpdate.SingleOrDefault(x =>
                            x.BranchCode == item.BranchCode && x.GroupCode == item.GroupCode
                                                            && x.AccountNumber == item.AccountNumber &&
                                                            x.CIFkey == item.CIFkey);
                        if (caupdate != null)
                        {
                            caupdate.AccountName = item.AccountName;
                            caupdate.AccountStatus = item.AccountStatus;
                            caupdate.AccountBalance = item.AccountBalance;
                            caupdate.DateClosed = item.DateClosed;
                            caupdate.PassbookNumber = item.PassbookNumber;
                            caupdate.HoldBalance = item.HoldBalance;
                            caupdate.MaturityDate = item.MaturityDate;
                            caupdate.Principal = item.Principal;
                            caupdate.Terms = item.Terms;
                            caupdate.InterestRate = item.InterestRate;


                        }

                    }
                }
               

                var ListofAccountToAdd = ListofAccountRef.Where(itemref =>
                    !matchetAccList.Contains(itemref.BranchCode + itemref.GroupCode + itemref.AccountNumber +
                                             itemref.CIFkey));

                if (ListofAccountToAdd.Count() > 0)
                {
                    ctx.CasaAccounts.AddRange(ListofAccountToAdd);
                }

            }
           else
           {
               ctx.CasaAccounts.AddRange(ListofAccountRef);
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

        /// <summary>
        /// Bulk posting of casa ledger entity
        /// </summary>
        /// <param name="casaLedgerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/casaaccount/ledger-bulk")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostCasaledger([FromBody] List<CasaLedgerDTO> casaLedgerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ledger = Mapper.Map<List<CasaLedger>>(casaLedgerDto);
            if (ledger == null || ledger.Count < 1)
            {
                return BadRequest();
            }

            ctx.CasaLedgers.AddRange(ledger);

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CasaAccountExists(string id)
        {
            return ctx.CasaAccounts.Count(e => e.BranchCode == id) > 0;
        }
    }
}