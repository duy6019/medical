namespace PdfGenerator.Models
{
    public class ComplianceFormModel
    {
        public string? Client { get; set; }
        public string? Company { get; set; }
        public DateTime? ReportingFrom { get; set; }
        public DateTime? ReportingTo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Signature { get; set; }
        public bool CertifiedAsAccurate { get; set; }
        public DateTime? SubmitDate { get; set; }

        //reporting
        public string? QnAssignedDebtsResponse { get; set; }
        public string? QnAssignedDebtsComment { get; set; }
        public string? QnInternalComplaintsResponse { get; set; }
        public string? QnInternalComplaintsComment { get; set; }
        public string? QnExternalComplaintsResponse { get; set; }
        public string? QnExternalComplaintsComment { get; set; }
        public string? QnBackruptcyApplicationResponse { get; set; }
        public string? QnBackruptcyApplicationComment { get; set; }
        public string? QnLitigationResponse { get; set; }
        public string? QnLitigationComment { get; set; }

        //customer engagement
        public string? QnNPSScoreResponse { get; set; }
        public string? QnNPSScoreComment { get; set; }
        public string? QnQualityAssuranceResponse { get; set; }
        public string? QnQualityAssuranceComment { get; set; }
        public string? QnCallMonitoringResponse { get; set; }
        public string? QnCallMonitoringComment { get; set; }

        //declarations
        public string? QnACCCResponse { get; set; }
        public string? QnACCCComment { get; set; }
        public string? QnAdverseMediaArticlesResponse { get; set; }
        public string? QnAdverseMediaArticlesComment { get; set; }
        public string? QnThreatsMediaAttentionResponse { get; set; }
        public string? QnThreatsMediaAttentionComment { get; set; }

        //documentation - business structure
        public string? QnChangesToDocumentsResponse { get; set; }
        public string? QnChangesToDocumentsComment { get; set; }
        public string? QnBusinessStructureResponse { get; set; }
        public string? QnBusinessStructureComment { get; set; }

        public List<string> Attachments { get; set; } = new List<string>();
    }
}
