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
    public class MarketAreaIntegrationTest
    {
        #region Dummy Data
        private MarketArea marketArea1 = new MarketArea()
        {
            Id = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A14"),
            City = "Bandung",
            CodeArea = "BDO",
            Country = "Indonesia",
            Name = "Bandung Area",
            CreatedBy = "Rahardian",
            CreatedDate = DateTime.Now,
        };
        

        private Guid id1 = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A14");

        private Collection<MarketArea> marketAreas = new Collection<MarketArea>();
        #endregion

        #region Test Initialization
        MarketAreaDomainService service;


        [SetUp]
        public void Initialization()
        {
            var repository = Substitute.For<IMarketAreaRepository>();
            service = new MarketAreaDomainService(repository);
            repository.GetById(id1).Returns(marketArea1);
            marketAreas.Add(marketArea1);
            repository.GetAll().Returns(marketAreas);
        }

        #endregion

        #region Test Method

        [Test]
        public void GetMarketAreaByIdTest()
        {
            Assert.NotNull(service.GetMarketAreaById(id1).MarketAreaDomain);
        }

        #endregion
    }
}
