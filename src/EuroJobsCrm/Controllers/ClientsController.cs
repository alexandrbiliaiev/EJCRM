using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public class ClientsController : Controller
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
        [Route("/api/Clients")]
        public IEnumerable<ClientDto> GetClients()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<ClientDto> clients = context.Clients
                    .Where(c => c.CltAuditRd == null)
                    .GroupJoin(context.Addresses.Where(a => a.AdrAuditRd == null), c => c.CltId, a => a.ArdCltId,
                        (c, a) => new {Client = c, Addresses = a})
                    .GroupJoin(context.ContactPersons.Where(a => a.CtpAuditRd == null), c => c.Client.CltId,
                        cp => cp.CtpCltId,
                        (c, cp) => new {c.Client, c.Addresses, ContactPersons = cp})
                    .GroupJoin(context.Offers.Where(o => o.OfrAuditRd == null), c => c.Client.CltId, o => o.OfrCltId,
                        (c, o) => new {c.Client, c.Addresses, c.ContactPersons, Offers = o})
                    .GroupJoin(context.Employees.Where(e => e.EmpAuditRd == null && e.EmpCltId != null),
                        c => c.Client.CltId, e => e.EmpCltId,
                        (c, e) => new {c.Client, c.Addresses, c.ContactPersons, c.Offers, AcceptedEmployees = e})
                    .GroupJoin(context.DocumentFiles.Where(f => f.DcfAuditRd == null),
                        c => c.Client.CltId, f => f.DcfCliId,
                        (c, f) => new
                        {
                            c.Client, c.Addresses, c.ContactPersons, c.Offers, c.AcceptedEmployees, Files = f
                        })
                    .ToList()
                    .Select(
                        c =>
                            new ClientDto(c.Client, c.Addresses, c.ContactPersons, c.Offers, c.AcceptedEmployees, c.Files))
                    .ToList();

                foreach (var client in clients)
                {
                    client.FreeVacancies = 0;
                    client.AwaitingVacancies = 0;
                    client.BusyVacancies = 0;

                    foreach (var offer in client.Offers)
                    {
                        offer.AcceptedCount = context.EmploymentRequests.Count(er => er.EtrOfrId == offer.Id && er.EtrAuditRd == null && er.EtrStatus == 1);
                        offer.AwaitingCount = context.EmploymentRequests.Count(er => er.EtrOfrId == offer.Id && er.EtrAuditRd == null && er.EtrStatus == 0);
                        client.FreeVacancies += offer.VacanciesNumber - offer.AcceptedCount;
                        client.AwaitingVacancies += offer.AwaitingCount;
                        client.BusyVacancies += offer.AcceptedCount;
                    }
                }

                return clients;
            }
        }

        [HttpPost]
        [Route("/api/Clients/Save")]
        public ClientDto SaveClient([FromBody] ClientDto client)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Clients clt;
                if (client.Id != 0)
                {
                    clt = context.Clients.FirstOrDefault(c => c.CltId == client.Id);
                }
                else
                {
                    clt = new Clients
                    {
                        CltAuditCd = DateTime.UtcNow,
                        CltAuditCu = User.GetUserId()
                    };
                    context.Clients.Add(clt);
                }

                if (clt == null)
                {
                    return null;
                }

                clt.CltKrs = client.Krs;
                clt.CltName = client.Name;
                clt.CltNip = client.Nip;
                clt.CltRegon = client.Regon;
                clt.CltBranch = client.Branch;
                clt.CltStatus = client.Status;
                clt.CltType = client.Type;
                clt.CltAuditMd = DateTime.UtcNow;
                clt.CltAuditMu = User.GetUserId();
 
                context.SaveChanges();
                client.Id = clt.CltId;
                return client;
            }
        }

        [HttpPost]
        [Route("/api/Clients/Delete")]
        public bool DeleteClient([FromBody] int clientId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Clients clt = context.Clients.FirstOrDefault(c => c.CltId == clientId);
              

                if (clt == null)
                {
                    return false;
                }

                clt.CltAuditRd = DateTime.UtcNow;
                clt.CltAuditRu = User.GetUserId();

                context.SaveChanges();
               
                return true;
            }
        }


    }
}