using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class IdentityDocuments
    {
        public int IdcId { get; set; }
        public int? IdcParentIdcId { get; set; }
        public int IdcEmpId { get; set; }
        public string IdcSeria { get; set; }
        public string IdcNumber { get; set; }
        public DateTime? IdsIssueDate { get; set; }
        public DateTime? IdsValidFrom { get; set; }
        public DateTime? IdsValidTo { get; set; }
        public int IdsType { get; set; }
        public string IdsVisaType { get; set; }
        public string IdsRemarks { get; set; }
        public DateTime? IdsAuditCd { get; set; }
        public string IdsAuditCu { get; set; }
        public DateTime? IdsAuditMd { get; set; }
        public string IdsAuditMu { get; set; }
        public DateTime? IdsAuditRd { get; set; }
        public string IdsAuditRu { get; set; }
    }
}
