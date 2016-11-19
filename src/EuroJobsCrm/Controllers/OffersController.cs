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
        public IEnumerable<OfferDto> GetOffers()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<OfferDto> offers = context.Offers.Where(o => o.OfrAuditRd == null)
                                                      .Select(o => new OfferDto(o))
                                                      .ToList();

                return offers;
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
                    ofr = new Offers()
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


                ofr.OfrAuditMd = DateTime.UtcNow;
                ofr.OfrAuditMu = User.GetUserId();

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
    }
}