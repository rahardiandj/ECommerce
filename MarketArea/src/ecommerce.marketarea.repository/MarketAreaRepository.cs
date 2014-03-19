using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using ecommerce.marketarea.repositories;
using ecommerce.core;
using System.Collections.ObjectModel;
using System.Security.Principal;

namespace ecommerce.marketarea.repository
{
    public class MarketAreaRepository : GenericRepository<MarketArea>, IMarketAreaRepository
    {

        #region Constructor

        public MarketAreaRepository(IObjectContextManager objectContextManager)
            : base(objectContextManager)
        {
        }

        #endregion

        public override void Add(MarketArea entity)
        {
            entity.CreatedBy = Environment.UserName;
            entity.CreatedDate = DateTime.Now;
            Entities.AddObject(entity);
        }

        public override void Update(MarketArea entity)
        {
            var marketArea = (from e in Entities
                              where e.Id == entity.Id
                              select e).FirstOrDefault();
            if (marketArea == null)
                throw (new InvalidOperationException("Entity doesn't exist in database."));

            Entities.ApplyCurrentValues(entity);
        }

        public override void Delete(MarketArea entity)
        {
            var marketArea = (from e in Entities
                            where e.Id == entity.Id
                            select e).FirstOrDefault();

            Entities.DeleteObject(marketArea);
        }

        MarketArea IMarketAreaRepository.GetById(Guid id)
        {
            var marketArea = (from e in Entities
                              where e.Id == id
                              select e).FirstOrDefault();
            return marketArea;

        }

        Collection<MarketArea> core.IDALRepository<MarketArea>.GetAll()
        {
            Collection<MarketArea> results = new Collection<MarketArea>(Entities.OrderBy(C => C.Name).ToList());
            return results;
        }


        #region Not Implemented

        core.IObjectContextManager core.IDALRepository<MarketArea>.ObjectContexManager
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

        

        //System.Collections.ObjectModel.Collection<MarketArea> core.IDALRepository<MarketArea>.GetByCriteria(string criteria)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

    }
}
