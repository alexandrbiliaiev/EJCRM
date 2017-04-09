using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Dto;
using DbContext = EuroJobsCrm.Models.DB_A12601_bielkaContext;

namespace EuroJobsCrm.Contragents
{
    public class EntireContragetsDataRepository : IRepository<ContragentDto>
    {
        /// <summary>
        /// The method returns a list of all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        public List<ContragentDto> Get()
        {
            using (DbContext context = new DbContext())
            {
                List<ContragentDto> contagents = context.Contragents
                    .Where(c => c.CgtAuditRd == null)
                    .GroupJoin(context.Addresses.Where(a => a.AdrAuditRd == null), c => c.CgtId, a => a.AdrCgtId,
                        (c, a) => new { Contragent = c, Addresses = a })
                    .GroupJoin(context.ContactPersons.Where(a => a.CtpAuditRd == null), c => c.Contragent.CgtId,
                        cp => cp.CtpCgtId,
                        (c, cp) => new { c.Contragent, c.Addresses, ContactPersons = cp })
                    .GroupJoin(context.Employees.Where(a => a.EmpAuditRd == null), c => c.Contragent.CgtId,
                        cp => cp.EmpCtgId,
                        (c, em) => new { c.Contragent, c.Addresses, c.ContactPersons, Employees = em })
                    .GroupJoin(context.DocumentFiles.Where(f => f.DcfAuditRu == null && f.DcfCntId != null),
                        c => c.Contragent.CgtId, f => f.DcfCntId,
                        (c, f) => new { c.Contragent, c.Addresses, c.ContactPersons, c.Employees, Files = f })
                    .LeftJoin(context.AspNetUsers.Where(u => !u.Deleted), c => c.Contragent.CgtResponsibleUser, u => u.Id,
                        (c, u) =>
                            new { c.Contragent, c.Addresses, c.ContactPersons, c.Employees, c.Files, ResponsibleUser = u })
                    .GroupJoin(context.Notes.Where(n => n.NotAuditRu == null).LeftJoin(context.AspNetUsers,
                            n => n.NotAuditCu, u => u.Id, (n, u) => new
                            {
                                Note = n,
                                UserData = u
                            }), c => c.Contragent.CgtId, n => n.Note.NotCtgId,
                        (c, n) => new
                        {
                            c.Contragent,
                            c.Addresses,
                            c.ContactPersons,
                            c.Files,
                            c.Employees,
                            c.ResponsibleUser,
                            Notes = n
                        })
                    .GroupJoin(
                        context.AspNetUsers.Where(u => !u.Deleted)
                        , c => c.Contragent.CgtId, u => u.ContragentId,
                        (c, u) => new
                        {
                            c.Contragent,
                            c.Addresses,
                            c.ContactPersons,
                            c.Employees,
                            c.Files,
                            c.ResponsibleUser,
                            c.Notes,
                            ContragentUsers = u.Select(e => new
                            {
                                e.Id,
                                e.UserName,
                                e.Email,
                                CtgId = e.ContragentId,
                                Name = e.FullName
                            })
                        })
                    .ToList()
                    .Select(
                        c =>
                            new ContragentDto(c.Contragent, c.Addresses, c.ContactPersons, c.Employees, c.Files,
                                c.ResponsibleUser, c.ContragentUsers.Select(e => new UserDto
                                {
                                    Id = e.Id,
                                    CtgId = e.CtgId,
                                    Email = e.Email,
                                    Name = e.Name,
                                    UserName = e.UserName,
                                    UserRole = null,
                                }).ToList())
                            {
                                Notes = c.Notes.Select(n => new EventDetailsDto(n.Note)
                                {
                                    TargetUserName = n.UserData?.FullName,
                                    TargetUser = n.UserData?.Id
                                }).ToList()
                            })
                    .ToList();

                return contagents;
            }
        }

        /// <summary>
        /// The method returns an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity</returns>
        public ContragentDto Get(int id)
        {
            using (DbContext context = new DbContext())
            {
                ContragentDto contagent = context.Contragents
                    .Where(c => c.CgtAuditRd == null && c.CgtId == id)
                    .GroupJoin(context.Addresses.Where(a => a.AdrAuditRd == null), c => c.CgtId, a => a.AdrCgtId,
                        (c, a) => new { Contragent = c, Addresses = a })
                    .GroupJoin(context.ContactPersons.Where(a => a.CtpAuditRd == null), c => c.Contragent.CgtId,
                        cp => cp.CtpCgtId,
                        (c, cp) => new { c.Contragent, c.Addresses, ContactPersons = cp })
                    .GroupJoin(context.Employees.Where(a => a.EmpAuditRd == null), c => c.Contragent.CgtId,
                        cp => cp.EmpCtgId,
                        (c, em) => new { c.Contragent, c.Addresses, c.ContactPersons, Employees = em })
                    .GroupJoin(context.DocumentFiles.Where(f => f.DcfAuditRu == null && f.DcfCntId != null),
                        c => c.Contragent.CgtId, f => f.DcfCntId,
                        (c, f) => new { c.Contragent, c.Addresses, c.ContactPersons, c.Employees, Files = f })
                    .LeftJoin(context.AspNetUsers.Where(u => !u.Deleted), c => c.Contragent.CgtResponsibleUser, u => u.Id,
                        (c, u) =>
                            new { c.Contragent, c.Addresses, c.ContactPersons, c.Employees, c.Files, ResponsibleUser = u })
                    .GroupJoin(context.Notes.Where(n => n.NotAuditRu == null).LeftJoin(context.AspNetUsers,
                            n => n.NotAuditCu, u => u.Id, (n, u) => new
                            {
                                Note = n,
                                UserData = u
                            }), c => c.Contragent.CgtId, n => n.Note.NotCtgId,
                        (c, n) => new
                        {
                            c.Contragent,
                            c.Addresses,
                            c.ContactPersons,
                            c.Files,
                            c.Employees,
                            c.ResponsibleUser,
                            Notes = n
                        })
                    .GroupJoin(
                        context.AspNetUsers.Where(u => !u.Deleted)
                        , c => c.Contragent.CgtId, u => u.ContragentId,
                        (c, u) => new
                        {
                            c.Contragent,
                            c.Addresses,
                            c.ContactPersons,
                            c.Employees,
                            c.Files,
                            c.ResponsibleUser,
                            c.Notes,
                            ContragentUsers = u.Select(e => new
                            {
                                e.Id,
                                e.UserName,
                                e.Email,
                                CtgId = e.ContragentId,
                                Name = e.FullName
                            })
                        })
                    .ToList()
                    .Select(
                        c =>
                            new ContragentDto(c.Contragent, c.Addresses, c.ContactPersons, c.Employees, c.Files,
                                c.ResponsibleUser, c.ContragentUsers.Select(e => new UserDto
                                {
                                    Id = e.Id,
                                    CtgId = e.CtgId,
                                    Email = e.Email,
                                    Name = e.Name,
                                    UserName = e.UserName,
                                    UserRole = null,
                                }).ToList())
                            {
                                Notes = c.Notes.Select(n => new EventDetailsDto(n.Note)
                                {
                                    TargetUserName = n.UserData?.FullName,
                                    TargetUser = n.UserData?.Id
                                }).ToList()
                            })
                    .FirstOrDefault();

                return contagent;
            }
        }

        /// <summary>
        /// The method returns a list of filtered entities
        /// </summary>
        /// <param name="predicate">Filtering predicate</param>
        /// <returns>List of entities</returns>
        public IEnumerable<ContragentDto> Get(Func<ContragentDto, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }

        /// <summary>
        /// The method writes an entity to the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public void Save(ContragentDto entity)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The method removes an entity from the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public void Delete(ContragentDto entity)
        {
            throw new NotSupportedException();
        }
    }
}