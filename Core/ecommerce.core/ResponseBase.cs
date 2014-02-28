using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ecommerce.core
{
    public class ResponseBase
    {
        private Collection<Message> _messages;

        /// <summary>
        /// Collection of messages that returned by Service.
        /// </summary>
        public Collection<Message> Messages
        {
            get
            {
                return _messages ?? (_messages = new Collection<Message>());
            }
        }
    }
}
