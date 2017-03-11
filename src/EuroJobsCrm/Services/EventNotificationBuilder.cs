using System;
using System.IO;
using EuroJobsCrm.Models;
using EuroJobsCrm.Utils;

namespace EuroJobsCrm.Services
{
    internal class EventNotificationBuilder : IEmailMessageBuilder
    {
        private readonly Notes _event;
        public EventNotificationBuilder(Notes @event)
        {
            _event = @event;
        }

        public string BuildBody()
        {
            string environmentPath = EnvironmentUtil.Environment.WebRootPath;

            string emailHtmlTemplate = File.ReadAllText(environmentPath + "\\emailTemplates\\eventNotificationTemplate.html");

            string body = emailHtmlTemplate.Replace("[Header]", "Event notification")
                .Replace("[DETAILLINE]", _event.NotSubject)
                .Replace("[CONTENT]", _event.NotText)
                .Replace("[INFO_STRING]", string.Empty);

            return body;
        }

        public string BuildSubject()
        {
            return _event.NotSubject;
        }
    }
}