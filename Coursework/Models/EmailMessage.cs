using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Models
{
    public class EmailMessage : MessageBase
    {
        public string Sender { get; set; } // Email address
        public string Subject { get; set; }
        public string MessageText { get; set; }

        // Constructor
        public EmailMessage(string messageId, string body, string sender, string subject, string messageText)
            : base(messageId, body)
        {
            Sender = sender;
            Subject = subject;
            MessageText = messageText;
        }
    }

}
