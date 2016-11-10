using System.Collections.Generic;
using System.Linq;

namespace SonarExperiments.Helpers
{
    public class AlternatingSonarData : ISonarData
    {
        public AlternatingSonarData(string filepath)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(',');
                if (!line.Contains("%") && !new[]  {0, 2, 4}.Contains(int.Parse(split[2])))
                {
                    Samples.Add(new AlternatingSamples
                    {
                        CountDownTimer = int.Parse(split[0]),
                        Status = (AlternatingStatus) int.Parse(split[2]),
                        Position = int.Parse(split[3]),
                        Sonar1 = int.Parse(split[4]),
                        Sonar4 = int.Parse(split[5]),
                        AccX = double.Parse(split[6]),
                        AccY = double.Parse(split[7]),
                        AccZ = double.Parse(split[8]),
                        Yaw = double.Parse(split[9]),
                        Pitch = double.Parse(split[10]),
                        Roll = double.Parse(split[11]),
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
        public List<AlternatingSamples> Samples = new List<AlternatingSamples>();

        public List<string> HeaderInfo()
        {
            return HeaderLines;
        }

        public List<string> BodyInfo()
        {
            return BodyLines;
        }
    }


    public class AlternatingSamples
    {
        public int CountDownTimer { get; set; }
        public AlternatingStatus Status { get; set; }
        public int Position { get; set; }
        public int Sonar1 { get; set; }
        public int Sonar4 { get; set; }
        public double AccX { get; set; }
        public double AccY { get; set; }
        public double AccZ { get; set; }
        public double Yaw { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
    }

    public enum AlternatingStatus { Double = 1, Left = 3, Right = 5 }
}
