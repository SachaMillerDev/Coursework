using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Coursework.Services
{
    public class TextspeakService
    {
        private Dictionary<string, string> _textspeakDictionary = new Dictionary<string, string>
        {
            { "ROFL", "Rolls on the floor laughing" },
            { "BRB", "Be right back" },
            { "LOL", "Laugh out loud" }
             // ... add more as needed
        };


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
            foreach (var abbreviation in _textspeakDictionary.Keys)
            {
                message = message.Replace(abbreviation, $"{abbreviation} <{_textspeakDictionary[abbreviation]}>");
            }
            return message;
        }
    }
}
