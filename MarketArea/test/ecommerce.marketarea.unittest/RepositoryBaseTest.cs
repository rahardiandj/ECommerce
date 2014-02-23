using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Releasers;
using Castle.MicroKernel.Registration;
using System.IO;
using Microsoft.Practices.Windsor.ServiceLocatorAdapter;
using Microsoft.Practices.ServiceLocation;
using ecommerce.datamodel;
using ecommerce.core;

namespace ecommerce.repository.test
{
    public class RepositoryBaseTest
    {
        private static WindsorContainer _container;
        private static WindsorServiceLocator _windsorServiceLocator;

        public RepositoryBaseTest()
        {
            _container = new WindsorContainer();

            NoTrackingReleasePolicy policy = new NoTrackingReleasePolicy();

            _container.Kernel.ReleasePolicy = policy;

            //_container.Register(Component
            //        .For<IConnectionManager>()
            //        .ImplementedBy<ConnectionManager>()
            //        .Named("ConnectionManager")
            //        .LifeStyle.Transient);

            _container.Register(Component
                   .For<IObjectContextManager>()
                   .ImplementedBy<ObjectContextManager>()
                   .Named("ObjectContextManager")
                   .LifeStyle.Transient);

            _windsorServiceLocator = new WindsorServiceLocator(_container);

            ServiceLocator.SetLocatorProvider(() => _windsorServiceLocator);
        }
    }
}
