using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using ecommerce.store.repositories;
using System.Collections.ObjectModel;
using ecommerce.core;

namespace ecommerce.store.repository
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        #region Constructor

        public StoreRepository(IObjectContextManager objectContextManager)
            : base(objectContextManager)
        {
        }

        #endregion

        public override void Add(Store entity)
        {
            Entities.AddObject(entity);
        }

        public override void Update(Store entity)
        {
            var store = (from e in Entities
                        where e.Id == entity.Id
                        select e).FirstOrDefault();
            if (store == null)
                throw (new InvalidOperationException("Entity doesn't exist in database."));

            Entities.ApplyCurrentValues(entity);
        }

        public override void Delete(Store entity)
        {
            var merk = (from e in Entities
                        where e.Id == entity.Id
                        select e).FirstOrDefault();

            Entities.DeleteObject(merk);
        }

        Store IStoreRepository.GetById(Guid id)
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

        Collection<Store> core.IDALRepository<Store>.GetAll()
        {
            Collection<Store> results = new Collection<Store>(Entities.OrderBy(C => C.Name).ToList());
            return results;
        }


        #region Not Implemented

        core.IObjectContextManager core.IDALRepository<Store>.ObjectContexManager
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

        

        System.Collections.ObjectModel.Collection<Store> core.IDALRepository<Store>.GetByCriteria(string criteria)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
