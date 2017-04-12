using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EuroJobsCrm.Contragents;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using EuroJobsCrm.Offers;
using EuroJobsCrm.Services;
using EuroJobsCrm.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public partial class ContragentsController : Controller
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
        [Route("/api/Contragents")]
        public IEnumerable<ContragentDto> GetContragents()
        {
            IRepository<ContragentDto> repository = new EntireContragetsDataRepository();
            var contragents = repository.Get();
            return contragents;
        }

        [HttpGet]
        [Route("/api/Contragents/Lite")]
        public IEnumerable<ContragentDto> GetContragentsLite()
        {
            IRepository<Contragent> contragentsRepository = new ContragentsRepository();
            var contragents = contragentsRepository.Get().Select(c => (ContragentDto)c).ToList();
            return contragents;
        }

        [HttpPost]
        [Route("/api/Contragents/Save")]
        public ContragentDto SaveContragent([FromBody] ContragentDto contragent)
        {
            IRepository<Contragent> contragentsRepository = new ContragentsRepository();
            Contragent contragentEntity = contragentsRepository.Get(contragent.Id) ?? new Contragent
            {
                CgtAuditCu = User.GetUserId()
            };

            contragentEntity.CgtName = contragent.Name;
            contragentEntity.CgtLicenseNumber = contragent.LicenseNumber;
            contragentEntity.CgtStatus = contragent.Status;
            contragentEntity.CgtAuditMu = User.GetUserId();

            contragentsRepository.Save(contragentEntity);

            return (ContragentDto)contragentEntity;
        }

        [HttpPost]
        [Route("/api/Contragents/addResponsiblePersonToContragent")]
        public ContragentDto AddResponsiblePersonToContragent([FromBody] ResponsiblePersonToContragentParamDto param)
        {
            IRepository<Contragent> contragentsRepository = new ContragentsRepository();
            Contragent contragentEntity = contragentsRepository.Get(param.ContragentId);
            if (contragentEntity == null)
            {
                return new ContragentDto
                {
                    Success = false,
                    ErrorMessage = "Customer doesn't exist"
                };
            }
            contragentEntity.CgtResponsibleUser = param.UserId;
            contragentsRepository.Save(contragentEntity);
            return (ContragentDto)contragentEntity;
        }

        [HttpPost]
        [Route("/api/Contragents/Delete")]
        public bool DeleteContragent([FromBody] int contragentId)
        {
            IRepository<Contragent> contragentsRepository = new ContragentsRepository();
            Contragent contragentEntity = contragentsRepository.Get(contragentId);

            if (contragentEntity == null)
            {
                return false;
            }

            contragentEntity.CgtAuditRu = User.GetUserId();
            contragentsRepository.Delete(contragentEntity);

            return true;
        }

        [HttpPost]
        [Route("/api/Contragents/Notify")]
        public async void NotifyContragents([FromBody] OfferNotificationRequest request)
        {
            IRepository<AspNetUsers> repository = new ContragentUsersRepository();
            IEnumerable<AspNetUsers> contragentUsers = request.ToAll ?
                repository.Get() :
                repository.Get(c => request.ContragentsIds.Contains(c.ContragentId ?? 0));

            IRepository<Offer> offersRepository = new OffersRepository();
            Offer offer = offersRepository.Get(request.OfferId);

            string offerUrl = $"{Request.Scheme}://{Request.Host}/Offers#/off_edit/{request.OfferId}";

            IEmailMessageBuilder messageBuilder = new OfferNotifyMessageBuilder(offer, offerUrl);
            string subject = messageBuilder.BuildSubject();
            string message = messageBuilder.BuildBody();

            IEmailSender sender = new NotificationEmailSender();
            foreach (AspNetUsers user in contragentUsers)
            {
                await sender.SendEmailAsync(user.Email, subject, message);
            }
        }
    }

    public class OfferNotifyMessageBuilder : IEmailMessageBuilder
    {
        private readonly Offer _offer;
        private readonly string _url;
        public OfferNotifyMessageBuilder(Offer offer, string url)
        {
            _offer = offer;
            _url = url;
        }

        public string BuildBody()
        {
            string environmentPath = EnvironmentUtil.Environment.WebRootPath;

            string emailHtmlTemplate = File.ReadAllText(environmentPath + "\\emailTemplates\\offerNotifyEmailTemplate.html");

            string body = emailHtmlTemplate.Replace("[Header]", $"The {_offer.OfrPosition} job offer")
                .Replace("[MAINTEXT]", $@"Hi, <br/> A new job offer was published on the BWG.")
                .Replace("[href]", _url)
                .Replace("[Confirmation]", "Look at the offer")
                .Replace("[CONTENT]", "Click on \"Look at the offer\" button to open the offer");
            return body;
        }

        public string BuildSubject()
        {
            return $"The \"{_offer.OfrPosition}\" job offer was published!";
        }
    }
}