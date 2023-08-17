using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coursework.Services
{
    public class TextspeakService
    {
        private Dictionary<string, string> _textspeakDictionary;

        public TextspeakService(string csvFilePath)
        {
            _textspeakDictionary = LoadTextspeakFromCSV(csvFilePath);
        }

        private Dictionary<string, string> LoadTextspeakFromCSV(string csvFilePath)
        {
            var dictionary = new Dictionary<string, string>();
            var lines = File.ReadAllLines(csvFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    dictionary[parts[0].Trim()] = parts[1].Trim();
                }
            }

            return dictionary;
        }

        public string ExpandTextspeak(string message)
        {
            return _textspeakDictionary.Aggregate(message, (current, pair) => current.Replace(pair.Key, $"<{pair.Value}>"));
        }
    }
}
