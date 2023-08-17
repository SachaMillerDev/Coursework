using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Models
{
    public class SMSMessage : MessageBase
    {
        public string Sender { get; set; } // International phone number
        public string MessageText { get; set; }

        // Constructor
        public SMSMessage(string messageId, string body, string sender, string messageText)
            : base(messageId, body)
        {
            Sender = sender;
            MessageText = messageText;
        }
    }

}
