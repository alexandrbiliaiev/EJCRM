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
        public string EmpAuditCu { get; set; }
        public DateTime? EmpAuditMd { get; set; }
        public string EmpAuditMu { get; set; }
        public DateTime? EmpAuditRd { get; set; }
        public string EmpAuditRu { get; set; }
    }
}
