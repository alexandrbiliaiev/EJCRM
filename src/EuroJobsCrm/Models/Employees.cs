using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Employees
    {
        public int EmpId { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpMiddleName { get; set; }
        public string EmpLastName { get; set; }
        public DateTime EmpBirthDate { get; set; }
        public DateTime? EmpAuditCd { get; set; }
        public int? EmpAuditCu { get; set; }
        public DateTime? EmpAuditMd { get; set; }
        public int? EmpAuditMu { get; set; }
        public DateTime? EmpAuditRd { get; set; }
        public int? EmpAuditRu { get; set; }
    }
}
