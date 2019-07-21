using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalAPI.DTO
{
    public class SavingStatementofAccountDTO
    {
        public int id { get; set; }
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<decimal> BeginningBalance { get; set; }
        public Nullable<decimal> CashDeposits { get; set; }
        public Nullable<decimal> CheckDeposits { get; set; }
        public Nullable<decimal> CreditMemo { get; set; }
        public Nullable<decimal> Interest { get; set; }
        public Nullable<decimal> LoanDeduction { get; set; }
        public Nullable<decimal> SavingsCollection { get; set; }
        public Nullable<decimal> CashWithdrawal { get; set; }
        public Nullable<decimal> CheckWithdrawal { get; set; }
        public Nullable<decimal> DebitMemo { get; set; }
        public Nullable<decimal> ChargeBelowMinBal { get; set; }
        public Nullable<decimal> ChargeonDormancy { get; set; }
        public Nullable<decimal> LoanOffsetting { get; set; }
        public Nullable<decimal> EndingBalance { get; set; }
    }
}