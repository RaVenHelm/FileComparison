using System.Linq;
using System.IO;
using System.Collections.Generic;

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
            var lines1 = File.ReadAllLines(@"S:\Ty\tiffs.txt");
            var lines2 = File.ReadAllLines(@"S:\Ty\files3.txt");
            var fileNamesFound = from line in lines2
                                 select Path.GetFileNameWithoutExtension(line);
            var results = new List<string>();
            foreach (var line in lines1)
            {
                var fileName = Path.GetFileNameWithoutExtension(line);
                if (!fileNamesFound.Contains(fileName))
                {
                    results.Add(line);
                }
            }

            File.WriteAllLines(@"S:\Ty\file_diffs.txt", results);
        }
    }
}
