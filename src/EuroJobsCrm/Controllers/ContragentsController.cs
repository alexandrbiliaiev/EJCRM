using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<ContragentsDto> GetContragents()
        {
            using (var context = new DB_A12601_bielkaContext())
            {
                var contagents = context.Contragents
                    .Where(c => c.CgtAuditRd == null)
                    .ToList();
                return contagents.Select(c => new ContragentsDto(c));
            }
        }

        [HttpPost]
        [Route("/api/Contragents/Add")]
        public ContragentsDto AddContragent([FromBody] ContragentsDto newContragent)
        {
            using (var context = new DB_A12601_bielkaContext())
            {
                Contragents ctr = new Contragents
                {
                    CgtName = newContragent.Name,
                    CgtLicenseNumber = newContragent.LicenseNumber,
                    CgtStatus = newContragent.Status
                };

                context.Contragents.Add(ctr);
                context.SaveChanges();
                return new ContragentsDto(ctr);
            }
        }



    }
}