using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;
using DbContext = EuroJobsCrm.Models.DB_A12601_bielkaContext;

namespace EuroJobsCrm.Contragents
{
    public class ContragentsRepository : IRepository<Contragent>, IDisposable
    {
        private readonly DbContext _context;
        public ContragentsRepository()
        {
            _context = new DbContext();
        }

        /// <summary>
        /// The method returns an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity</returns>
        public virtual Contragent Get(int id)
        {
            return _context.Contragents.FirstOrDefault(c => c.CgtId == id);
        }

        /// <summary>
        /// The method returns a list of all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        public virtual List<Contragent> Get()
        {
            return _context.Contragents.Where(c => c.CgtAuditRd == null).ToList();
        }

        /// <summary>
        /// The method returns a list of filtered entities
        /// </summary>
        /// <param name="predicate">Filtering predicate</param>
        /// <returns>List of entities</returns>
        public virtual IEnumerable<Contragent> Get(Func<Contragent, bool> predicate)
        {
            return _context.Contragents.Where(c => c.CgtAuditRd == null && predicate(c)).ToList();
        }

        /// <summary>
        /// The method writes an entity to the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public virtual void Save(Contragent entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            if (entity.CgtId == 0)
            {
                entity.CgtAuditCd = DateTime.Now;
                _context.Contragents.Add(entity);
            }

            entity.CgtAuditMd = DateTime.Now;
            _context.SaveChanges();

        }

        /// <summary>
        /// The method removes an entity from the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public virtual void Delete(Contragent entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            entity.CgtAuditRd = DateTime.Now;
            _context.SaveChanges();         
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}