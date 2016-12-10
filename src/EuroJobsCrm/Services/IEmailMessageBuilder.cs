using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroJobsCrm.Services
{
    interface IEmailMessageBuilder
    {
        string BuildBody();
        string BuildSubject();
    }
}
