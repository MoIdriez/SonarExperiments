using System;
using System.Linq;
using System.Windows;
using OxyPlot;
using SonarExperiments.Helpers;

namespace SonarExperiments
{
    /// <summary>
    /// Interaction logic for SonarScan.xaml
    /// </summary>
    public partial class SonarScan : IUserControl
    {
        private readonly SonarScanData _data;
        private readonly int _wx;
        private readonly int _wy;
        public SonarScan(int wx, int wy, string fileName)
        {
            _wx = wx;
            _wy = wy;
            _data = new SonarScanData(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\" + fileName);
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
            TopPlotView.Model = new PlotModel
            {
                Title = "Raw Data vs Expected Result Linear by millis x = y / cox(a)",
                Series =
                {
                    PlotHelper.GetLineSeries("sonar", _data.Samples.Select(d => d.Sonar)),
                    PlotHelper.GetLineSeries("expected", _data.Samples.Select(d => GetExpected(d.MilliSeconds)))
                }
            };

            MidPlotView.Model = new PlotModel
            {
                Title = "Running Average(100)",
                Series =
                {
                    PlotHelper.GetRunningAverage("sonar", _data.Samples.Select(d => d.Sonar), 100),
                    PlotHelper.GetRunningAverage("Expected", _data.Samples.Select(d => GetExpected(d.MilliSeconds)), 100)
                }
            };

            BottomPlotView.Model = new PlotModel
            {
                Title = "Gyro Data",
                Series =
                {
                    PlotHelper.GetRunningAverage("yaw", _data.Samples.Select(d => d.Yaw), 100),
                    PlotHelper.GetRunningAverage("pitch", _data.Samples.Select(d => d.Pitch), 100),
                    PlotHelper.GetRunningAverage("roll", _data.Samples.Select(d => d.Roll), 100)
                }
            };
        }

        private void DisplayMainDataTable() { }

        private double GetExpected(int millis)
        {
            var angle = GetAngle(millis);
            return MathHelper.GetHypotenuse(_wx, angle);
        }

        private double GetAngle(int millis)
        {
            var angle = MathHelper.ConvertRange(millis, _data.Samples.Min(s => s.MilliSeconds),
                _data.Samples.Max(s => s.MilliSeconds), 0, 90);
            return angle > 45 ? 90 - angle : angle;
        }
    }
}
