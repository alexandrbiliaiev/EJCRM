﻿using System;

namespace EuroJobsCrm.Dto
{
    public class ContractDto : DataTransferObjectBase
    {
        public int Id { get; set; }
        public int ContragentId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
