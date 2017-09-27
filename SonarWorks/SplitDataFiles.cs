using System.Collections.Generic;
using System.IO;

namespace SonarWorks
{
    public class SplitDataFiles
    {
        private readonly string _fileLocation;
        public SplitDataFiles(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

        public void Run()
        {
            var list = Split();
            int counter = 0;
            foreach (var l in list)
            {
                File.WriteAllLines(_fileLocation + "\\" + counter + ".txt", l);
                counter++;
            }
        }

        public List<List<string>> Return()
        {
            return Split();
        }

        private List<List<string>> Split()
        {
            var list = new List<List<string>>();
            string line;
            var lines = new List<string>();
            StreamReader file = new StreamReader(_fileLocation);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Equals("-1,-1"))
                {
                    list.Add(lines);
                    lines = new List<string>();
                }
                else
                {
                    lines.Add(line);
                }

            }
            return list;
        }
    }
}
