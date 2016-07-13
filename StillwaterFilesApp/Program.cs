using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;


namespace StillwaterFilesApp
{
    class Program
    {
        // We'll just start and make this as simple as possible
        // TODO: Add features like:
        //       * Robust Logging
        //       * Macros
        //       * JSON/XML Config Support
        static void Main(string[] args)
        {
            // TODO: Make into a config file
            // TODO: break this functionality into it's own class
            //var lines1 = File.ReadAllLines(@"S:\Ty\tiffs.txt");
            //var lines2 = File.ReadAllLines(@"S:\Ty\files3.txt");
            //var fileNamesFound = from line in lines2
            //                     select Path.GetFileNameWithoutExtension(line);
            //var results = new List<string>();
            //foreach (var line in lines1)
            //{
            //    var fileName = Path.GetFileNameWithoutExtension(line);
            //    if (!fileNamesFound.Contains(fileName))
            //    {
            //        results.Add(line);
            //    }
            //}

            //File.WriteAllLines(@"S:\Ty\file_diffs.txt", results);

            var jsonString = string.Join("", File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "config.json")));

            JObject json = JObject.Parse(jsonString);
            var config = JsonConvert.DeserializeObject<Config.Config>(json.ToString());
            var lines = new Dictionary<string, List<string>>();
            string[] paths = { Path.Combine(config.InDir, config.InFile1), Path.Combine(config.InDir, config.InFile2) };
            foreach (var path in paths)
            {
                lines.Add(path, new List<string>(File.ReadAllLines(path)));
            }

            var sorted = from pair in lines
                         orderby pair.Value.Count() descending
                         select pair;

            var fileNamesFound = from line in sorted.Last().Value
                                 select Path.GetFileNameWithoutExtension(line);

            var diffs = new List<string>();
            foreach (var line in sorted.First().Value)
            {
                var fileName = Path.GetFileNameWithoutExtension(line);
                if (!fileNamesFound.Contains(fileName))
                {
                    diffs.Add(line);
                }
            }

            File.WriteAllLines(Path.Combine(config.OutDir, config.OutFile), diffs);
        }
    }
}
