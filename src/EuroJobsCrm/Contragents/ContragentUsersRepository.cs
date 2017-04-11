using System;
using System.Collections.Generic;
using System.Linq;
using EuroJobsCrm.Models;
using DbContext = EuroJobsCrm.Models.DB_A12601_bielkaContext;

namespace EuroJobsCrm.Contragents
{
    public class ContragentUsersRepository : IRepository<AspNetUsers>
    {
        /// <summary>
        /// The method returns an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity</returns>
        public AspNetUsers Get(int id)
        {
            using (DbContext context = new DbContext())
            {
                return context.AspNetUsers.FirstOrDefault(c => c.ContragentId == id);
            }
        }

        /// <summary>
        /// The method returns a list of all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        public List<AspNetUsers> Get()
        {
            using (DbContext context = new DbContext())
            {
                return context.AspNetUsers.Where(u => u.ContragentId != null).ToList();
            }
        }

        /// <summary>
        /// The method returns a list of filtered entities
        /// </summary>
        /// <param name="predicate">Filtering predicate</param>
        /// <returns>List of entities</returns>
        public IEnumerable<AspNetUsers> Get(Func<AspNetUsers, bool> predicate)
        {
            using (DbContext context = new DbContext())
            {
                return context.AspNetUsers.Where(u => u.ContragentId != null).ToList().Where(predicate).ToList();
            }
        }

        /// <summary>
        /// The method writes an entity to the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public async void Save(AspNetUsers entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            using (DbContext context = new DbContext())
            {
                if (string.IsNullOrEmpty(entity.Id))
                {
                    context.AspNetUsers.Add(entity);
                }

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// The method removes an entity from the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        public async void Delete(AspNetUsers entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            using (DbContext context = new DbContext())
            {
                if (string.IsNullOrEmpty(entity.Id))
                {
                    context.AspNetUsers.Remove(entity);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}