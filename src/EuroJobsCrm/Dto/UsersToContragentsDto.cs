using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class UsersToContragentsDto : DataTransferObjectBase
    {

        public int Id { get; set; }
        public string UsrId { get; set; }
        public string UsrName { get; set; }
        public int? CtgId { get; set; }
        public string PrefLng { get; set; }

        public UsersToContragentsDto()
        {

        }
        public UsersToContragentsDto(UsersToContragents ustoctg)
        {
            Id = ustoctg.UtcId;
            UsrId = ustoctg.UtcUsrId;
            UsrName = ustoctg.UtcUsrName;
            CtgId = ustoctg.UtcCtgId;
            PrefLng = ustoctg.UtcLng;
        }

    }
}