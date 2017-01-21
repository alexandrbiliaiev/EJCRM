using EuroJobsCrm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroJobsCrm.Dto
{
    public class EventDetailsDto : EventDto
    {
        public string TargetUserName { get; set; }
        public string ContragentName { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }

        public EventDetailsDto(Notes n) : base(n)
        {

        }

    }
}
