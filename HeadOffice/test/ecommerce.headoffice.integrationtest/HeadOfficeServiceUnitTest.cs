using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using System.Collections.ObjectModel;
using NUnit.Framework;
using ecommerce.headoffice.repositories;
using NSubstitute;
using ecommerce.headoffice.entities;

namespace ecommerce.headoffice.integrationtest
{
    public class HeadOfficeServiceUnitTest
    {
        #region Dummy Data
        HeadOfficeDomain h = null;
        private HeadOffice headOffice1 = new HeadOffice()
        {
            Id = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A12"),
            Address = "Jl Sarijadi 40",
            City = "Bandung",
            Country = "Indonesia",
            CreateDate = DateTime.Now,
            CreatedBy = "BlackMail",
            Name = "PT BlackMail",
            Phone = "098987",
        };

        private HeadOffice headOffice2 = new HeadOffice()
        {
            Id = new Guid("4DD4B154-0F16-49E0-AB9B-8D17354C4A12"),
            Address = "Jl Hegarmanah 40",
            City = "Bandung",
            Country = "Indonesia",
            CreateDate = DateTime.Now,
            CreatedBy = "BrownMail",
            Name = "PT BrownMail",
            Phone = "098985",
        };

        private HeadOfficeDomain headOfficeDomainNull = new HeadOfficeDomain()
        {
            Id = new Guid("4FF4B154-0F16-49E0-AB9B-8D17354C4A13"),
            Address = "",
            City = "",
            Country = "",
            Name = "",
        };

        private HeadOfficeDomain headOfficeDomain = new HeadOfficeDomain()
        {
            Id = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13"),
            Address = "Jl Sarimanah 40",
            City = "Bandung",
            Country = "Indonesia",
            Name = "PT WhiteMail",
        };

        private HeadOfficeDomain headOfficeDomainExist = new HeadOfficeDomain()
        {
            Id = new Guid("4BB4B154-0F16-49E0-AB9B-8D17354C4A13"),
            Address = "Jl Saribundo 40",
            City = "Bandung",
            Country = "Indonesia",
            Name = "PT Mail",
        };

        private Collection<HeadOffice> headOffices = new Collection<HeadOffice>();
        private Guid id1 = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13");

        #endregion

        #region Test Initilization

        private HeadOfficeDomainService service;
            

        [SetUp]
        public void Initialization()
        {
            //Mock up object
            var repository = Substitute.For<IHeadOfficeRepository>();
            h = new HeadOfficeDomain();
            service = new HeadOfficeDomainService(repository);
            repository.GetById(id1).Returns(headOffice1);
            headOffices.Add(headOffice1);
            repository.GetAll().Returns(headOffices);
        }

        #endregion

        #region Test Method

        [Test]
        public void GetByIdHeadOfficeServiceTest()
        {
            Assert.NotNull(service.GetHeadOfficeById(id1));
        }

        [Test]
        public void GetHeadOfficeIfNotExistTest()
        {
            string message = service.GetHeadOfficeById(headOffice2.Id).Messages.FirstOrDefault().ToString();
            Assert.AreEqual(message, "Data is not in Database");
        }

        [Test]
        public void InsertTest()
        {
            Assert.IsNotEmpty(service.Create(headOfficeDomain).Messages);
        }

        [Test]
        public void UpdateTest()
        {
            Assert.IsEmpty(service.Update(headOfficeDomainExist).Messages);
        }

        [Test]
        public void GetAllHeadOfficeTest()
        {
            Collection<HeadOfficeDomain> headOffices = service.GetAllMerk().HeadOfficeDomains;
            Assert.IsNotNull(headOffices);
        }

        [Test]
        public void GetAllHeadOfficeNullTest()
        {
            var _repository = Substitute.For<IHeadOfficeRepository>();
            _repository.GetAll().Returns(new Collection<HeadOffice>());
            HeadOfficeDomainService _service = new HeadOfficeDomainService(_repository);
            string message = _service.GetAllMerk().Messages.FirstOrDefault().Value;
            Assert.AreEqual(message, "Tidak Ada Merk Yang Terdaftar");
        }

        [Test]
        public void getValidateIsNotExistTest()
        {
            Assert.IsNotNull(service.GetHeadOfficeById(headOffice1.Id));
        }


        #endregion
    }
}
