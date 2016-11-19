using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class DocumentFiles
    {
        public int DcfId { get; set; }
        public int? DcfInvoiceId { get; set; }
        public int? DcfCntId { get; set; }
        public int? DcfIdcId { get; set; }
        public string DcfGoogleFileId { get; set; }
        public string DcfName { get; set; }
        public string DcfDescription { get; set; }
        public string DcfUrl { get; set; }
        public DateTime? DcfAuditCd { get; set; }
        public string DcfAuditCu { get; set; }
        public DateTime? DcfAuditMd { get; set; }
        public string DcfAuditMu { get; set; }
        public DateTime? DcfAuditRd { get; set; }
        public string DcfAuditRu { get; set; }
    }
}
