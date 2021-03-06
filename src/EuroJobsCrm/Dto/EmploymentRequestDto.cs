﻿using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class EmploymentRequestDto : DataTransferObjectBase
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int EmployeeId { get; set; }
        public int? ClientId { get; set; }

        /// <summary>
        /// 0 - pending
        /// 1 - accepted
        /// 2 - rejected
        /// 3 - finished good (candidate returns to contragent)
        /// 4 - finished not good (change candidate status to "Run" and block futher adding to offers)
        /// </summary>
        public int Status { get; set; }
        public int? ContractId { get; set; }

        public EmploymentRequestDto()
        {
            
        }

        public EmploymentRequestDto(EmploymentRequests employmentRequest)
        {
            Id = employmentRequest.EtrId;
            ContractId = employmentRequest.EtrCntId;
            EmployeeId = employmentRequest.EtrEmpId;
            OfferId = employmentRequest.EtrOfrId;
            Status = employmentRequest.EtrStatus;
            ClientId = employmentRequest.EtrCltId;
        }
    }
}
