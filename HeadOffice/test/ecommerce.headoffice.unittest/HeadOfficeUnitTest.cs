using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.repository.test;
using NUnit.Framework;
using System.Collections.ObjectModel;
using ecommerce.datamodel;
using ecommerce.headoffice.repositories;
using ecommerce.core;
using ecommerce.headoffice.repository;
using Microsoft.Practices.ServiceLocation;

namespace ecommerce.headoffice.unittest
{
    public class HeadOfficeUnitTest : RepositoryBaseTest
    {
        private Merk merk = null;
        private IObjectContextManager _objectContext;
        private IHeadOfficeRepository _repository;
        private Collection<HeadOffice> headOfficeList = null;

        private HeadOffice headOfficeStub1 = new HeadOffice()
        {
            Id = Guid.NewGuid(),
            Address = "Jl Ganesha 10",
            City = "Bandung",
            Country = "Indonesia",
            CreateDate = DateTime.Now,
            CreatedBy = "Rahardian D J",
            Name = "PT TeenSpirit",
            Phone = "098273"
        };

        [SetUp]
        public void Initialization()
        {
            _objectContext = ServiceLocator.Current.GetInstance<IObjectContextManager>("ObjectContextManager");
            _repository = new HeadOfficeRepository(_objectContext);
        }

        [Test]
        public void InsertTest()
        {
            //Add first
            _repository.Add(headOfficeStub1);
            _repository.SaveChanges();

            //Check whether on database or not
            //merk = _repository.(headOfficeStub1.Id);
            //Assert.IsNotNull(merk, "Merk Berhasil Terbuat");

            //Delete so this method can be used again
            //_repository.Delete(merkStub1);
            //_repository.SaveChanges();
        }
    }
}
