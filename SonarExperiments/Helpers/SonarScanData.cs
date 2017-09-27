using System.Collections.Generic;

namespace SonarExperiments.Helpers
{
    public class SonarScanData : ISonarData
    {
        public SonarScanData(string filePath)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(',');
                if (!line.Contains("%") && split.Length == 8)
                {
                    Samples.Add(new ScanData
                    {
                        MilliSeconds = int.Parse(split[0]),
                        Sonar = int.Parse(split[1]),
                        AccX = double.Parse(split[2]),
                        AccY = double.Parse(split[3]),
                        AccZ = double.Parse(split[4]),
                        Yaw = double.Parse(split[5]),
                        Pitch = double.Parse(split[6]),
                        Roll = double.Parse(split[7])
                    });
                    BodyLines.Add(line);
                }
                else
                {
                    HeaderLines.Add(line);
                }
            }
            file.Close();

        }

        public List<string> HeaderLines = new List<string>();
        public List<string> BodyLines = new List<string>();
        public List<ScanData> Samples = new List<ScanData>();

        public List<string> HeaderInfo()
        {
            return HeaderLines;
        }

        public List<string> BodyInfo()
        {
            return BodyLines;
        }
    }

    public class ScanData
    {
        public int MilliSeconds { get; set; }
        public int Sonar { get; set; }
        public double AccX { get; set; }
        public double AccY { get; set; }
        public double AccZ { get; set; }
        public double Yaw { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
    }
}
