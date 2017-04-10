using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class ClientDto : RelationSubject
    {
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Krs { get; set; }
        public string Regon { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public string Branch { get; set; }
        public int? FreeVacancies { get; set; }
        public int? AwaitingVacancies { get; set; }
        public int? BusyVacancies { get; set; }
        public List<OfferDto> Offers { get; set; }
        public List<EmployeeDto> Employees { get; set; }
        public List<EmploymentRequestDto> EmploymentRequests { get; set; }
        public List<DocumentFilesDto> Files { get; set; }
        public List<EventDetailsDto> Notes { get; set; }

        public ClientDto()
        {

        }

        public ClientDto(Clients client, IEnumerable<Addresses> addresses, IEnumerable<ContactPersons> contactPersons,
                         IEnumerable<Offer> offers, IEnumerable<Employees> acceptedEmployees, IEnumerable<DocumentFiles> files):this(client)
        {
            Addresses = addresses?.Select(a => new AddressDto(a)).ToList();
            ContactPersons = contactPersons?.Select(c => new ContactPersonDto(c)).ToList();
            Offers = offers?.Select(o => new OfferDto(o)).ToList();
            Employees = acceptedEmployees?.Select(e => new EmployeeDto(e)).ToList();
            Files = files?.Select(f => new DocumentFilesDto(f)).ToList();
            
        }

        public ClientDto(Clients client)
        {
            Id = client.CltId;
            Name = client.CltName;
            Krs = client.CltKrs;
            Nip = client.CltNip;
            Regon = client.CltRegon;
            Status = client.CltStatus;
            Type = client.CltType;
            Branch = client.CltBranch;
        }
    }
}
