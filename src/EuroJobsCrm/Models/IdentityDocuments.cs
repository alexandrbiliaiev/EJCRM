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
        public DateTime? IdcIssueDate { get; set; }
        public DateTime? IdcValidFrom { get; set; }
        public DateTime? IdcValidTo { get; set; }
        public int IdcType { get; set; }
        public string IdcVisaType { get; set; }
        public string IdcRemarks { get; set; }
        public DateTime? IdcAuditCd { get; set; }
        public string IdcAuditCu { get; set; }
        public DateTime? IdcAuditMd { get; set; }
        public string IdcAuditMu { get; set; }
        public DateTime? IdcAuditRd { get; set; }
        public string IdcAuditRu { get; set; }
    }
}
