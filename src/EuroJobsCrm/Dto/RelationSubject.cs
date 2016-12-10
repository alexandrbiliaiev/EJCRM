using System.Collections.Generic;

namespace EuroJobsCrm.Dto
{
    public class RelationSubject : DataTransferObjectBase
    {
        public int Id { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<ContactPersonDto> ContactPersons { get; set; }
    }
}