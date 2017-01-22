using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Notes
    {
        public string NotId { get; set; }
        public string NotSubject { get; set; }
        public string NotText { get; set; }
        public DateTime? NotEndDate { get; set; }
        public DateTime? NotRemindDate { get; set; }
        public string NotTargetUser { get; set; }
        public int? NotCtgId { get; set; }
        public int? NotCltId { get; set; }
        public int? NotEmp { get; set; }
        public DateTime? NotAuditMd { get; set; }
        public string NotAuditMu { get; set; }
        public DateTime? NotAuditRd { get; set; }
        public string NotAuditRu { get; set; }
        public DateTime? NotStartDate { get; set; }
        public DateTime? NotAuditCd { get; set; }
        public string NotAuditCu { get; set; }
    }
}
