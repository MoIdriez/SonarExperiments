using System;
using System.IO;

namespace SonarWorks
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles("D:\\Git\\Data\\Experiments\\20161117\\", "data.txt", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                new SplitDataFiles(file).Run();
            }
        }
    }
}
