using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace PNE04
{
    public class DatasetCompare
    {
        private static readonly object lockAnalyzing = new object();

        public static async Task DownloadAndAnalyze(string url)
        {
            using HttpClient client = new HttpClient();
            var data = await client.GetStringAsync(url);

            //zámek, aby se to nemíchalo v konzoli
            lock (lockAnalyzing)
            {
                Console.WriteLine($"\nAnalyzing dataset from {url}");
                AnalyzeDataset(data);
            }
        }

        static void AnalyzeDataset(string data)
        {
            var size = System.Text.Encoding.UTF8.GetByteCount(data) / 1024.0;
            var lines = data.Split('\n');
            var rowCount = lines.Length;
            var columnCount = lines[0].Split(',').Length;
            var charCount = data.Length;

            var charFrequency = new Dictionary<char, int>();
            foreach (char c in data)
            {
                if (charFrequency.ContainsKey(c))
                {
                    charFrequency[c]++;
                }
                else
                {
                    charFrequency[c] = 1;
                }
            }

            Console.WriteLine($"Velikost: {size} kB, Řádků: {rowCount}, Sloupců: {columnCount}, Znaků: {charCount}");
            foreach (var entry in charFrequency)
            {
                Console.WriteLine($"Znak '{entry.Key}': {entry.Value} výskytů");
            }
        }
    }
    
}
