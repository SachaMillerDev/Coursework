using System;
using System.Collections.Generic;
using System.Text;

namespace Coursework.Models
{
    public class TweetMessage : MessageBase
    {
        public string TwitterId { get; set; } // Twitter ID starting with "@"
        public string TweetText { get; set; }

        // Constructor
        public TweetMessage(string messageId, string body, string twitterId, string tweetText)
            : base(messageId, body)
        {
            TwitterId = twitterId;
            TweetText = tweetText;
        }
    }

}
