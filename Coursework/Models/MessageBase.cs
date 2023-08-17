using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Models
{
    public abstract class MessageBase
    {
        public string MessageId { get; set; }
        public string Body { get; set; }

        // Constructor
        public MessageBase(string messageId, string body)
        {
            MessageId = messageId;
            Body = body;
        }
    }

}
