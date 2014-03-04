using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ecommerce.merk.aggregate;
using ecommerce.merk.parameters;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using Microsoft.Practices.ServiceLocation;
using ecommerce.datamodel;
using ecommerce.merk.repositories;
using ecommerce.merk.repository;
using CRS.Repository.Test;
using ecommerce.core;
using System.Collections.ObjectModel;

namespace ecommerce.merk.unittest
{
    [TestFixture]
    public class MerkUnitTest : RepositoryBaseTest
    {
        
        #region Dummy Data
        

        private Merk merkStub1 = new Merk()
        {
            Id = "007",
            Code = "B",
            Name = "Billabong",
            Manufacture = "Billabong"
        };

        private Merk merkUpdateStub = new Merk()
        {
            Id = "X",
            Code = "X",
            Name = "Planet Surf",
            Manufacture = "Billabong",
        };   

        #endregion

        #region Test Initialization

        private Merk merk = null;
        private IObjectContextManager _objectContext;
        private IMerkRepository _repository;
        private Collection<Merk> merkList = null;
        
        [SetUp]
        public void Initialization()
        {
            _objectContext = ServiceLocator.Current.GetInstance<IObjectContextManager>("ObjectContextManager");
            _repository = new MerkRepository(_objectContext);
        }

        #endregion

        #region Test Method

        [Test]
        public void InsertTest()
        {
            //Add first
            _repository.Add(merkStub1);
            _repository.SaveChanges();

            //Check whether on database or not
            merk = _repository.GetById(merkStub1.Id);
            Assert.IsNotNull(merk, "Merk Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(merkStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void GetByIdMerkTest()
        {
            merk = _repository.GetById("X"); 
            Assert.IsNotNull(merk);
        }

        [Test]
        public void GetAllMerkTest()
        {
            merkList = _repository.GetAll();
            Assert.IsNotNull(merkList, "Merk Tidak Kosong");
        }

        [Test]
        public void UpdateTest()
        {
            _repository.Update(merkUpdateStub);
            _repository.SaveChanges();
            merk =_repository.GetById(merkUpdateStub.Id);
            
            Assert.AreEqual(merkUpdateStub.Id, merk.Id);
        }

        [Test]
        public void DeleteMerk()
        {
            //Add first to make data dummy
            _repository.Add(merkStub1);
            _repository.SaveChanges();

            //Delete
            _repository.Delete(merkStub1);
            _repository.SaveChanges();
            
            //make sure wheter data is no longer in database
            merk = _repository.GetById(merkStub1.Id);
            Assert.IsNull(merk, "Merk Belum Terhapus");
        }
        
        #endregion 
    }
}
