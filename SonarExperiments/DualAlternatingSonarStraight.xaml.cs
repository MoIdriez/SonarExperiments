using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using SonarExperiments.Helpers;

namespace SonarExperiments
{
    /// <summary>
    /// Interaction logic for DualAlternatingSonarStraight.xaml
    /// </summary>
    public partial class DualAlternatingSonarStraight : IUserControl
    {
        private readonly AlternatingSonarData _data = new AlternatingSonarData(Filepath);
        private static readonly string Filepath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\Trial 2 - Dual and Alternating Sonar Straight Line.txt";

        public DualAlternatingSonarStraight()
        {
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
            var doubleSamples = _data.Samples.Where(data => data.Status == AlternatingStatus.Double).ToList();
            DoublePlotView.Model = new PlotModel
            {
                Title = "Double Raw Data vs Expected Result",
                Series =
                {
                    PlotHelper.GetLineSeries("sonar1", doubleSamples.Select(data => data.Sonar1)),
                    PlotHelper.GetLineSeries("sonar4", doubleSamples.Select(data => data.Sonar4)),
                    PlotHelper.GetLineSeries("expected", doubleSamples.Select(data => GetExpected(data.Position)))
                }
            };

            var sonar1Samples = _data.Samples.Where(d => d.Status == AlternatingStatus.Right).ToList();
            var sonar4Samples = _data.Samples.Where(d => d.Status == AlternatingStatus.Left).ToList();
            SinglePlotView.Model = new PlotModel
            {
                Title = "Single Raw Data vs Expected Result",
                Series =
                {
                    PlotHelper.GetLineSeries("sonar1", sonar1Samples.Select(d => d.Sonar1)),
                    PlotHelper.GetLineSeries("sonar4", sonar4Samples.Select(d => d.Sonar4)),
                    PlotHelper.GetLineSeries("expected", sonar4Samples.Select(d => GetExpected(d.Position)))
                }
            };

            RunningPlotView.Model = new PlotModel
            {
                Title = "Running average (100)",
                Series =
                {
                    PlotHelper.GetRunningAverage("sonar1", sonar1Samples.Select(d => d.Sonar1), 100),
                    PlotHelper.GetRunningAverage("sonar4", sonar4Samples.Select(d => d.Sonar4), 100),
                    PlotHelper.GetRunningAverage("expected", sonar4Samples.Select(d => GetExpected(d.Position)), 100)
                }
            };
        }

        private void DisplayMainDataTable()
        {
            MainDataGrid.ItemsSource = _data.Samples.GroupBy(d => d.Position).Select(d => new
            {
                ID = d.Key,
                Expected = GetExpected(d.Key),
                DoubleSonar1 = Math.Round(d.Where(data => data.Status == AlternatingStatus.Double).Average(data => data.Sonar1), 2),
                DoubleSonar4 = Math.Round(d.Where(data => data.Status == AlternatingStatus.Double).Average(data => data.Sonar4), 2),
                Sonar1 = Math.Round(d.Where(data => data.Status == AlternatingStatus.Right).Average(data => data.Sonar1), 2),
                Sonar4 = Math.Round(d.Where(data => data.Status == AlternatingStatus.Left).Average(data => data.Sonar4), 2)
            });
        }

        private int GetExpected(int key)
        {
            return new Dictionary<int, int>
            {
                { 0, 400 },
                { 1, 350 },
                { 2, 300 },
                { 3, 250 },
                { 4, 200 },
                { 5, 150 },
                { 6, 100 },
                { 7, 90 },
                { 8, 80 },
                { 9, 70 },
                { 10, 60 },
                { 11, 50 },
                { 12, 40 },
                { 13, 30 },
                { 14, 20 },
                { 15, 10 }

            }.First(d => d.Key == key).Value;
        }
    }
}
