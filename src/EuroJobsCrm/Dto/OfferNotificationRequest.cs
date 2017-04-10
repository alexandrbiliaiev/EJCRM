using System.Collections.Generic;

namespace EuroJobsCrm.Dto
{
    public class OfferNotificationRequest
    {
        public int OfferId { get; set; }
        public List<int> ContragentsIds { get; set; }
        public bool ToAll { get; set; }
    }
}