using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Contragents;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using EuroJobsCrm.Offers;
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

            return (ContragentDto) contragentEntity;
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
            return (ContragentDto) contragentEntity;
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

            contragentsRepository.Delete(contragentEntity);

            return true;
        }

        public void NotifyContragents([FromBody] OfferNotificationRequest request)
        {
            IRepository<Contragent> repository = new ContragentsRepository();
            IEnumerable<Contragent> contragents = request.ToAll ? 
                repository.Get() : 
                repository.Get(c => request.ContragentsIds.Contains(c.CgtId));

            IRepository<Offer> offersRepository = new OffersRepository();
            Offer offer = offersRepository.Get(request.OfferId);




        }
    }
}