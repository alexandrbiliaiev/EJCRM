using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EuroJobsCrm.Dto
{
    public class CandidateOfferRequestDto : DataTransferObjectBase
    {
        public int OfferId { get; set; }
        public int ContragentId { get; set; }

    }
}
