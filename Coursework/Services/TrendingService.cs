using System.Collections.Generic;
using System.Linq;

namespace Coursework.Services
{
    public class TrendingService
    {
        private Dictionary<string, int> _hashtags = new Dictionary<string, int>();

        public void AddHashtag(string hashtag)
        {
            if (_hashtags.ContainsKey(hashtag))
            {
                _hashtags[hashtag]++;
            }
            else
            {
                _hashtags[hashtag] = 1;
            }
        }

        public List<string> GetTrendingHashtags(int topN = 10)
        {
            return _hashtags.OrderByDescending(pair => pair.Value).Take(topN).Select(pair => pair.Key).ToList();
        }
    }
}
