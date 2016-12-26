using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Clients
    {
        public int CltId { get; set; }
        public string CltName { get; set; }
        public string CltNip { get; set; }
        public string CltKrs { get; set; }
        public string CltRegon { get; set; }
        public DateTime? CltAuditCd { get; set; }
        public string CltAuditCu { get; set; }
        public DateTime? CltAuditMd { get; set; }
        public string CltAuditMu { get; set; }
        public DateTime? CltAuditRd { get; set; }
        public string CltAuditRu { get; set; }
        public int? CltStatus { get; set; }
        public int? CltType { get; set; }
        public string CltBranch { get; set; }
    }
}
