using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class IdentityDocumentsDto : DataTransferObjectBase
    {
        public int Id { get; set; }
        public int? ParentDocumentId { get; set; }
        public int EmployeeId { get; set; }
        public string Seria { get; set; }
        public string Number { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        /// <summary>
        /// 1 - passport
        /// 2 - visa
        /// </summary>
        public int Type { get; set; }
        public string VisaType { get; set; }
        public string Remarks { get; set; }
        public List<DocumentFilesDto> Files { get; set; }

        public IdentityDocumentsDto()
        {

        }

        public IdentityDocumentsDto(IdentityDocuments document)
        {
            Id = document.IdcId;
            ParentDocumentId = document.IdcParentIdcId;
            EmployeeId = document.IdcEmpId;
            Seria = document.IdcSeria;
            Number = document.IdcNumber;
            IssueDate = document.IdcIssueDate;
            ValidFrom = document.IdcValidFrom;
            ValidTo = document.IdcValidTo;
            Type = document.IdcType;
            VisaType = document.IdcVisaType;
            Remarks = document.IdcRemarks;
        }

        public IdentityDocumentsDto(IdentityDocuments document, IEnumerable<DocumentFiles> files) : this(document)
        {
            Files = files.Select(f => new DocumentFilesDto(f)).ToList();
        }
    }
}