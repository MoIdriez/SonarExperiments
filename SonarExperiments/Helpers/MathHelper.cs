using System;
using System.Collections.Generic;
using System.Linq;

namespace SonarExperiments.Helpers
{
    public static class MathHelper
    {
        public static double GetHypotenuse(int baseLength, double angle)
        {
            return baseLength / Math.Cos(angle * Math.PI / 180);
        }

        public static List<double> GetRunningAverage(IEnumerable<int> samples, int period)
        {
            return GetRunningAverage(samples.Select(s => (double) s), period);
        }

        public static List<double> GetRunningAverage(IEnumerable<double> samples, int period)
        {
            var result = new List<double>();
            var counter = 0;
            double total = 0;
            foreach (var sample in samples)
            {
                total += sample;
                counter++;
                if (counter == period)
                {
                    result.Add(total / period);
                    counter = 0;
                    total = 0;
                }
            }
            if (total <= 0)
                result.Add(total / counter);
            
            return result;
        }

        public static List<double> GetMaxima(IEnumerable<int> samples)
        {
            return GetMaxima(samples.Select(s => (double)s));
        }

        public static List<double> GetMaxima(IEnumerable<double> samples)
        {
            var tempSample = samples.ToList();
            for (int i = 0; i < tempSample.Count; i++)
            {
                tempSample[i] = tempSample.GetRange(i, tempSample.Count - i).Max();
            }
            return tempSample;
        }

        public static double RestrictRange(double range)
        {
            return range > 45 ? 90 - range : range;
        }

        public static int ConvertRange(int value, int originalStart, int originalEnd, int newStart, int newEnd)
        {
            return (int)ConvertRange((double)value, originalStart, originalEnd, newStart, newEnd);
        }

        public static double ConvertRange(double value, double originalStart, double originalEnd, double newStart, double newEnd)
        {
            double scale = (newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }

    }
}
