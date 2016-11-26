using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class EmploymentRequestDto
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int EmployeeId { get; set; }

        /// <summary>
        /// 0 - pending
        /// 1 - accepted
        /// 2 - rejected
        /// 3 - finished
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
        }
    }
}
