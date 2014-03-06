using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using ecommerce.core;
using ecommerce.marketarea.repositories;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Microsoft.Practices.ServiceLocation;
using ecommerce.marketarea.repository;
using ecommerce.repository.test;

namespace ecommerce.marketarea.unittest
{
    public class MarketAreaUnitTest : RepositoryBaseTest
    {

        private MarketArea marketAreaStub = new MarketArea()
        {
            Id = new Guid("2a2c57c3-361b-444b-b725-04215b49bc1a"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            CreatedBy = "Rahardian",
            CreatedDate = DateTime.Now,
            Name = "Bandung Area",
        };

        private MarketArea marketAreaStub1 = new MarketArea()
        {
            Id = new Guid("2a2c57c3-361b-444b-b725-04215b49bc1e"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            CreatedBy = "Rahardian",
            CreatedDate = DateTime.Now,
            Name = "Bandung Market"
        };
        private MarketArea area = null;

        #region Test Initialization

        private IObjectContextManager _objectContext;
        private IMarketAreaRepository _repository;

        private MarketArea marketAreaUpdateStub = new MarketArea()
        {
            Id = new Guid("2a2c57c3-361b-444b-b725-04215b49bc1b"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            CreatedBy = "Ekasari",
            CreatedDate = DateTime.Now,
            Name = "X"
        };
        private Collection<MarketArea> areaList = null;

        [SetUp]
        public void Initialization()
        {
            _objectContext = ServiceLocator.Current.GetInstance<IObjectContextManager>("ObjectContextManager");
            _repository = new MarketAreaRepository(_objectContext);
        }

        #endregion

        [Test]
        public void AddMarketAreaTest()
        {
            //Add first
            _repository.Add(marketAreaStub);
            _repository.SaveChanges();

            //Check whether on database or not
            area = _repository.GetById(marketAreaStub.Id);
            Assert.IsNotNull(area, "Market Area Baru Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(marketAreaStub);
            _repository.SaveChanges();

        }

        [Test]
        public void UpdateMarketAreaTest()
        {
            _repository.Update(marketAreaUpdateStub);
            _repository.SaveChanges();
            area = _repository.GetById(marketAreaUpdateStub.Id);

            Assert.AreEqual(marketAreaUpdateStub, area);
        }

        [Test]
        public void DeleteMarketAreaTest()
        {   //Add first to make data dummy
            _repository.Add(marketAreaStub);
            _repository.SaveChanges();

            //Delete
            _repository.Delete(marketAreaStub);
            _repository.SaveChanges();

            //make sure wheter data is no longer in database
            area = _repository.GetById(marketAreaStub.Id);
            Assert.IsNull(area, "Area Belum Terhapus");
        }

        [Test]
        public void GetByNameMarketAreaTest()
        {
            area = _repository.GetById(marketAreaStub1.Id);
            Assert.IsNotNull(area);
        }

        [Test]
        public void GetAllMarketAreaTest()
        {
            areaList = _repository.GetAll();
            Assert.IsNotNull(areaList, "Market Area Tidak Kosong");
        }
    }
}
