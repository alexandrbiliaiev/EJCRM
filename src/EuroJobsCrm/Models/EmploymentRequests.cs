using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class EmploymentRequests
    {
        public int EtrId { get; set; }
        public int EtrOfrId { get; set; }
        public int EtrEmpId { get; set; }
        public int EtrStatus { get; set; }
        public int? EtrCntId { get; set; }
        public DateTime? EtrAuditCd { get; set; }
        public string EtrAuditCu { get; set; }
        public DateTime? EtrAuditMd { get; set; }
        public string EtrAuditMu { get; set; }
        public DateTime? EtrAuditRd { get; set; }
        public string EtrAuditRu { get; set; }
    }
}
