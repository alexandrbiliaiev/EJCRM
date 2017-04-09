using System;
using System.Collections.Generic;

namespace EuroJobsCrm
{
    public interface IRepository<T> where T : class 
    {
        /// <summary>
        /// The method returns an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity</returns>
        T Get(int id);

        /// <summary>
        /// The method returns a list of all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        List<T> Get();

        /// <summary>
        /// The method returns a list of filtered entities
        /// </summary>
        /// <param name="predicate">Filtering predicate</param>
        /// <returns>List of entities</returns>
        IEnumerable<T> Get(Func<T, bool> predicate);

        /// <summary>
        /// The method writes an entity to the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        void Save(T entity);

        /// <summary>
        /// The method removes an entity from the storage
        /// </summary>
        /// <param name="entity">The entity</param>
        void Delete(T entity);
    }
}