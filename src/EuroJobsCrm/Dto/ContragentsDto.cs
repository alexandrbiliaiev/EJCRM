using System;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class ContragentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime? CreationDate { get; set; }

        public ContragentsDto()
        {
            
        }

        public ContragentsDto(Contragents contragent)
        {
            Id = contragent.CgtId;
            Name = contragent.CgtName;
            LicenseNumber = contragent.CgtLicenseNumber;
            Status = contragent.CgtStatus;
            CreationDate = contragent.CgtAuditCd;

        }
    }


}
