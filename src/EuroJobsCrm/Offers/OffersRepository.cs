using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;
using DbContext = EuroJobsCrm.Models.DB_A12601_bielkaContext;

namespace EuroJobsCrm.Offers
{
    public class OffersRepository : IRepository<Offer>
    {
        /// <summary>
        /// The method returns an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity</returns>
        public Offer Get(int id)
        {
            using (DbContext context = new DbContext())
            {
                Offer offer = context.Offers.FirstOrDefault(o => o.OfrId == id);
                return offer;
            }
        }

        /// <summary>
        /// The method returns a list of all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        public List<Offer> Get()
        {
            using (DbContext context = new DbContext())
            {
                List<Offer> offers = context.Offers.Where(o => o.OfrAuditRd == null).ToList();
                return offers;
            }
        }

        /// <summary>
        /// The method returns a list of filtered entities
        /// </summary>
        /// <param name="predicate">Filtering predicate</param>
        /// <returns>List of entities</returns>
        public IEnumerable<Offer> Get(Func<Offer, bool> predicate)
        {
            using (DbContext context = new DbContext())
            {
                List<Offer> offers = context.Offers.Where(predicate).ToList();
                return offers;
            }
        }

        /// <summary>
        /// The method writes an entity to the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public void Save(Offer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            if (entity.OfrId == 0)
            {
                entity.OfrAuditCd = DateTime.Now;
            }

            entity.OfrAuditMd = DateTime.Now;
            using (DbContext context = new DbContext())
            {
                context.Offers.Add(entity);
                context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// The method removes an entity from the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public void Delete(Offer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            using (DbContext context = new DbContext())
            {
                context.Offers.Remove(entity);
                context.SaveChangesAsync();
            }
        }
    }
}
