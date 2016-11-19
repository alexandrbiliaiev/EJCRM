using System;

namespace EuroJobsCrm.Dto
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
