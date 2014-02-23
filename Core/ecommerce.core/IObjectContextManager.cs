using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace ecommerce.core
{
    public interface IObjectContextManager
    {
        /// <summary>
        /// Name of the container that will be used by Object Context.
        /// </summary>
        string ContainerName { get; set; }

        /// <summary>
        /// Return the Object Context managed by Manager.
        /// </summary>
        /// <returns>Object Context Instance</returns>
        ObjectContext ObjectContext { get; }
    }
}
