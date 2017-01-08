using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EuroJobsCrm.Dto
{
    public class FileDto : DataTransferObjectBase
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
