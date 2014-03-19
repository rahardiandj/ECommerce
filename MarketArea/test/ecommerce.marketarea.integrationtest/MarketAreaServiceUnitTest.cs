using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.ObjectModel;
using ecommerce.datamodel;
using ecommerce.marketarea.repositories;
using NSubstitute;
using ecommerce.marketarea.entities;

namespace ecommerce.marketarea.integrationtest
{
    public class MarketAreaServiceUnitTest
    {
        #region Dummy Data

        MarketAreaDomain m = null;

        private MarketArea marketArea1 = new MarketArea()
        {
            Id = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area 1",
            CreatedBy = "Nastain",
            CreatedDate = DateTime.Now,
        };

        private MarketArea marketArea2 = new MarketArea()
        {
            Id = new Guid("e41118af-a430-407e-a472-4db2750086b9"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area 2",
            CreatedBy = "Nastain",
            CreatedDate = DateTime.Now,
        };

        private MarketArea marketAreaInsert = new MarketArea()
        {
            Id = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area 3",
        };

        private MarketAreaDomain marketDomainNull = new MarketAreaDomain()
        {
            Id = new Guid("54bb1e9e-804c-4b1d-9799-1d18e705c976"),
            City = "",
            CodeArea = "",
            Country = "",
            Name = "",
        };

        private MarketAreaDomain marketAreaDomain = new MarketAreaDomain()
        {
            Id = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area 4",
        };

        private MarketAreaDomain marketAreaDomainInsert = new MarketAreaDomain()
        {
            Id = new Guid("90fc798e-37c5-4d1b-8563-af084b227eca"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area 5",
        };

        private MarketAreaDomain marketAreaDomainExist = new MarketAreaDomain()
        {
            Id = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area 6",
        };

        private Collection<MarketArea> marketAreas = new Collection<MarketArea>();

        private Guid id1 = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706");

        #endregion

        #region Test Initialization

        private MarketAreaDomainService service;


        [SetUp]
        public void Initialization()
        {
            //Mock up object
            var repository = Substitute.For<IMarketAreaRepository>();
            m = new MarketAreaDomain();
            service = new MarketAreaDomainService(repository);
            repository.GetById(id1).Returns(marketArea1);
            marketAreas.Add(marketArea1);
            marketAreas.Add(marketAreaInsert);
            repository.GetAll().Returns(marketAreas);
        }

        #endregion

        #region Test Method

        [Test]
        public void GetByIdMarketAreaServiceTest()
        {
            Assert.NotNull(service.GetMarketAreaById(id1));
        }

        public void GetMarketAreaIfNotExistTest()
        {
            string message = service.GetMarketAreaById(marketArea2.Id).Messages.FirstOrDefault().ToString();
            Assert.AreEqual(message, "Data is not in Database");
        }

        [Test]
        public void InsertTest()
        {
            Assert.IsNotEmpty(service.Create(marketAreaDomain).Messages);
        }

        [Test]
        public void InsertNotNullTest()
        {
            Assert.IsEmpty(service.Create(marketAreaDomainInsert).Messages);
        }

        [Test]
        public void UpdateTest()
        {
            Assert.IsEmpty(service.Update(marketAreaDomainExist).Messages);
        }

        [Test]
        public void UpdateNotExistTest()
        {
            Assert.AreEqual(service.Update(marketAreaDomainInsert).Messages.FirstOrDefault().Value, "Data is not in Database");
        }

        [Test]
        public void GetAllMarketAreaTest()
        {
            Collection<MarketAreaDomain> headOffices = service.GetAllMarketArea().MarketAreaDomains;
            Assert.IsNotNull(marketAreas);
        }

        [Test]
        public void GetAllMarketAreaNullTest()
        {
            var _repository = Substitute.For<IMarketAreaRepository>();
            _repository.GetAll().Returns(new Collection<MarketArea>());
            MarketAreaDomainService _service = new MarketAreaDomainService(_repository);
            string message = _service.GetAllMarketArea().Messages.FirstOrDefault().Value;
            Assert.AreEqual(message, "Tidak Ada Market Area Yang Terdaftar");
        }

        [Test]
        public void getValidateIsNotExistTest()
        {
            Assert.IsNotNull(service.GetMarketAreaById(marketArea1.Id));
        }

        #endregion
    }
}
