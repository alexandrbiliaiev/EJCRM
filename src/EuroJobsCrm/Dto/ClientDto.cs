using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public List<OfferDto> Offers { get; set; }

        public ClientDto()
        {

        }

        public ClientDto(Clients client, IEnumerable<Addresses> addresses, IEnumerable<ContactPersons> contactPersons,
                         IEnumerable<Offers> offers)
        {
            Id = client.CltId;
            Name = client.CltName;
            Krs = client.CltKrs;
            Nip = client.CltNip;
            Regon = client.CltRegon;
            Status = client.CltStatus;
            Type = client.CltType;
            Branch = client.CltBranch;
            Addresses = addresses.Select(a => new AddressDto(a)).ToList();
            ContactPersons = contactPersons.Select(c => new ContactPersonDto(c)).ToList();
            Offers = offers.Select(o => new OfferDto(o)).ToList();
        }
    }
}
