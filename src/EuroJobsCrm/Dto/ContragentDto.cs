using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class ContragentDto : RelationSubject
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime? CreationDate { get; set; }
      

        public ContragentDto()
        {
            Addresses = new List<AddressDto>();
            ContsctPersons = new List<ContactPersonDto>();
        }

        public ContragentDto(Contragents contragent, IEnumerable<Addresses> addresses, IEnumerable<ContactPersons> contactPersons )
        {
            Id = contragent.CgtId;
            Name = contragent.CgtName;
            LicenseNumber = contragent.CgtLicenseNumber;
            Status = contragent.CgtStatus;
            CreationDate = contragent.CgtAuditCd;
            Addresses = addresses.Select(a => new AddressDto(a)).ToList();
            ContsctPersons = contactPersons.Select(c => new ContactPersonDto(c)).ToList();
        }

        public ContragentDto(Contragents contragent)
        {
            Id = contragent.CgtId;
            Name = contragent.CgtName;
            LicenseNumber = contragent.CgtLicenseNumber;
            Status = contragent.CgtStatus;
            CreationDate = contragent.CgtAuditCd;
        }
    }


}
