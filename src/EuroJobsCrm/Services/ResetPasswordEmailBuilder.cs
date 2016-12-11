using System;
using System.IO;
using EuroJobsCrm.Models;
using EuroJobsCrm.Utils;

namespace EuroJobsCrm.Services
{
    public class ResetPasswordEmailBuilder : IEmailMessageBuilder
    {
        private readonly string _ip;
        private readonly string _newPassword;

        public ResetPasswordEmailBuilder(string ip, string newPassword)
        {
            _ip = ip;
            _newPassword = newPassword;
        }

        public string BuildBody()
        {
            string environmentPath = EnvironmentUtil.Environment.WebRootPath;
            
            string emailHtmlTemplate = File.ReadAllText(environmentPath + "\\emailTemplates\\resetPasswordTemplate.html");

            string body = emailHtmlTemplate.Replace("[Header]", "Changing of credentials")
                .Replace("[DETAILLINE]", "Your password has been reset")
                .Replace("[CONTENT]", $"<br/><br/>Eurojobs administrator has reset your password. You can login with new password: <b>{_newPassword}</b>")
                .Replace("[INFO_STRING]", $"IP address of admin: {_ip} at {DateTime.Now}");
            
                return body;

        }

        public string BuildSubject()
        {
            return "Your password has been reset";
        }
    }
}