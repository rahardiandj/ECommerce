using System;
using ecommerce.merk.aggregate;
using ecommerce.merk.repositories;
using ecommerce.datamodel;
using System.Data.SqlServerCe;
using System.Data.Objects;
using ecommerce.core;
using System.Linq;
using System.Collections.ObjectModel;
using System.Security.Principal;

namespace ecommerce.merk.repository
{
    public class MerkRepository : GenericRepository<Merk>, IMerkRepository
    {
        #region Constructor

        public MerkRepository(IObjectContextManager objectContextManager)
            : base(objectContextManager)
        {
        }

        #endregion

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        public Collection<Merk> GetAll()
        {
            Collection<Merk> results = new Collection<Merk>(Entities.OrderBy(C => C.Name).ToList());
            return results;
        }

        /// <summary>
        /// Insert Into Database
        /// </summary>
        /// <param name="entity"></param>
        public override void Add(Merk entity)
        {
            entity.CreatedBy = Environment.UserName;
            entity.CreateDate = DateTime.Now;
            Entities.AddObject(entity);
        }

        /// <summary>
        /// Update Data in Database
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(Merk entity)
        {
            var merk = (from e in Entities
                        where e.Id == entity.Id
                        select e).FirstOrDefault();
            if (merk == null)
                throw (new InvalidOperationException("Entity doesn't exist in database."));

            Entities.ApplyCurrentValues(entity);
        }

        /// <summary>
        /// Delete Data From Database
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(Merk entity)
        {
            var merk = (from e in Entities
                        where e.Id == entity.Id
                        select e).FirstOrDefault();

            Entities.DeleteObject(merk);
        }

        /// <summary>
        /// Get Data By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Merk GetById(string id)
        {
            try
            {
                var merk = (from e in Entities
                            where e.Id == id
                            select e).FirstOrDefault();
                return merk;
            }
            catch
            {
                return null;
            }
        }

        #region Not Implemented
        IObjectContextManager IDALRepository<Merk>.ObjectContexManager
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        System.Collections.ObjectModel.Collection<Merk> IDALRepository<Merk>.GetByCriteria(string criteria)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
