using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecommerce.core
{
    public class Message
    {
        public enum MessageType
        {
            Error,
            Warning,
            Information
        }

        public string Code { get; set; }
        public MessageType Type { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public Message()
        {
        }

        public Message(string message)
        {
            Type = MessageType.Error;
            Value = message;
        }
    }
}
