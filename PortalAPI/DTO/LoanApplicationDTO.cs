using System;

namespace PortalAPI.DTO
{
    public class LoanApplicationDTO
    {
        /// <summary>
        /// Loan Application Number
        /// </summary>
        public int LAno { get; set; }

        /// <summary>
        /// Member Code
        /// </summary>
        public string MemberCode { get; set; }

        public Nullable<decimal> AppliedAmount { get; set; }
        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public string Product { get; set; }
        public Nullable<int> TermsQty { get; set; }
        public string ApplicationFormPath { get; set; }
        public string File1Path { get; set; }
        public string File2Path { get; set; }
        public string File3Path { get; set; }
        public string File4Path { get; set; }
        public string File5Path { get; set; }
        public string VerificationStatus { get; set; }
        public string VerificationRemarks { get; set; }
        public Nullable<System.DateTime> DateVerified { get; set; }
        public string EvaluationStatus { get; set; }
        public Nullable<System.DateTime> DateEvaluated { get; set; }
        public Nullable<bool> IsChangeLoanAmount { get; set; }
        public Nullable<bool> IsChangeTerm { get; set; }
        public Nullable<bool> IsAddRequirement { get; set; }
        public Nullable<bool> IsAgreeToCondition { get; set; }
        public string ClientEvalRemarks { get; set; }
        public string EvaluatorRemarks { get; set; }
        public string ApprovalStatus { get; set; }
        public Nullable<System.DateTime> DateApproved { get; set; }
        public string ApproverRemarks { get; set; }
        public string AddedReqPath1 { get; set; }
        public string AddedReqPath2 { get; set; }
        public string AddedReqPath3 { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public string ReleaseRemarks { get; set; }
    }
}