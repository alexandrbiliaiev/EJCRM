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
                        (c, a) => new { Contragent = c, Addresses = a })
                    .GroupJoin(context.ContactPersons.Where(a => a.CtpAuditRd == null), c => c.Contragent.CgtId, cp => cp.CtpCgtId,
                        (c, cp) => new { c.Contragent, c.Addresses, ContactPersons = cp })
                    .GroupJoin(context.Employees.Where(a => a.EmpAuditRd == null), c => c.Contragent.CgtId, cp => cp.EmpCtgId,
                        (c, em) => new { c.Contragent, c.Addresses, c.ContactPersons, Employees = em })
                    .GroupJoin(context.DocumentFiles.Where(f => f.DcfAuditRu == null && f.DcfCntId != null), c => c.Contragent.CgtId, f => f.DcfCntId,
                        (c, f) => new { c.Contragent, c.Addresses, c.ContactPersons, c.Employees, Files = f })
                    .LeftJoin(context.AspNetUsers, c => c.Contragent.CgtResponsibleUser, u => u.Id,
                        (c, u) => new { c.Contragent, c.Addresses, c.ContactPersons, c.Employees, c.Files, ResponsibleUser = u })
                    .GroupJoin(context.AspNetUsers.Join(context.UsersToContragents, c => c.Id, u => u.UtcUsrId, (c, u) => new { User = c, ContragentUser = u })
                    , c => c.Contragent.CgtId, u => u.ContragentUser.UtcCtgId,
                        (c, u) => new
                        {
                            c.Contragent,
                            c.Addresses,
                            c.ContactPersons,
                            c.Employees,
                            c.Files,
                            c.ResponsibleUser,
                            ContragentUsers = u.Select(e => new
                            {
                                Id = e.User.Id,
                                UserName = e.User.UserName,
                                Email = e.User.Email,
                                CtgId = e.ContragentUser.UtcCtgId,
                                Name = e.ContragentUser.UtcUsrName
                            })
                        })
                    .ToList()
                    .Select(c => new ContragentDto(c.Contragent, c.Addresses, c.ContactPersons, c.Employees, c.Files, c.ResponsibleUser, c.ContragentUsers.Select(e => new UserDto
                    {
                        Id = e.Id,
                        CtgId = e.CtgId,
                        Email = e.Email,
                        Name = e.Name,
                        UserName = e.UserName,
                        UserRole = null
                    }).ToList()))
                    .ToList();

               return contagents;
            }
        }

        [HttpGet]
        [Route("/api/Contragents/Lite")]
        public IEnumerable<ContragentDto> GetContragentsLite()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<ContragentDto> contagents = context.Contragents
                    .Where(c => c.CgtAuditRd == null)       
                    .ToList()
                    .Select(c => new ContragentDto(c))
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
        [Route("/api/Contragents/addResponsiblePersonToContragent")]
        public async Task<ContragentDto> AddResponsiblePersonToContragent([FromBody] ResponsiblePersonToContragentParamDto param)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var contragnet = context.Contragents.FirstOrDefault(c => c.CgtId == param.ContragentId);
                if (contragnet == null)
                {
                    return new ContragentDto
                    {
                        Success = false,
                        ErrorMessage = "Customer doesn't exist"
                    };
                }

                contragnet.CgtResponsibleUser = param.UserId;
                await context.SaveChangesAsync();
                return new ContragentDto(contragnet);
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