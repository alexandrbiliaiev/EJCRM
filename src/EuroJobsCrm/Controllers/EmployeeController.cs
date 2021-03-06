using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace EuroJobsCrm.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public  EmployeeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
       
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/Employees/GetAll")]
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var user =  _userManager.FindByIdAsync(User.GetUserId());
           
            var roles = _userManager.GetRolesAsync(user.Result);
            var ffdsfd = roles.Result;

            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
  

                var employees = context.Employees.Where(e=>e.EmpAuditRd == null)
                    .GroupJoin(context.IdentityDocuments.Where(d=>d.IdcAuditRd == null), e => e.EmpId, d => d.IdcEmpId,
                        (e, d) => new { employee = e, documents = d })
                  
                    .ToList()
                    .Select(e => new EmployeeDto(e.employee, e.documents, new List<DocumentFiles>())
                    {
                        CreateDate = e.employee.EmpAuditCd ?? DateTime.Now
                    })
                    .ToList();

                var employeesIds = employees.Select(e => e.Id).ToArray();
                var emploeesPositions = context.EmploymentRequests.Where(
                        r => r.EtrAuditRd == null && r.EtrStatus == 1 && employeesIds.Contains(r.EtrEmpId))
                    .LeftJoin(context.Offers, r => r.EtrOfrId, o => o.OfrId, (r, o) => new {r.EtrEmpId, o.OfrPosition})
                    .ToLookup(e => e.EtrEmpId, e => e.OfrPosition);

                foreach (var employee in employees)
                {
                    employee.Position = emploeesPositions.FirstOrDefault(p => p.Key == employee.Id)?.FirstOrDefault();
                }

                return employees;
            }
        }

        [HttpGet]
        [Route("api/Employees/GetAll/Lite")]
        public IEnumerable<EmployeeDto> GetAllEmployeesLite()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var employees = context.Employees.Where(e => e.EmpAuditRd == null)
                    .ToList()
                    .Select(e => new EmployeeDto(e))
                    .ToList();

                return employees;
            }
        }


        [HttpGet]
        [Route("api/Employees/GetAllFree")]
        public IEnumerable<EmployeeDto> GetAllFreeEmployees()
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var empReq = context.EmploymentRequests.Where(r => r.EtrAuditRd == null).ToList();


                var employees = context.Employees.Where(e => e.EmpAuditRd == null)
                    .GroupJoin(context.IdentityDocuments.Where(d => d.IdcAuditRd == null), e => e.EmpId, d => d.IdcEmpId,
                        (e, d) => new { employee = e, documents = d })
                    .ToList()
                    .Select(e => new EmployeeDto(e.employee, e.documents, new List<DocumentFiles>()))
                    .ToList();

                foreach (EmploymentRequests e in empReq)
                {
                    employees.RemoveAll(x => x.Id == e.EtrEmpId);
                }

                return employees;
            }
        }

        [HttpPost]
        [Route("api/Employees/GetByCtgForReq")]
        public IEnumerable<EmployeeDto> GetEmployeesByCtgForReq([FromBody] string ctgId)
        {

            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var empReq = context.EmploymentRequests.Where(r => r.EtrAuditRd == null).ToList();
                       

                var employees = context.Employees.Where(e => e.EmpAuditRd == null && e.EmpCtgId == Int32.Parse(ctgId))
                    .GroupJoin(context.IdentityDocuments.Where(d => d.IdcAuditRd == null), e => e.EmpId, d => d.IdcEmpId,
                        (e, d) => new { employee = e, documents = d })
                    .ToList()
                    .Select(e => new EmployeeDto(e.employee, e.documents, new List<DocumentFiles>()))
                    .ToList();

                foreach (EmploymentRequests e in empReq)
                {
                    employees.RemoveAll(x => x.Id == e.EtrEmpId);
                }

                return employees;
            }
        }

        [HttpPost]
        [Route("api/Employees/GetByCtg")]
        public IEnumerable<EmployeeDto> GetEmployeesByCtg([FromBody] string ctgId)
        {
            var user = _userManager.FindByIdAsync(User.GetUserId());

            var roles = _userManager.GetRolesAsync(user.Result);
            var ffdsfd = roles.Result;

            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                 var employees = context.Employees.Where(e => e.EmpAuditRd == null && e.EmpCtgId == Int32.Parse(ctgId))
                    .GroupJoin(context.IdentityDocuments.Where(d => d.IdcAuditRd == null), e => e.EmpId, d => d.IdcEmpId,
                        (e, d) => new { employee = e, documents = d })
                    .ToList()
                    .Select(e => new EmployeeDto(e.employee, e.documents, new List<DocumentFiles>()))
                    .ToList();

                return employees;
            }
        }

        [HttpPost]
        [Route("api/Employees/Get")]
        public EmployeeDto GetEmployee([FromBody] int employeeId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                var employeeData = context.Employees.Where(c => c.EmpId == employeeId)
                    .GroupJoin(context.IdentityDocuments.Where(d => d.IdcAuditRd == null), e => e.EmpId, d => d.IdcEmpId,
                        (e, d) => new { employee = e, documents = d })
                    .ToList();

                IEnumerable<int?> documentsIds = employeeData.SelectMany(e => e.documents.Select(d => (int?)d.IdcId)).ToList();

                List<DocumentFiles> files = context.DocumentFiles
                                                          .Where(d => documentsIds.Contains(d.DcfIdcId) && d.DcfAuditRd == null)
                                                          .ToList();
                
                var notes =  context.Notes.Where(n => n.NotAuditRd == null && n.NotEmp == employeeId).LeftJoin(context.AspNetUsers,
                    n => n.NotAuditCu, u => u.Id, (n, u) => new
                    {
                        Note = n,
                        UserData = u
                    }).ToList();

                EmployeeDto emp = employeeData
                    .Select(e => new EmployeeDto(e.employee, e.documents, files)).FirstOrDefault();

                if (emp != null)
                {
                    emp.Notes = notes.Select(n => new EventDetailsDto(n.Note)
                    {
                        TargetUser = n.UserData.Id,
                        TargetUserName = n.UserData.FullName,
                    }).ToList();
                }

                return emp;
            }
        }



        [HttpPost]
        [Route("api/Employees/Save")]
        public EmployeeDto SaveEmployee([FromBody] EmployeeDto employee)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {

                Employees emp;
                if (employee.Id != 0)
                {
                    emp = context.Employees.FirstOrDefault(c => c.EmpId == employee.Id);
                }
                else
                {
                    emp = new Employees
                    {
                        EmpAuditCd = DateTime.UtcNow,
                        EmpAuditCu = User.GetUserId()
                    };
                    context.Employees.Add(emp);
                }

                if (emp == null)
                {
                    return null;
                }

                emp.EmpBirthDate = employee.BirthDate.Date.AddDays(1);
                emp.EmpCtgId = employee.ContragentId;
                emp.EmpDescription = employee.Description;
                emp.EmpFirstName = employee.FirstName;
                emp.EmpLastName = employee.LastName ?? string.Empty;
                emp.EmpMiddleName = employee.MiddleName ;
                emp.EmpResponsibleUser = employee.ResponsibleUser;
                emp.EmpAuditMu = User.GetUserId();
                emp.EmpAuditMd = DateTime.UtcNow;
                emp.EmpStatus = employee.Status;

                context.SaveChanges();
                employee.Id = emp.EmpId;

                return employee;
            }

        }

        [HttpPost]
        [Route("api/Employees/Delete")]
        public bool DeleteEmployee([FromBody] int employeeId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                Employees emp = context.Employees.FirstOrDefault(c => c.EmpId == employeeId);

                if (emp == null)
                {
                    return false;
                }

                emp.EmpAuditRd = DateTime.UtcNow;
                emp.EmpAuditRu = User.GetUserId();
                context.SaveChanges();

                return true;
            }
        }

        [HttpPost]
        [Route("api/Employees/GetDocuments")]
        public List<IdentityDocumentsDto> GetIdentityDocuments([FromBody] int employeeId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                List<IdentityDocumentsDto> documents =
                    context.IdentityDocuments
                        .Where(i => i.IdcAuditRd == null && i.IdcEmpId == employeeId)
                        .GroupJoin(context.DocumentFiles, d => d.IdcId, f => f.DcfIdcId,
                            (d, f) => new { doc = d, files = f })
                        .ToList()
                        .Select(d => new IdentityDocumentsDto(d.doc, d.files))
                        .ToList();

                return documents;
            }
        }

        [HttpPost]
        [Route("api/Employees/SaveDocument")]
        public IdentityDocumentsDto SaveIdentityDocument([FromBody] IdentityDocumentsDto documentDto)
        {
            return SaveDocument(documentDto);
        }

        private IdentityDocumentsDto SaveDocument(IdentityDocumentsDto documentDto)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                IdentityDocuments document;
                if (documentDto.Id == 0)
                {
                    document = new IdentityDocuments
                    {
                        IdcAuditCd = DateTime.UtcNow,
                        IdcAuditCu = User.GetUserId()
                    };
                    context.IdentityDocuments.Add(document);
                }
                else
                {
                    document = context.IdentityDocuments.FirstOrDefault(d => d.IdcId == documentDto.Id);
                }

                document.IdcEmpId = documentDto.EmployeeId;
                document.IdcAuditMd = DateTime.UtcNow;
                document.IdcAuditMu = User.GetUserId();
                document.IdcIssueDate = documentDto.IssueDate;
                document.IdcNumber = documentDto.Number;
                document.IdcParentIdcId = documentDto.ParentDocumentId;
                document.IdcRemarks = documentDto.Remarks;
                document.IdcSeria = documentDto.Seria;
                document.IdcType = documentDto.Type;
                document.IdcValidFrom = documentDto.ValidFrom;
                document.IdcValidTo = documentDto.ValidTo;
                document.IdcVisaType = documentDto.VisaType;

                context.SaveChanges();

                foreach (var file in documentDto.Files)
                {
                    DocumentFiles docFile = new DocumentFiles
                    {
                        DcfAuditCd = DateTime.UtcNow,
                        DcfAuditCu = User.GetUserId(),
                        DcfDescription = file.Description,
                        DcfName = file.Name,
                        DcfIdcId = document.IdcId,
                        DcfUrl = file.Url
                    };

                    context.DocumentFiles.Add(docFile);
                }

                context.SaveChanges();

                documentDto.Id = document.IdcId;
                documentDto.Files =
                    context.DocumentFiles.Where(d => d.DcfIdcId == document.IdcId)
                        .ToList()
                        .Select(d => new DocumentFilesDto(d))
                        .ToList();

                return documentDto;
            }
        }

        [HttpPost]
        [Route("api/Employees/DeleteDocument")]
        public bool DeleteIdentityDocument([FromBody] int documentId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                IdentityDocuments doc = context.IdentityDocuments.FirstOrDefault(c => c.IdcId == documentId);

                if (doc == null)
                {
                    return false;
                }

                doc.IdcAuditRd = DateTime.UtcNow;
                doc.IdcAuditRu = User.GetUserId();
                context.SaveChanges();

                return true;
            }
        }

        [HttpPost]
        [Route("api/Employees/DeleteFile")]
        public bool DeleteIdDocumentFile([FromBody] int fileId)
        {
            using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
            {
                DocumentFiles docFile = context.DocumentFiles.FirstOrDefault(c => c.DcfId == fileId);

                if (docFile == null)
                {
                    return false;
                }

                docFile.DcfAuditRd = DateTime.UtcNow;
                docFile.DcfAuditRu = User.GetUserId();
                context.SaveChanges();

                return true;
            }
        }


        [HttpPost]
        [Route("/api/Offers/FinishEmployeeContract")]
        public bool FinishEmployeeContract([FromBody] EmploymentRequestDto employmentRequestDto)
        {
            try
            {
                var employee = new Employees();
                var contragent = new Contragent();
                var offer = new Offer();
                var responsibleUser = new AspNetUsers();
                string status;

                using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
                {
                    EmploymentRequests employmentRequest =
                        context.EmploymentRequests.FirstOrDefault(e => e.EtrOfrId == employmentRequestDto.OfferId && e.EtrEmpId == employmentRequestDto.EmployeeId && e.EtrAuditRd == null);

                    if (employmentRequest == null)
                    {
                        return false;
                    }

                    employmentRequest.EtrAuditMd = DateTime.UtcNow;
                    employmentRequest.EtrAuditMu = User.GetUserId();
                    employmentRequest.EtrStatus = employmentRequestDto.Status;

                    status = employmentRequestDto.Status == 1 ? "Accepted" : "Rejected";

                    employee = context.Employees.FirstOrDefault(c => c.EmpId == employmentRequest.EtrEmpId);
                    contragent = context.Contragents.FirstOrDefault(c => c.CgtId == employee.EmpCtgId);
                    offer = context.Offers.FirstOrDefault(o => o.OfrId == employmentRequest.EtrOfrId);
                    responsibleUser = context.AspNetUsers.FirstOrDefault(u => u.Id == contragent.CgtResponsibleUser);

                    if (employmentRequest.EtrStatus == 1)
                    {
                        employee.EmpCltId = employmentRequest.EtrCltId;
                        employee.EmpOffId = employmentRequest.EtrOfrId;
                    }

                    if (employmentRequest.EtrStatus == 2)
                    {
                        employee.EmpCltId = null;
                        employee.EmpOffId = null;
                    }


                    context.SaveChanges();
                }

            //    IEmailMessageBuilder bodyBuilder = new ChangePimpStatusBodyBuilder(new EmployeeDto(employee), new ContragentDto(contragent), new OfferDto(offer), status);
            //    string emailBody = bodyBuilder.BuildBody();
            //    string emailSubject = bodyBuilder.BuildSubject();

            //    NotificationEmailSender emailSender = new NotificationEmailSender();
            //    if (responsibleUser.Email != null)
            //    {
            //        //       await emailSender.SendEmailAsync(responsibleUser.Email, emailSubject, emailBody);
            //    }
                //   await emailSender.SendEmailAsync("tadeusz@eurojobs.info.pl", emailSubject, emailBody);


                return true;
            }
            catch (Exception)
            {
                //todo: add logging
                return false;
            }
        }


    }
}