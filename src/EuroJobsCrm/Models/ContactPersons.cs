using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class ContactPersons
    {
        public int CtpId { get; set; }
        public int? CtpCltId { get; set; }
        public int? CtpCgtId { get; set; }
        public string CtpName { get; set; }
        public string CtpSurname { get; set; }
        public string CtpPosition { get; set; }
        public string CtpEmail { get; set; }
        public string CtpSkype { get; set; }
        public string CtpPhoneNumber { get; set; }
        public string CtpMessanger { get; set; }
        public int? CtpMessangerType { get; set; }
        public DateTime? CtpAuditCd { get; set; }
        public string CtpAuditCu { get; set; }
        public DateTime? CtpAuditMd { get; set; }
        public string CtpAuditMu { get; set; }
        public DateTime? CtpAuditRd { get; set; }
        public string CtpAuditRu { get; set; }

    }
}
