using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Addresses")]
    public class AddressesController : Controller
    {
        [HttpPost]
        [Route("api/Addresses/Save")]
        public AddressDto SaveAddress([FromBody] AddressDto address)
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

        [HttpPost]
        [Route("api/Addresses/Delete")]
        public bool DeleteAddress([FromBody] int addressId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Addresses adr = context.Addresses.FirstOrDefault(c => c.AdrId == addressId);

                if (adr == null)
                {
                    return false;
                }

                adr.AdrAuditRd = DateTime.UtcNow;
                adr.AdrAduitRu = User.GetUserId();
                context.SaveChanges();

                return true;
            }
        }

    }
}