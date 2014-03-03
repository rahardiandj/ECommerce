using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using ecommerce.headoffice.repositories;
using ecommerce.core;
using System.Collections.ObjectModel;
using System.Security.Principal;

namespace ecommerce.headoffice.repository
{
    public class HeadOfficeRepository : GenericRepository<HeadOffice>, IHeadOfficeRepository
    {
        #region Constructor

        public HeadOfficeRepository(IObjectContextManager objectContextManager)
            : base(objectContextManager)
        {
        }

        #endregion

        public override void Add(HeadOffice entity)
        {
            entity.CreatedBy = Environment.UserName;
            entity.CreateDate = DateTime.Now;
            Entities.AddObject(entity);
        }

        public override void Update(HeadOffice entity)
        {
            var headOffice = (from e in Entities
                              where e.Id == entity.Id
                              select e).FirstOrDefault();
            if (headOffice == null)
                throw (new InvalidOperationException("Entity doesn't exist in database."));

            Entities.ApplyCurrentValues(entity);
        }

        public override void Delete(HeadOffice entity)
        {
            var merk = (from e in Entities
                        where e.Id == entity.Id
                        select e).FirstOrDefault();

            Entities.DeleteObject(merk);
        }

        Collection<HeadOffice> IDALRepository<HeadOffice>.GetAll()
        {
            Collection<HeadOffice> results = new Collection<HeadOffice>(Entities.OrderBy(C => C.Name).ToList());
            return results;
        }

        HeadOffice IHeadOfficeRepository.GetById(Guid id)
        {
            var headOffice = (from e in Entities
                        where e.Id == id
                        select e).FirstOrDefault();
            return headOffice;
        }

        #region Not Implemented

        IObjectContextManager IDALRepository<HeadOffice>.ObjectContexManager
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

        System.Collections.ObjectModel.Collection<HeadOffice> IDALRepository<HeadOffice>.GetByCriteria(string criteria)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
