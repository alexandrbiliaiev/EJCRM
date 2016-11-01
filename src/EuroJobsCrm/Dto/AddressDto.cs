using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class AddressDto
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? EmploeeId { get; set; }
        public int? ContragentId { get; set; }
        public string Type { get; set; }
        public string Pay { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

        public AddressDto()
        {
            
        }

        public AddressDto(Addresses address)
        {
            Id = address.AdrId;
            Address = address.AdrAddress;
            City = address.AdrCity;
            ClientId = address.ArdCltId;
            ContragentId = address.AdrCgtId;
            Country = address.AdrCountry;
            EmploeeId = address.ArdEmpId;
            Pay = address.AdrPay;
            PostCode = address.AdrPostCode;
            Type = address.AdrType;
        }
    }
}