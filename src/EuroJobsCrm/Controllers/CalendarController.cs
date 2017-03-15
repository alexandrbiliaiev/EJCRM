using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public class CalendarController : Controller
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

        [HttpGet("api/Calendar/Events")]
        public List<EventDto> GetEvents()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var eventsEntities = context.Notes.Where(n => n.NotAuditRd == null).ToList();
                return eventsEntities.Select(e => new EventDto(e)).ToList();
            }
        }

        [HttpPost]
        [Route("api/Calendar/Events")]
        public EventDetailsDto GetEvent([FromBody] EventDto eventdto)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var eventsEntity = context.Notes.Where(n => n.NotId == eventdto.Id && n.NotAuditRd == null)
                                                  .LeftJoin(context.AspNetUsers, n => n.NotTargetUser, u => u.Id,
                                                            (n, u) => new { Note = n, User = u })
                                                  .LeftJoin(context.Contragents, n => n.Note.NotCtgId, c => c.CgtId,
                                                            (n, c) => new { n.Note, n.User, Contragent = c })
                                                  .LeftJoin(context.Clients, n => n.Note.NotCltId, c => c.CltId,
                                                            (n, c) => new { n.Note, n.User, n.Contragent, Client = c })
                                                  .LeftJoin(context.Employees, n => n.Note.NotEmp, e => e.EmpId,
                                                            (n, e) => new { n.Note, n.User, n.Contragent, n.Client, Employee = e })
                                                  .FirstOrDefault();

                return new EventDetailsDto(eventsEntity.Note)
                {
                    TargetUserName = eventsEntity.User?.UserName,
                    ClientName = eventsEntity.Client?.CltName,
                    ContragentName = eventsEntity.Contragent?.CgtName,
                    EmployeeName = eventsEntity.Employee?.EmpFirstName + " " + eventsEntity.Employee?.EmpLastName
                };

            }

        }

        [HttpPost]
        [Route("api/Calendar/Events/Save")]
        public EventDetailsDto SaveEvent([FromBody] EventDto eventdto)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Notes eventEntity = context.Notes.FirstOrDefault(n => n.NotId == eventdto.Id);
                if (eventEntity == null)
                {
                    eventEntity = new Notes()
                    {
                        NotAuditCd = DateTime.Now,
                        NotAuditCu = User.GetUserId(),
                        NotId = eventdto.Id
                    };

                    context.Notes.Add(eventEntity);
                }

                eventEntity.NotAuditMd = DateTime.Now;
                eventEntity.NotAuditMu = User.GetUserId();

                eventEntity.NotCltId = eventdto.ClientId;
                eventEntity.NotCtgId = eventdto.ContragentId;
                eventEntity.NotEmp = eventdto.EmployeeId;
                eventEntity.NotEndDate = eventdto.EndDate;
                eventEntity.NotRemindDate = eventdto.RemindDate;
                eventEntity.NotStartDate = eventdto.StartDate;
                eventEntity.NotSubject = eventdto.Subject;
                eventEntity.NotTargetUser = eventdto.TargetUser;
                eventEntity.NotText = eventdto.NoteText;

                context.SaveChanges();
            }

            return GetEvent(eventdto);
        }


        [HttpGet]
        [Route("api/Calendar/Events/GetLatestEvents")]
        public List<EventDto> GetMyEvents()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                string userId = User.GetUserId();

                DateTime from = DateTime.Today;
                DateTime to = DateTime.Today.AddDays(1);

                var latestEvents = context.Notes.Where(n => n.NotAuditRd == null && (n.NotAuditCu == userId || n.NotTargetUser == userId) &&
                                                            (n.NotRemindDate >= from && n.NotRemindDate <= to ||
                                                            n.NotStartDate >= from && n.NotStartDate <= to)).ToList();

                return latestEvents.Select(e => new EventDto(e)).ToList();
            }
        }




        [HttpPost]
        [Route("api/Calendar/Events/Delete")]
        public bool DeleteEvent([FromBody] EventDto eventdto)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Notes eventEntity = context.Notes.FirstOrDefault(n => n.NotId == eventdto.Id);
                if (eventEntity == null)
                {
                    return false;
                }

                eventEntity.NotAuditRd = DateTime.Now;
                eventEntity.NotAuditRu = User.GetUserId();

                context.SaveChanges();
                return true;
            }
        }
    }
}
