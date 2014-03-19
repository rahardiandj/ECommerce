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
        private HeadOffice headOffice = null;
        private IObjectContextManager _objectContext;
        private IHeadOfficeRepository _repository;
        private Collection<HeadOffice> headOfficeList = null;

        private HeadOffice headOfficeStub1 = new HeadOffice()
        {
            Id = new Guid("2a2c57c3-361b-444b-b725-04215b49bc1d"),
            Address = "Jl Ganesha 10",
            City = "Bandung",
            Country = "Indonesia",
            CreateDate = DateTime.Now,
            CreatedBy = "Rahardian D J",
            Name = "PT TeenSpirit",
            Phone = "098273"
        };

        private HeadOffice headOfficeUpdateStub1 = new HeadOffice()
        {
            Id = new Guid("2a2c57c3-361b-444b-b725-04215b49bc1c"),
            Address = "Jl Bungur 10",
            City = "Cimahi",
            Country = "Indonesia",
            CreateDate = DateTime.Now,
            CreatedBy = "Endang S",
            Name = "PT Teen",
            Phone = "098274"
        };

        private HeadOffice headOfficeInsertStub1 = new HeadOffice()
        {
            Id = new Guid("2a2c57c3-361b-444b-b725-04215b49bc1c"),
            Address = "Jl Majapahit",
            City = "Cimahi",
            Country = "Indonesia",
            CreateDate = DateTime.Now,
            CreatedBy = "Endang S",
            Name = "PT Teen",
            Phone = "098274"
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
            _repository.Add(headOfficeStub1);
            _repository.SaveChanges();

            //Check whether on database or not
            headOffice = _repository.GetById(headOfficeStub1.Id);
            Assert.IsNotNull(headOffice, "Data Head Office Baru Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(headOfficeStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void GetByIdHeadOfficeTest()
        {
            //Add data 
            _repository.Add(headOfficeStub1);
            _repository.SaveChanges();

            //Test case
            HeadOffice headoffice = _repository.GetById(headOfficeStub1.Id);
            Assert.IsNotNull(headoffice);

            //Delete data
            _repository.Delete(headOfficeStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void AddHeadOfficeTest()
        {
            //Add first
            _repository.Add(headOfficeStub1);
            _repository.SaveChanges();

            //Check whether on database or not
            headOffice = _repository.GetById(headOfficeStub1.Id);
            Assert.IsNotNull(headOffice, "Data Head Office Baru Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(headOfficeStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void DeleteHeadOfficeTest()
        {
            //Add first to make data dummy
            _repository.Add(headOfficeStub1);
            _repository.SaveChanges();

            //Delete
            _repository.Delete(headOfficeStub1);
            _repository.SaveChanges();

            //make sure wheter data is no longer in database
            headOffice = _repository.GetById(headOfficeStub1.Id);
            Assert.IsNull(headOffice, "Head Office Terhapus");
        }

        [Test]
        public void GetAllHeadOfficeTest()
        {
            headOfficeList = _repository.GetAll();
            Assert.IsNotNull(headOfficeList, "Data Head Office tidak Kosong");
        }


        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void UpdateNullHeadOfficeTest()
        {
            _repository.Update(headOfficeUpdateStub1);
            _repository.SaveChanges();
            headOffice = _repository.GetById(headOfficeUpdateStub1.Id);
        }

        [Test]
        public void UpdateHeadOfficeTest()
        {
            //precondition
            _repository.Add(headOfficeInsertStub1);
            _repository.SaveChanges();

            _repository.Update(headOfficeUpdateStub1);
            _repository.SaveChanges();
            headOffice = _repository.GetById(headOfficeUpdateStub1.Id);
            Assert.AreEqual(headOffice.Address, headOfficeUpdateStub1.Address);

            //delete data
            _repository.Delete(headOfficeUpdateStub1);
            _repository.SaveChanges();
        }
    }
}
