using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using EuroJobsCrm.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EuroJobsCrm.Services
{
    public class PimpEmployeeBodyBuilder : IEmailMessageBuilder
    {
        private EmployeeDto _employee;
        private ContragentDto _contragent;
        private OfferDto _offer;

        public PimpEmployeeBodyBuilder(EmployeeDto employee, ContragentDto contragent, OfferDto offer)
        {
            _employee = employee;
            _contragent = contragent;
            _offer = offer;

        }
        public string BuildBody()
        {
            string environmentPath = EnvironmentUtil.Environment.WebRootPath;

            string emailHtmlTemplate = File.ReadAllText(environmentPath + "\\emailTemplates\\candidateSubmitionEmailTemplate.html");

            string body = emailHtmlTemplate.Replace("[Header]", "New candidate request")
                .Replace("[Detail line 1]",$"Applies: {_contragent.Name}")
                .Replace("[DETAILLINE2]",$"Candidate: {_employee.FirstName} {_employee.LastName}")
                .Replace("[MAINTEXT]",$@"Dear colegue, <br/> Please confirm your choise in Eurojobs CRM. <br/> Offer: {_offer.Position}")
                .Replace("[href]", $"http://localhost:52962/Offers#/off_edit/{_offer.Id}")
                .Replace("[Confirmation]", "Confirm")
                .Replace("[CONTENT]", "Please note, contragent will receive email with your decision.");




            return body;
        }

        public string BuildSubject()
        {
            string subject = 
$@"{_contragent.Name} added new candidate to offer {_offer.Position}";
            return subject;
        }
    }
}
