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
        public IEnumerable<EmployeeDto> Employees { get; set; }

        public ContragentDto()
        {
            Addresses = new List<AddressDto>();
            ContactPersons = new List<ContactPersonDto>();
        }

        public ContragentDto(Contragents contragent, IEnumerable<Addresses> addresses, 
                IEnumerable<ContactPersons> contactPersons, IEnumerable<Employees> employees)
        {
            Id = contragent.CgtId;
            Name = contragent.CgtName;
            LicenseNumber = contragent.CgtLicenseNumber;
            Status = contragent.CgtStatus;
            CreationDate = contragent.CgtAuditCd;
            Addresses = addresses.Select(a => new AddressDto(a)).ToList();
            ContactPersons = contactPersons.Select(c => new ContactPersonDto(c)).ToList();
            Employees = employees.Select(e => new EmployeeDto(e)).ToList();
        }

        public ContragentDto(Contragents contragent)
        {
            Id = contragent.CgtId;
            Name = contragent.CgtName;
            LicenseNumber = contragent.CgtLicenseNumber;
            Status = contragent.CgtStatus;
            CreationDate = contragent.CgtAuditCd;
            Employees= new List<EmployeeDto>();
        }
    }


}
