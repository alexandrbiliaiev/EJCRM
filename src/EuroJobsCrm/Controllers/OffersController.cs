using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public class OffersController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnUrl = Request.Path
                });
            }
            return View();
        }



        [HttpGet]
        [Route("/api/Offers")]
        public IEnumerable<OfferDto> GetOffersList()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<OfferDto> offers = context.Offers.Where(o => o.OfrAuditRd == null).ToList()
                                                      .Select(o => new OfferDto(o))
                                                      .ToList();

                return offers;
            }
        }

        [HttpPost]
        [Route("/api/Offers/Get")]
        public OfferDetailsDto GetOffer([FromBody] int offerId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Offers ofr = context.Offers.FirstOrDefault(c => c.OfrId == offerId);

                List<EmployeeDto> candidates =
                    context.EmploymentRequests.Where(r => r.EtrOfrId == offerId && r.EtrStatus == 0)
                        .Join(context.Employees, r => r.EtrEmpId, e => e.EmpId,
                            (request, employee) => new {request, employee})
                        .Select(e => new EmployeeDto(e.employee)).ToList();

                List<EmployeeDto> employees =
                  context.EmploymentRequests.Where(r => r.EtrOfrId == offerId && r.EtrStatus == 1)
                      .Join(context.Employees, r => r.EtrEmpId, e => e.EmpId,
                          (request, employee) => new { request, employee })
                      .Select(e => new EmployeeDto(e.employee)).ToList();

                OfferDetailsDto offerDetails = new OfferDetailsDto(ofr)
                {
                    Candidates = candidates,
                    Employees = employees
                };

                return offerDetails;
            }
        }


        [HttpGet]
        [Route("/api/ClientOffers")]
        public IEnumerable<OfferDto> GetClientOffers(int clientId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<OfferDto> offers = context.Offers.Where(o => o.OfrCltId == clientId &&
                                                                  o.OfrAuditRd == null)
                                                      .Select(o => new OfferDto(o))
                                                      .ToList();

               return offers;
            }
        }

        [HttpPost]
        [Route("/api/Offers/Save")]
        public OfferDto SaveOffer([FromBody] OfferDto offer)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Offers ofr;
                if (offer.Id != 0)
                {
                    ofr = context.Offers.FirstOrDefault(c => c.OfrId == offer.Id);
                }
                else
                {
                    ofr = new Offers
                    {
                        OfrAuditCd = DateTime.UtcNow,
                        OfrAuditCu = User.GetUserId()
                    };
                    context.Offers.Add(ofr);
                }

                if (ofr == null)
                {
                    return null;
                }

                offer.SetWorkDaysBitMap();

                ofr.OfrId = offer.Id;
                ofr.OfrAccomodationPrice = offer.AccomodationPrice;
                ofr.OfrAccomodationType = offer.AccomodationType;
                ofr.OfrAdditionalInfo = offer.AdditionalInfo;
                ofr.OfrAdvanceAmount = offer.AdvanceAmount;
                ofr.OfrAgeFrom = offer.AgeFrom;
                ofr.OfrAgeTo = offer.AgeTo;
                ofr.OfrBranch = offer.Branch;
                ofr.OfrCltId = offer.ClientId;
                ofr.OfrComments = offer.Comments;
                ofr.OfrContractType = offer.ContractType;
                ofr.OfrDistanceToWork = offer.DistanceToWork;
                ofr.OfrDocuments = offer.Documents;
                ofr.OfrEducation = offer.Education;
                ofr.OfrEndingDate = offer.EndingDate;
                ofr.OfrExperience = offer.Experience;
                ofr.OfrFacilities = offer.Facilities;
                ofr.OfrHoursPerMonth = offer.HoursPerMonth;
                ofr.OfrLanguages = offer.Languages;
                ofr.OfrOvertimeRate = offer.OvertimeRate;
                ofr.OfrPaymentMethod = offer.PaymentMethod;
                ofr.OfrRatePerHour = offer.RatePerHour;
                ofr.OfrRatePerMonth = offer.RatePerMonth;
                ofr.OfrResponsibilities = offer.Responsibilities;
                ofr.OfrRoomPeopleNumber = offer.RoomPeopleNumber;
                ofr.OfrStartingDate = offer.StartingDate;
                ofr.OfrTransportPrice = offer.TransportPrice;
                ofr.OfrTransportToWork = offer.TransportToWork;
                ofr.OfrVacanciesNumber = offer.VacanciesNumber;
                ofr.OfrWorkDays = offer.WorkDays;
                ofr.OfrWorkEnd = offer.WorkEnd;
                ofr.OfrWorkPlace = offer.WorkPlace;
                ofr.OfrWorkStart = offer.WorkStart;
                ofr.OfrAuditMd = DateTime.UtcNow;
                ofr.OfrAuditMu = User.GetUserId();
                ofr.OfrPosition = offer.Position;
                ofr.OfrGender = offer.Gender;

                context.SaveChanges();
                offer.Id = ofr.OfrId;
                return offer;
            }
        }

        [HttpPost]
        [Route("/api/Offers/Delete")]
        public bool DeleteOffer([FromBody] int offerId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Offers ofr = context.Offers.FirstOrDefault(c => c.OfrId == offerId);
              

                if (ofr == null)
                {
                    return false;
                }

                ofr.OfrAuditRd = DateTime.UtcNow;
                ofr.OfrAuditRu = User.GetUserId();

                context.SaveChanges();
               
                return true;
            }
        }

        [HttpPost]
        [Route("/api/Offers/MakeRequest")]
        public EmploymentRequestDto MakeEmploymentRequest([FromBody] EmploymentRequestDto employmentRequestDto)
        {
            EmploymentRequests employmentRequest = new EmploymentRequests
            {
                EtrAuditCd = DateTime.UtcNow,
                EtrAuditCu = User.GetUserId(),
                EtrOfrId = employmentRequestDto.OfferId,
                EtrCntId = null,
                EtrEmpId = employmentRequestDto.EmployeeId,
                EtrStatus = employmentRequestDto.Status
            };

            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                context.EmploymentRequests.Add(employmentRequest);
                context.SaveChanges();
            }

            employmentRequestDto.Id = employmentRequest.EtrId;
            return employmentRequestDto;
        }

        [HttpPost]
        [Route("/api/Offers/ChangeRequestStatus")]
        public bool ChangeEmploymentRequestStatus([FromBody] EmploymentRequestDto employmentRequestDto)
        {
            try
            {
                using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
                {
                    EmploymentRequests employmentRequest =
                        context.EmploymentRequests.FirstOrDefault(e => e.EtrId == employmentRequestDto.Id);

                    if (employmentRequest == null)
                    {
                        return false;
                    }

                    employmentRequest.EtrAuditMd = DateTime.UtcNow;
                    employmentRequest.EtrAuditMu = User.GetUserId();
                    employmentRequest.EtrStatus = employmentRequestDto.Status;

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                //todo: add logging
                return false;
            }
        }

        [HttpPost]
        [Route("/api/Offers/IssueContract")]
        public ContractDto IssueContract([FromBody] ContractDto contractDto)
        {
            Contracts contract = new Contracts
            {
                CntAuditCd = DateTime.UtcNow,
                CntAuditCu = User.GetUserId(),
                CntCgtId = contractDto.ContragentId,
                CntEmpId = contractDto.EmployeeId,
                CntIssueDate = DateTime.UtcNow,
                CntStartDate = contractDto.StartDate,
                CntEndDate = contractDto.EndDate
            };

            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                context.Contracts.Add(contract);
                context.SaveChanges();

                contractDto.Id = contract.CntId;
            }

            return contractDto;
        }

    }
}