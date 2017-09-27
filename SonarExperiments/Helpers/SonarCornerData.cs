using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarExperiments.Helpers
{
    public class SonarCornerData : ISonarData
    {
        public SonarCornerData(string filePath)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(',');
                if (!line.Contains("%") && int.Parse(split[1]) != 0)
                {
                    Samples.Add(new CornerSample
                    {
                        CountDownTimer = int.Parse(split[0]),
                        Position = int.Parse(split[2]),
                        Sonar = int.Parse(split[3]),
                        AccX = double.Parse(split[4]),
                        AccY = double.Parse(split[5]),
                        AccZ = double.Parse(split[6]),
                        Yaw = double.Parse(split[7]),
                        Pitch = double.Parse(split[8]),
                        Roll = double.Parse(split[9]),
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
        public List<CornerSample> Samples = new List<CornerSample>();

        public List<string> HeaderInfo()
        {
            return HeaderLines;
        }

        public List<string> BodyInfo()
        {
            return BodyLines;
        }
    }
    public class CornerSample
    {
        public int CountDownTimer { get; set; }
        public int Position { get; set; }
        public int Sonar { get; set; }
        public double AccX { get; set; }
        public double AccY { get; set; }
        public double AccZ { get; set; }
        public double Yaw { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
    }
}
