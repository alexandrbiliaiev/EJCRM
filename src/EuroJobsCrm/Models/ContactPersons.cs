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
        public DateTime? CtpCd { get; set; }
        public string CtpCu { get; set; }
        public DateTime? CtpMd { get; set; }
        public string CtpMu { get; set; }
        public DateTime? CtpRd { get; set; }
        public string CtpRu { get; set; }
    }
}
