using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EuroJobsCrm.Services
{
    public class AccountEmailConfirmationService : IAccountConfirmationService
    {
        public async Task Confirm(ApplicationUser user, string callbackUrl)
        {
            IEmailMessageBuilder builder = new ConfirmAccountBodyBuilder(user, callbackUrl);
            string subject = builder.BuildSubject();
            string message = builder.BuildBody();

            IEmailSender sender = new NotificationEmailSender();
            await sender.SendEmailAsync(user.Email, subject, message);
        }
    }

    public interface IAccountConfirmationService
    {
        Task Confirm(ApplicationUser user, string callbackUrl);
    }
}