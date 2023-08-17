using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Services
{
    public class MessageProcessorService
    {
        public MessageBase ProcessMessage(string messageId, string body)
        {
            switch (messageId[0])
            {
                case 'S':
                    // Process SMS message
                    return ProcessSMSMessage(messageId, body);
                case 'E':
                    // Process Email message
                    return ProcessEmailMessage(messageId, body);
                case 'T':
                    // Process Tweet
                    return ProcessTweetMessage(messageId, body);
                default:
                    throw new ArgumentException("Invalid message type");
            }
        }

        private SMSMessage ProcessSMSMessage(string messageId, string body)
        {
            // Extract sender and message text from body
            return new SMSMessage(); // Add appropriate parameters if required
        }

        private EmailMessage ProcessEmailMessage(string messageId, string body)
        {
            // Extract sender, subject, and message text from body
            return new EmailMessage(); // Add appropriate parameters if required
        }

        private TweetMessage ProcessTweetMessage(string messageId, string body)
        {
            // Extract Twitter ID and tweet text from body
            return new TweetMessage(); // Add appropriate parameters if required
        }

    }

}
