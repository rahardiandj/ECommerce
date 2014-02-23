using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using ecommerce.core;
using System.Configuration;

namespace ecommerce.datamodel
{

    public class ObjectContextManager : IObjectContextManager
    {
        private ObjectContext _objectContext;
        private string _containerName = ConfigurationManager.ConnectionStrings[2].Name;//"metadata=res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl;provider=System.Data.SqlServerCe.3.5;provider connection string=&quot;Data Source=|DataDirectory|\\db_commerce.sdf&quot;";
        //ConfigurationManager.ConnectionStrings["ecommerce.datamodel.Properties.Settings.Database1ConnectionString"].ConnectionString;//"db_commerceEntities";//ConfigurationManager.ConnectionStrings["db_commerceEntities"].Name;

        /// <summary>
        /// Name of the container that will be used by Object Context.
        /// </summary>
        public string ContainerName
        {
            get { return _containerName; }
            set { _containerName = value; }
        }

        /// <summary>
        /// Return instance of Object Context.
        /// </summary>
        /// <returns></returns>
        public ObjectContext ObjectContext
        {
            get { return _objectContext ?? CreateObjectContext(); }
        }

        private ObjectContext CreateObjectContext()
        {
            _objectContext = new ObjectContext("name=" + ContainerName);
            _objectContext.DefaultContainerName = ContainerName;
            _objectContext.ContextOptions.LazyLoadingEnabled = false;

            return _objectContext;
        }
    }
}
