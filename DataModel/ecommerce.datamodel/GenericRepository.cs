using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using ecommerce.core;
using System.Collections.ObjectModel;

namespace ecommerce.datamodel
{
    public abstract class GenericRepository<T> : IDALRepository<T> where T : class
    {
        private IObjectContextManager _objectContextManager;
        private ObjectSet<T> _entities;

        /// <summary>
        /// Return Object Context Manager that will manages the Object Context.
        /// </summary>
        public IObjectContextManager ObjectContexManager
        {
            get { return _objectContextManager; }
            set { _objectContextManager = value; }
        }

        /// <summary>
        /// Returns ObjectContext used by repository.
        /// </summary>
        protected ObjectContext Context
        {
            get { return _objectContextManager.ObjectContext; }
        }

        /// <summary>
        /// Initializes the repository.
        /// </summary>
        /// <param name="objectContexManager">Manager object that holds the ObjectContext</param>
        protected GenericRepository(IObjectContextManager objectContexManager)
        {
            _objectContextManager = objectContexManager;
        }

        #region IDALRepository

        /// <summary>
        /// Entities contained by this repository.
        /// </summary>        
        protected ObjectSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.CreateObjectSet<T>()); }
        }

        /// <summary>
        /// Get All entities.
        /// </summary>        
        public virtual Collection<T> GetAll()
        {
            return new Collection<T>(Entities.ToList<T>());
        }

        /// <summary>
        /// Add entity to the repository.
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        public abstract void Add(T entity);

        /// <summary>
        /// Update entity in reporsitory.
        /// </summary>
        /// <param name="entity">The updated entity</param>
        public abstract void Update(T entity);

        /// <summary>
        /// Save all changes from repository to store.
        /// </summary>
        /// <returns>Total number of objects affected</returns>
        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }

        /// <summary>
        /// Mark entity to be deleted within the repository.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public abstract void Delete(T entity);

        /// <summary>
        /// Return entities matches a string criteria.
        /// </summary>
        /// <param name="criteria">criteria for selection in string</param>
        /// <returns>Matching entities</returns>
        public virtual Collection<T> GetByCriteria(string criteria)
        {
            return new Collection<T>(Entities.Where(criteria).ToList());
        }

        #endregion

    }
}
