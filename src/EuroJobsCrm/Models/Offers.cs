using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class Offers
    {
        public int OfrId { get; set; }
        public int OfrCltId { get; set; }
        public int OfrBranch { get; set; }
        public int OfrVacanciesNumber { get; set; }
        public DateTime OfrStartingDate { get; set; }
        public DateTime OfrEndingDate { get; set; }
        public string OfrWorkPlace { get; set; }
        public string OfrResponsibilities { get; set; }
        public int OfrEducation { get; set; }
        public string OfrExperience { get; set; }
        public int? OfrAgeFrom { get; set; }
        public int? OfrAgeTo { get; set; }
        public string OfrLanguages { get; set; }
        public string OfrComments { get; set; }
        public int OfrContractType { get; set; }
        public DateTime? OfrWorkStart { get; set; }
        public DateTime? OfrWorkEnd { get; set; }
        public double? OfrOvertimeRate { get; set; }
        public int? OfrHoursPerMonth { get; set; }
        public int OfrWorkDays { get; set; }
        public double? OfrRatePerHour { get; set; }
        public double? OfrRatePerMonth { get; set; }
        public int OfrPaymentMethod { get; set; }
        public double? OfrAdvanceAmount { get; set; }
        public int OfrAccomodationType { get; set; }
        public double OfrAccomodationPrice { get; set; }
        public int OfrRoomPeopleNumber { get; set; }
        public double OfrDistanceToWork { get; set; }
        public int? OfrTransportToWork { get; set; }
        public double OfrTransportPrice { get; set; }
        public string OfrFacilities { get; set; }
        public string OfrAdditionalInfo { get; set; }
        public string OfrDocuments { get; set; }
        public DateTime? OfrAuditCd { get; set; }
        public string OfrAuditCu { get; set; }
        public DateTime? OfrAuditMd { get; set; }
        public string OfrAuditMu { get; set; }
        public DateTime? OfrAuditRd { get; set; }
        public string OfrAuditRu { get; set; }
        public int OfrGender { get; set; }
        public string OfrPosition { get; set; }
    }
}
