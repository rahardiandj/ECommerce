using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ecommerce.core
{
        public interface IDALRepository<T> where T : class
        {
            /// <summary>
            /// Return Object Context Manager that will manages the Object Context.
            /// </summary>
            IObjectContextManager ObjectContexManager { get; set; }

            /// <summary>
            /// Returns all entities for a given type.
            /// </summary>
            /// <returns>All entities</returns>
            Collection<T> GetAll();


            /// <summary>
            /// Return entities matches a string criteria.
            /// </summary>
            /// <param name="criteria">criteria for selection in string</param>
            /// <returns>Matching entities</returns>
            Collection<T> GetByCriteria(string criteria);

            /// <summary>
            /// Add entity to the repository.
            /// </summary>
            /// <param name="entity">the entity to add</param>
            /// <returns>The added entity</returns>
            void Add(T entity);

            /// <summary>
            /// Update entity in reporsitory.
            /// </summary>
            /// <param name="entity">The updated entity</param>
            void Update(T entity);

            /// <summary>
            /// Mark entity to be deleted within the repository.
            /// </summary>
            /// <param name="entity">The entity to delete</param>
            void Delete(T entity);

            /// <summary>
            /// Save all changes from repository to store.
            /// </summary>
            /// <returns>Total number of objects affected</returns>
            int SaveChanges();
        }
    
}
