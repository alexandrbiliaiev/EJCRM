using System;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class OfferDto
    {
        public OfferDto(Offers offer)
        {
            Id = offer.OfrId;
            AccomodationPrice = offer.OfrAccomodationPrice;
            AccomodationType = offer.OfrAccomodationType;
            AdditionalInfo = offer.OfrAdditionalInfo;
            AdvanceAmount = offer.OfrAdvanceAmount;
            AgeFrom = offer.OfrAgeFrom;
            AgeTo = offer.OfrAgeTo;
            Branch = offer.OfrBranch;
            ClientId = offer.OfrCltId;
            Comments = offer.OfrComments;
            ContractType = offer.OfrContractType;
            DistanceToWork = offer.OfrDistanceToWork;
            Documents = offer.OfrDocuments;
            Education = offer.OfrEducation;
            EndingDate = offer.OfrEndingDate;
            Experience = offer.OfrExperience;
            Facilities = offer.OfrFacilities;
            HoursPerMonth = offer.OfrHoursPerMonth;
            Languages = offer.OfrLanguages;
            OvertimeRate = offer.OfrOvertimeRate;
            PaymentMethod = offer.OfrPaymentMethod;
            RatePerHour = offer.OfrRatePerHour;
            RatePerMonth = offer.OfrRatePerMonth;
            Responsibilities = offer.OfrResponsibilities;
            RoomPeopleNumber = offer.OfrRoomPeopleNumber;
            StartingDate = offer.OfrStartingDate;
            TransportPrice = offer.OfrTransportPrice;
            TransportToWork = offer.OfrTransportToWork;
            VacanciesNumber = offer.OfrVacanciesNumber;
            WorkDays = offer.OfrWorkDays;
            WorkEnd = offer.OfrWorkEnd;
            WorkPlace = offer.OfrWorkPlace;
            WorkStart = offer.OfrWorkStart;
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Branch { get; set; }
        public int VacanciesNumber { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string WorkPlace { get; set; }
        public string Responsibilities { get; set; }
        public int Education { get; set; }
        public string Experience { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string Languages { get; set; }
        public string Comments { get; set; }
        public int ContractType { get; set; }
        public DateTime? WorkStart { get; set; }
        public DateTime? WorkEnd { get; set; }
        public double? OvertimeRate { get; set; }
        public int? HoursPerMonth { get; set; }
        public int WorkDays { get; set; }
        public double? RatePerHour { get; set; }
        public double? RatePerMonth { get; set; }
        public int PaymentMethod { get; set; }
        public double? AdvanceAmount { get; set; }
        public int AccomodationType { get; set; }
        public double AccomodationPrice { get; set; }
        public int RoomPeopleNumber { get; set; }
        public double DistanceToWork { get; set; }
        public int? TransportToWork { get; set; }
        public double TransportPrice { get; set; }
        public string Facilities { get; set; }
        public string AdditionalInfo { get; set; }
        public string Documents { get; set; }
    }
}