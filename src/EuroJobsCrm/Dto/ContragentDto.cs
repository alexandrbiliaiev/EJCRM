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
        public UserDto ResponsibleUser { get; set; }
        public IEnumerable<DocumentFilesDto> Files { get; set; }
        public List<EventDetailsDto> Notes { get; set; }

        public IEnumerable<UserDto> ContragentUsers { get; set; }

        public ContragentDto()
        {
            Addresses = new List<AddressDto>();
            ContactPersons = new List<ContactPersonDto>();
            ContragentUsers = new List<UserDto>();
        }

        public ContragentDto(Contragents contragent)
        {
            Id = contragent.CgtId;
            Name = contragent.CgtName;
            LicenseNumber = contragent.CgtLicenseNumber;
            Status = contragent.CgtStatus;
            CreationDate = contragent.CgtAuditCd;
            Employees = new List<EmployeeDto>();
            ContragentUsers = new List<UserDto>();
        }

        public ContragentDto(Contragents contragent, IEnumerable<Addresses> addresses, 
                IEnumerable<ContactPersons> contactPersons, IEnumerable<Employees> employees,
                IEnumerable<DocumentFiles> files, AspNetUsers responsibleUser, 
                IEnumerable<UserDto> contragentUsers) : this(contragent)
        {
            Addresses = addresses.Select(a => new AddressDto(a)).ToList();
            ContactPersons = contactPersons.Select(c => new ContactPersonDto(c)).ToList();
            Employees = employees.Select(e => new EmployeeDto(e)).ToList();
            ResponsibleUser = new UserDto(responsibleUser);
            Files = files.Select(f => new DocumentFilesDto(f)).ToList();
            ContragentUsers = contragentUsers;
        }

 
    }


}
