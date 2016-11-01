using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class ContactPersonDto
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? ContragentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string PhoneNumber { get; set; }
        public string Messanger { get; set; }

        public ContactPersonDto()
        {
            
        }

        public ContactPersonDto(ContactPersons contactPerson)
        {
            Id = contactPerson.CtpId;
            ClientId = contactPerson.CtpCltId;
            ContragentId = contactPerson.CtpCgtId;
            Email = contactPerson.CtpEmail;
            Messanger = contactPerson.CtpMessanger;
            Name = contactPerson.CtpName;
            PhoneNumber = contactPerson.CtpPhoneNumber;
            Position = contactPerson.CtpPosition;
            Skype = contactPerson.CtpSkype;
            Surname = contactPerson.CtpSurname;
        }
    }
}
