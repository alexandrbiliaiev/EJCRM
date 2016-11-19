using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class DocumentFilesDto
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? ContractId { get; set; }
        public int? IdtDocumentId { get; set; }
        public string GoogleFileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public DocumentFilesDto()
        {

        }

        public DocumentFilesDto(DocumentFiles file)
        {
            Id = file.DcfId;
            InvoiceId = file.DcfInvId;
            ContractId = file.DcfCntId;
            IdtDocumentId = file.DcfIdcId;
            GoogleFileId = file.DcfGoogleFileId;
            Name = file.DcfName;
            Description = file.DcfDescription;
            Url = file.DcfUrl;
        }
    }
}