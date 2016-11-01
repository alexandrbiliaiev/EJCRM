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
    [Produces("application/json")]
    [Route("api/ContactPersons")]
    public class ContactPersonsController : Controller
    {
        [HttpPost]
        [Route("/Save")]
        public AddressDto SaveContactPerson([FromBody] ContactPersonDto contactPerson)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {

                ContactPersons ctp;
                if (contactPerson.Id != 0)
                {
                    ctp = context.ContactPersons.FirstOrDefault(c => c.CtpId == contactPerson.Id);
                }
                else
                {
                    ctp = new ContactPersons
                    {
                        CtpAuditCd = DateTime.UtcNow,
                        CtpAuditCu = User.GetUserId()
                    };
                    context.ContactPersons.Add(ctp);
                }

                if (ctp == null)
                {
                    return null;
                }


                ctp.CtpCgtId = contactPerson.ContragentId;
                ctp.CtpEmail = contactPerson.Email;
                //ctp.AdrCity = address.City;
                //ctp.AdrCountry = address.Country;
                //ctp.AdrPay = address.Pay;
                //ctp.AdrPostCode = address.PostCode;
                //ctp.AdrType = address.Type;
                //ctp.AdrCgtId = address.ContragentId;
                //ctp.AdrAddress = address.Address;
                //ctp.AdrAuditCd = DateTime.UtcNow;
                //ctp.AdrAuditCu = User.GetUserId();

                //context.SaveChanges();
                //address.Id = adr.AdrId;

                return null;
            }

        }
    }
}