using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Contracts
    {
        public int CntId { get; set; }
        public int CntCgtId { get; set; }
        public int CntEmpId { get; set; }
        public DateTime CntStartDate { get; set; }
        public DateTime? CntEndDate { get; set; }
        public DateTime CntIssueDate { get; set; }
        public DateTime? CntAuditCd { get; set; }
        public string CntAuditCu { get; set; }
        public DateTime? CntAuditMd { get; set; }
        public string CntAuditMu { get; set; }
        public DateTime? CntAuditRd { get; set; }
        public string CntAuditRu { get; set; }
    }
}
