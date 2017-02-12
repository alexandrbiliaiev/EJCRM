using EuroJobsCrm.Models;
using System;

namespace EuroJobsCrm.Dto
{
    public class EventDto
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string NoteText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RemindDate { get; set; }
        public string TargetUser { get; set; }
        public int? ContragentId { get; set; }
        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? CreatingDate { get; set; }

        public EventDto()
        {

        }

        public EventDto(Notes n)
        {
            Id = n.NotId;
            Subject = n.NotSubject;
            NoteText = n.NotText;
            StartDate = n.NotStartDate;
            EndDate = n.NotEndDate;
            RemindDate = n.NotRemindDate;
            TargetUser = n.NotTargetUser;
            ContragentId = n.NotCtgId;
            ClientId = n.NotCltId;
            EmployeeId = n.NotEmp;
            CreatingDate = n.NotAuditCd;
        }
    }


}
