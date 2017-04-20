using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Contragent
    {
        public int CgtId { get; set; }
        public string CgtName { get; set; }
        public string CgtLicenseNumber { get; set; }
        public string CgtStatus { get; set; }
        public DateTime? CgtAuditCd { get; set; }
        public string CgtAuditCu { get; set; }
        public DateTime? CgtAuditMd { get; set; }
        public string CgtAuditMu { get; set; }
        public DateTime? CgtAuditRd { get; set; }
        public string CgtAuditRu { get; set; }
        public string CgtResponsibleUser { get; set; }
        public string CgtInn { get; set; }
        public bool CgtSubscription { get; set; } 
    }
}
