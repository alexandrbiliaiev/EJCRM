using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public class EmployeeController : Controller
    {
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
                emp.EmpMiddleName = employee.MiddleName;
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
                            (d, f) => new {doc = d, files = f})
                        .ToList()
                        .Select(d => new IdentityDocumentsDto(d.doc, d.files))
                        .ToList();

                return documents;
            }
        }

    }
}