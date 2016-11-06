using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Addresses
    {
        public int AdrId { get; set; }
        public int? ArdCltId { get; set; }
        public int? ArdEmpId { get; set; }
        public int? AdrCgtId { get; set; }
        public string AdrType { get; set; }
        public string AdrPostCode { get; set; }
        public string AdrCity { get; set; }
        public string AdrCountry { get; set; }
        public DateTime? AdrAuditCd { get; set; }
        public string AdrAuditCu { get; set; }
        public DateTime? AdrAuditMd { get; set; }
        public string AdrAduitMu { get; set; }
        public DateTime? AdrAuditRd { get; set; }
        public string AdrAduitRu { get; set; }
        public string AdrAddress { get; set; }
    }
}
