using System.Collections.Generic;
using System.Linq;
using SonarWorks;

namespace SonarExperiments.Helpers
{
    public class DScalingData : ISonarData
    {
        private readonly int _wx;
        public DScalingData(int wx, string dirLocation)
        {
            _wx = wx;
            var ez1List = new SplitDataFiles(dirLocation + "\\EZ1\\data.txt").Return();
            var ezbrownList = new SplitDataFiles(dirLocation + "\\EZBrown\\data.txt").Return();

            EZ1 = ez1List.Select(l => new ScalingSet(l)).ToList();
            EZBrown = ezbrownList.Select(l => new ScalingSet(l)).ToList();
            GroundTruth = GetGroundTruth(EZ1.First());
        }

        public List<ScalingSet> EZ1 { get; }
        public List<ScalingSet> EZBrown { get; }
        public ScalingSet GroundTruth { get; }
        public ScalingSet GetGroundTruth(ScalingSet set)
        {
            return new ScalingSet(
                set.Samples.Select(
                    s => new ScalingSample(
                        s.Millis, 
                        MathHelper.GetHypotenuse(_wx, MathHelper.RestrictRange(MathHelper.ConvertRange(
                            s.Millis,
                            set.Samples.Min(ss => ss.Millis), 
                            set.Samples.Max(ss => ss.Millis),
                            0,
                            90))))).ToList());
        }

        public List<string> HeaderInfo()
        {
            return new List<string>();
        }

        public List<string> BodyInfo()
        {
            return new List<string>();
        }
    }

    

    public class ScalingSet
    {
        public ScalingSet(List<ScalingSample> samples)
        {
            Samples = samples;
        }
        public ScalingSet(List<string> lines)
        {
            Samples = lines.Select(l => new ScalingSample(l)).ToList();
        }

        public List<ScalingSample> Samples { get; }
    }

    public class ScalingSample
    {
        public ScalingSample(double millis, double sonar)
        {
            Millis = millis;
            Sonar = sonar;
        }
        public ScalingSample(string line)
        {
            var split = line.Split(',');
            Millis = double.Parse(split[0]);
            Sonar = double.Parse(split[1]);
        }
        public double Millis { get; set; }
        public double Sonar { get; set; }
    }
}
