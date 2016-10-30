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
        public int? CltAuditCu { get; set; }
        public DateTime? CltAuditMd { get; set; }
        public int? CltAuditMu { get; set; }
        public DateTime? CltAuditRd { get; set; }
        public int? CltAuditRu { get; set; }
    }
}
