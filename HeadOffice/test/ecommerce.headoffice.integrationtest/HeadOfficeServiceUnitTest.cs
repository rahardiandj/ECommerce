using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using System.Collections.ObjectModel;
using NUnit.Framework;
using ecommerce.headoffice.repositories;
using NSubstitute;

namespace ecommerce.headoffice.integrationtest
{
    public class HeadOfficeServiceUnitTest
    {
        #region Dummy Data

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


        #endregion
    }
}
