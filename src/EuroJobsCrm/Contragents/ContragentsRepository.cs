using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;
using DbContext = EuroJobsCrm.Models.DB_A12601_bielkaContext;

namespace EuroJobsCrm.Contragents
{
    public class ContragentsRepository : IRepository<Contragent>
    {
        /// <summary>
        /// The method returns an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity</returns>
        public virtual Contragent Get(int id)
        {
            using (DbContext context = new DbContext())
            {
                return context.Contragents.FirstOrDefault(c => c.CgtId == id);
            }
        }

        /// <summary>
        /// The method returns a list of all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        public virtual List<Contragent> Get()
        {
            using (DbContext context = new DbContext())
            {
                return context.Contragents.ToList();
            }
        }

        /// <summary>
        /// The method returns a list of filtered entities
        /// </summary>
        /// <param name="predicate">Filtering predicate</param>
        /// <returns>List of entities</returns>
        public virtual IEnumerable<Contragent> Get(Func<Contragent, bool> predicate)
        {
            using (DbContext context = new DbContext())
            {
                return context.Contragents.Where(predicate).ToList();
            }
        }

        /// <summary>
        /// The method writes an entity to the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public virtual async void Save(Contragent entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            using (DbContext context = new DbContext())
            {
                if (entity.CgtId == 0)
                {
                    entity.CgtAuditCd = DateTime.Now;                
                    context.Contragents.Add(entity);
                }

                entity.CgtAuditMd = DateTime.Now;
                await context.SaveChangesAsync();
            }
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

            using (DbContext context = new DbContext())
            {
                entity.CgtAuditRd = DateTime.Now;
                context.Contragents.Remove(entity);
                context.SaveChangesAsync();
            }
        }
    }
}