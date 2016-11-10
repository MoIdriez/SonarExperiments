using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace SonarExperiments.Helpers
{
    public static class PlotHelper
    {
        public static LineSeries GetLineSeries(string identifier, IEnumerable<int> samples)
        {
            var lineSeries = new LineSeries
            {
                Title = identifier
            };
            var counter = 0;
            foreach (var sample in samples)
            {
                lineSeries.Points.Add(new DataPoint(counter, sample));
                counter++;
            }
            return lineSeries;
        }

        public static LineSeries GetRunningAverage(string identifier, IEnumerable<int> samples, int period)
        {
            var lineSeries = new LineSeries
            {
                Title = identifier
            };

            var runningSamples = MathHelper.GetRunningAverage(samples, period);
            for (int i = 0; i < runningSamples.Count; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, runningSamples[i]));
            }

            return lineSeries;
        }

        public static LineSeries GetMaxima(string identifier, IEnumerable<int> samples)
        {
            var lineSeries = new LineSeries
            {
                Title = identifier
            };

            var maximaSamples = MathHelper.GetMaxima(samples);
            for (int i = 0; i < maximaSamples.Count; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, maximaSamples[i]));
            }

            return lineSeries;
        }
    }
}
