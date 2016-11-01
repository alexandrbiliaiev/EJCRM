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
        public ContactPersonDto SaveContactPerson([FromBody] ContactPersonDto contactPerson)
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
                ctp.CtpMessanger = contactPerson.Messanger;
                ctp.CtpName = contactPerson.Name;
                ctp.CtpPhoneNumber = contactPerson.PhoneNumber;
                ctp.CtpPosition = contactPerson.Position;
                ctp.CtpSurname = contactPerson.Surname;
                ctp.CtpSkype = contactPerson.Skype;
                ctp.CtpAuditMd = DateTime.UtcNow;
                ctp.CtpAuditMu = User.GetUserId();


                context.SaveChanges();
                contactPerson.Id = ctp.CtpId;

                return contactPerson;
            }

        }
    }
}