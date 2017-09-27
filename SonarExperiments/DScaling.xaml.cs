using System;
using System.Linq;
using System.Windows;
using OxyPlot;
using SonarExperiments.Helpers;

namespace SonarExperiments
{
    /// <summary>
    /// Interaction logic for DScaling.xaml
    /// </summary>
    public partial class DScaling : IUserControl
    {
        private readonly DScalingData _data;
        public DScaling(int wx, string dirLocations)
        {
            _data = new DScalingData(wx, dirLocations);
            InitializeComponent();
        }

        public ISonarData GetRawData()
        {
            return _data;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayMainDataTable();
            DisplayPlots();
        }

        private void DisplayPlots()
        {
            TopPlotView.Model = new PlotModel {Title = "EZ1 Raw Data vs Ground Truth"};

            int counter = 0;
            foreach (var set in _data.EZ1)
            {
                var plot = PlotHelper.GetLineSeries(Convert.ToString(counter++), set.Samples.Select(s => s.Sonar));
                plot.Color.ChangeIntensity(-90);
                TopPlotView.Model.Series.Add(plot);
            }

            MidPlotView.Model = new PlotModel {Title = "EZBrown Raw Data vs Ground Truth"};
            counter = 0;
            foreach (var set in _data.EZBrown)
            {
                var plot = PlotHelper.GetLineSeries(Convert.ToString(counter++), set.Samples.Select(s => s.Sonar));
                MidPlotView.Model.Series.Add(plot);
            }

            MidPlotView.Model.Series.Add(PlotHelper.GetLineSeries("gnd", _data.GroundTruth.Samples.Select(s => s.Sonar)));
            TopPlotView.Model.Series.Add(PlotHelper.GetLineSeries("gnd", _data.GroundTruth.Samples.Select(s => s.Sonar)));
        }

        private void DisplayMainDataTable() {}
    }
}
