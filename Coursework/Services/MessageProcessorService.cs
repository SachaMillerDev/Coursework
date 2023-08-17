using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Coursework.Services
{
    public class MessageProcessorService
    {

        public MessageBase ProcessMessage(string messageId, string body)
        {
            if (string.IsNullOrEmpty(messageId) || messageId.Length < 10)
            {
                throw new ArgumentException("Invalid message header format");
            }

            switch (messageId[0])
            {
                case 'S':
                    return ProcessSMSMessage(messageId, body);
                case 'E':
                    return ProcessEmailMessage(messageId, body);
                case 'T':
                    return ProcessTweetMessage(messageId, body);
                default:
                    throw new ArgumentException("Unsupported message type");
            }
        }

        private TextspeakService _textspeakService;

        public MessageProcessorService(TextspeakService textspeakService)
        {
            _textspeakService = textspeakService;
        }


        private SMSMessage ProcessSMSMessage(string messageId, string body)
        {
            var parts = body.Split(new[] { ' ' }, 2);
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid SMS format");
            }

            var sender = parts[0];
            var messageText = _textspeakService.ExpandTextspeak(parts[1]);

            return new SMSMessage
            {
                MessageId = messageId,
                Sender = sender,
                Text = messageText
            };
        }

        private EmailMessage ProcessEmailMessage(string messageId, string body)
        {
            var parts = body.Split(new[] { " Subject: " }, 2);
            var sender = parts[0];
            var subjectAndMessage = parts[1].Split(new[] { ". " }, 2);
            var subject = subjectAndMessage[0];
            var messageText = HandleURLs(subjectAndMessage[1]);

            return new EmailMessage
            {
                MessageId = messageId,
                Sender = sender,
                Subject = subject,
                Text = messageText
            };
        }

        private TweetMessage ProcessTweetMessage(string messageId, string body)
        {
            var parts = body.Split(new[] { ' ' }, 2);
            var twitterId = parts[0];
            var tweetText = ExpandTextspeak(parts[1]);

            // Extract hashtags and mentions
            var hashtags = new List<string>();
            var mentions = new List<string>();
            foreach (var word in tweetText.Split(' '))
            {
                if (word.StartsWith("#"))
                    hashtags.Add(word);
                else if (word.StartsWith("@"))
                    mentions.Add(word);
            }

            // TODO: Update the ViewModel's lists with these extracted hashtags and mentions

            return new TweetMessage
            {
                MessageId = messageId,
                TwitterId = twitterId,
                Text = tweetText
            };
        }


        private string ExpandTextspeak(string text)
        {
            foreach (var abbreviation in textspeakDictionary)
            {
                text = text.Replace(abbreviation.Key, abbreviation.Value);
            }
            return text;
        }

        private string HandleURLs(string text)
        {
            return Regex.Replace(text, @"http[^\s]+", "<URL Quarantined>");
        }
    }
}