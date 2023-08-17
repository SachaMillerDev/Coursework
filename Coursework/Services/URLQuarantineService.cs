using System.Collections.Generic;

namespace Coursework.Services
{
    public class URLQuarantineService
    {
        private List<string> _quarantinedUrls = new List<string>();

        public void AddToQuarantine(string url)
        {
            _quarantinedUrls.Add(url);
        }

        public List<string> GetQuarantinedUrls()
        {
            return _quarantinedUrls;
        }
    }
}
