﻿using System;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int ContragentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string ResponsibleUser { get; set; }
        public int? Status { get; set; }

        public EmployeeDto()
        {

        }

        public EmployeeDto(Employees e)
        {
            Id = e.EmpId;
            BirthDate = e.EmpBirthDate;
            ContragentId = e.EmpCtgId;
            FirstName = e.EmpFirstName;
            LastName = e.EmpLastName;
            MiddleName = e.EmpMiddleName;
            LastName = e.EmpLastName;
            Description = e.EmpDescription;
            ResponsibleUser = e.EmpResponsibleUser;
            Status = e.EmpStatus;
        }
    }
}