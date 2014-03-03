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

        #region Test Initialization

        private IObjectContextManager _objectContext;
        private IMarketAreaRepository _repository;

        [SetUp]
        public void Initialization()
        {
            _objectContext = ServiceLocator.Current.GetInstance<IObjectContextManager>("ObjectContextManager");
            _repository = new MarketAreaRepository(_objectContext);
        }

        #endregion

        [Test]
        public void InsertTest()
        {
            _repository.Add(marketAreaStub1);
            _repository.SaveChanges();
        }
    }
}
