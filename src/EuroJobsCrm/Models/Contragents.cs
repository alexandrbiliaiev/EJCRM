using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Contragents
    {
        public int CgtId { get; set; }
        public string CgtName { get; set; }
        public string CgtLicenseNumber { get; set; }
        public string CgtStatus { get; set; }
        public DateTime? CgtAuditCd { get; set; }
        public int? CgtAuditCu { get; set; }
        public DateTime? CgtAuditMd { get; set; }
        public DateTime? CgtAuditMu { get; set; }
        public DateTime? CgtAuditRd { get; set; }
        public DateTime? CgtAuditRu { get; set; }
    }
}
