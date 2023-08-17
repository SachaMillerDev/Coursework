using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Coursework.Services
{
    public class MessageProcessorService
    {
        private readonly TextspeakService _textspeakService;

        public MessageProcessorService(TextspeakService textspeakService)
        {
            _textspeakService = textspeakService ?? throw new ArgumentNullException(nameof(textspeakService));
        }

        public MessageProcessorService()
        {
        }

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

        private SMSMessage ProcessSMSMessage(string messageId, string body)
        {
            var parts = body.Split(new[] { ' ' }, 2);
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid SMS format");
            }

            var sender = parts[0];
            var messageText = _textspeakService.ExpandTextspeak(parts[1]);

            return new SMSMessage(messageId, body, sender, messageText);
        }

        private EmailMessage ProcessEmailMessage(string messageId, string body)
        {
            int subjectIndex = body.IndexOf(" Subject: ");
            if (subjectIndex == -1)
            {
                throw new ArgumentException("Invalid email format");
            }

            var sender = body.Substring(0, subjectIndex);
            var remainingBody = body.Substring(subjectIndex + " Subject: ".Length);

            int messageIndex = remainingBody.IndexOf(". ");
            if (messageIndex == -1)
            {
                throw new ArgumentException("Invalid email format");
            }

            var subject = remainingBody.Substring(0, messageIndex);
            var messageText = HandleURLs(remainingBody.Substring(messageIndex + 2));

            return new EmailMessage(messageId, body, sender, subject, messageText);
        }

        private TweetMessage ProcessTweetMessage(string messageId, string body)
        {
            var parts = body.Split(new[] { ' ' }, 2);
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid Tweet format");
            }

            var twitterId = parts[0];
            var tweetText = _textspeakService.ExpandTextspeak(parts[1]);

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

            return new TweetMessage(messageId, body, twitterId, tweetText);
        }

        private string HandleURLs(string text)
        {
            return Regex.Replace(text, @"http[^\s]+", "<URL Quarantined>");
        }
    }
}
