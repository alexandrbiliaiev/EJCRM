using System.Collections.Generic;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class OfferDetailsDto : OfferDto
    {
        public List<EmployeeDto> Candidates { get; set; }
        public List<EmployeeDto> Employees { get; set; }
        public List<DocumentFilesDto> Files { get; set; }


        public OfferDetailsDto()
        {
            
        }

        public OfferDetailsDto(Offers offer) : base(offer)
        {
            
        }
    }
}
