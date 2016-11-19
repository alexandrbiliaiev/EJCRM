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
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<ContragentDto> contagents = context.Contragents
                    .Where(c => c.CgtAuditRd == null)
                    .GroupJoin(context.Addresses.Where(a => a.AdrAuditRd == null), c => c.CgtId, a => a.AdrCgtId,
                        (c, a) => new {Contragent = c, Addresses = a})
                    .GroupJoin(context.ContactPersons.Where(a => a.CtpAuditRd == null), c => c.Contragent.CgtId, cp => cp.CtpCgtId,
                        (c, cp) => new {c.Contragent, c.Addresses, ContactPersons = cp})
                    .GroupJoin(context.Employees.Where(a => a.EmpAuditRd == null), c => c.Contragent.CgtId, cp => cp.EmpCtgId,
                        (c, em) => new { c.Contragent, c.Addresses, c.ContactPersons, Employees = em })
                    .ToList()
                    .Select(c => new ContragentDto(c.Contragent, c.Addresses, c.ContactPersons, c.Employees))
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
        [Route("/api/Contragents/Delete")]
        public bool DeleteContragent([FromBody] int contragentId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Contragents ctr = context.Contragents.FirstOrDefault(c => c.CgtId == contragentId);
              

                if (ctr == null)
                {
                    return false;
                }
                
                ctr.CgtAuditRd = DateTime.UtcNow;
                ctr.CgtAuditRu = User.GetUserId();

                context.SaveChanges();
               
                return true;
            }
        }
    }
}