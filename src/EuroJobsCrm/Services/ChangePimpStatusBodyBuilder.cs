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
    public class ChangePimpStatusBodyBuilder : IEmailMessageBuilder
    {
        private EmployeeDto _employee;
        private ContragentDto _contragent;
        private OfferDto _offer;
        private string _status;

        public ChangePimpStatusBodyBuilder(EmployeeDto employee, ContragentDto contragent, OfferDto offer, string status)
        {
            _employee = employee;
            _contragent = contragent;
            _offer = offer;
            _status = status;

        }
        public string BuildBody()
        {
            string environmentPath = EnvironmentUtil.Environment.WebRootPath;

            string emailHtmlTemplate = File.ReadAllText(environmentPath + "\\emailTemplates\\candidateSubmitionEmailTemplate.html");

            string body = emailHtmlTemplate.Replace("[Header]", "Candidate request Update")
                .Replace("[Detail line 1]",$"Applies: {_contragent.Name}")
                .Replace("[DETAILLINE2]",$"Candidate: {_employee.FirstName} {_employee.LastName}")
                .Replace("[MAINTEXT]",$@"Dear colegue, <br/> Candidate was {_status}<br/> Offer: {_offer.Position}")
                .Replace("[href]", $"http://localhost:52962/Offers#/off_edit/{_offer.Id}")
                .Replace("[Confirmation]", "Go to Offer")
                .Replace("[CONTENT]", "Please note, contragent will received email with your decision.");
            return body;
        }

        public string BuildSubject()
        {
            string subject = 
$@"Candidate from {_contragent.Name} was {_status} for {_offer.Position}";
            return subject;
        }
    }
}
