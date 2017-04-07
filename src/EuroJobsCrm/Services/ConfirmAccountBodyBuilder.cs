using System.IO;
using EuroJobsCrm.Models;
using EuroJobsCrm.Utils;

namespace EuroJobsCrm.Services
{
    public class ConfirmAccountBodyBuilder : IEmailMessageBuilder
    {
        private readonly ApplicationUser _user;
        private readonly string _callbackUrl;
     
        public ConfirmAccountBodyBuilder(ApplicationUser user, string callbackUrl)
        {
            _user = user;
            _callbackUrl = callbackUrl;
        }
        public string BuildBody()
        {
            string environmentPath = EnvironmentUtil.Environment.WebRootPath;

            string emailHtmlTemplate = File.ReadAllText(environmentPath + "\\emailTemplates\\confirmAccountEmailTemplate.html");

            string body = emailHtmlTemplate.Replace("[Header]", "Account confirmation")
                .Replace("[MAINTEXT]", $@"Hi <b>{_user.FullName}</b>, <br/> Please confirm your account in BWG system.")
                .Replace("[href]", _callbackUrl)
                .Replace("[Confirmation]", "Confirm")
                .Replace("[CONTENT]", "If you did not register on the BWG system just ignore the message.");
            return body;
        }

        public string BuildSubject()
        {
            return @"BWG Account confirmation";
        }
    }
}