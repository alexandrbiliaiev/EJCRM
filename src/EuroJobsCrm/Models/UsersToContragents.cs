using System;
using System.Collections.Generic;

namespace EuroJobsCrm.Models
{
    public partial class UsersToContragents
    {
        public int UtcId { get; set; }
        public string UtcUsrId { get; set; }
        public string UtcUsrName { get; set; }
        public int? UtcCtgId { get; set; }
    }
}
