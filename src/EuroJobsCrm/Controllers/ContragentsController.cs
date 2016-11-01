using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public class ContragentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/Contragents")]
        public IEnumerable<ContragentDto> GetContragents()
        {
            using (var context = new DB_A12601_bielkaContext())
            {
                List<ContragentDto> contagents = context.Contragents
                    .Where(c => c.CgtAuditRd == null)
                    .GroupJoin(context.Addresses, c => c.CgtId, a => a.AdrCgtId,
                        (c, a) => new {Contragent = c, Addresses = a})
                    .GroupJoin(context.ContactPersons, c => c.Contragent.CgtId, cp => cp.CtpCgtId,
                        (c, cp) => new {c.Contragent, c.Addresses, ContactPersons = cp})
                    .ToList()
                    .Select(c => new ContragentDto(c.Contragent, c.Addresses, c.ContactPersons))
                    .ToList();

               return contagents;
            }
        }

        [HttpPost]
        [Route("/api/Contragents/Save")]
        public ContragentDto SaveContragent([FromBody] ContragentDto contragent)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Contragents ctr;
                if (contragent.Id != 0)
                {
                    ctr = context.Contragents.FirstOrDefault(c => c.CgtId == contragent.Id);
                }
                else
                {
                    ctr = new Contragents
                    {
                        CgtAuditCd = DateTime.UtcNow,
                        CgtAuditCu = User.GetUserId()
                    };
                    context.Contragents.Add(ctr);
                }

                if (ctr == null)
                {
                    return null;
                }

                ctr.CgtName = contragent.Name;
                ctr.CgtLicenseNumber = contragent.LicenseNumber;
                ctr.CgtStatus = contragent.Status;
                ctr.CgtAuditMd = DateTime.UtcNow;
                ctr.CgtAuditMu = User.GetUserId();
 
                context.SaveChanges();
                contragent.Id = ctr.CgtId;
                return contragent;
            }
        }

       
        [HttpPost]
        [Route("/api/ContactPersons/Save")]
        public AddressDto SaveContactPerson([FromBody] AddressDto address)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {

                Addresses adr;
                if (address.Id != 0)
                {
                    adr = context.Addresses.FirstOrDefault(c => c.AdrId == address.Id);
                }
                else
                {
                    adr = new Addresses
                    {
                        AdrAuditCd = DateTime.UtcNow,
                        AdrAuditCu = User.GetUserId()
                    };
                    context.Addresses.Add(adr);
                }

                if (adr == null)
                {
                    return null;
                }

                adr.AdrCity = address.City;
                adr.AdrCountry = address.Country;
                adr.AdrPay = address.Pay;
                adr.AdrPostCode = address.PostCode;
                adr.AdrType = address.Type;
                adr.AdrCgtId = address.ContragentId;
                adr.AdrAddress = address.Address;
                adr.AdrAuditCd = DateTime.UtcNow;
                adr.AdrAuditCu = User.GetUserId();

                context.SaveChanges();
                address.Id = adr.AdrId;

                return address;
            }

        }
    }
}