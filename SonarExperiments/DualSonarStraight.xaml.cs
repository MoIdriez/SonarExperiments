using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using SonarExperiments.Helpers;

namespace SonarExperiments
{
    /// <summary>
    /// Interaction logic for DualSonarStraight.xaml
    /// </summary>
    public partial class DualSonarStraight : IUserControl
    {
        private readonly StraightDualSonar _data = new StraightDualSonar(Filepath);
        private static readonly string Filepath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\Trial 1 - Dual Sonar Straight Line.txt";

        public DualSonarStraight()
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
            TopPlotView.Model = new PlotModel
            {
                Title = "Raw Data vs Expected Result",
                Series =
                {
                    PlotHelper.GetLineSeries("sonar1", _data.Samples.Select(d => d.Sonar1)),
                    PlotHelper.GetLineSeries("sonar4", _data.Samples.Select(d => d.Sonar4)),
                    PlotHelper.GetLineSeries("expected", _data.Samples.Select(d => GetExpected(d.Position)))
                }
            };

            MidPlotView.Model = new PlotModel
            {
                Title = "Running Average(100)",
                Series =
                {
                    PlotHelper.GetRunningAverage("sonar1", _data.Samples.Select(d => d.Sonar1), 100),
                    PlotHelper.GetRunningAverage("sonar4", _data.Samples.Select(d => d.Sonar4), 100),
                    PlotHelper.GetRunningAverage("Expected", _data.Samples.Select(d => GetExpected(d.Position)), 100)
                }
            };

            BottomPlotView.Model = new PlotModel
            {
                Title = "Maxima",
                Series =
                {
                    PlotHelper.GetMaxima("sonar1", _data.Samples.Select(d => d.Sonar1)),
                    PlotHelper.GetMaxima("sonar4", _data.Samples.Select(d => d.Sonar4)),
                    PlotHelper.GetMaxima("expected", _data.Samples.Select(d => GetExpected(d.Position)))

                }
            };
        }

        private void DisplayMainDataTable()
        {
            MainDataGrid.ItemsSource = _data.Samples.GroupBy(d => d.Position).Select(d => new
            {
                ID = d.Key,
                Expected = GetExpected(d.Key),
                Sonar1 = Math.Round(d.Average(data => data.Sonar1), 2),
                Sonar2 = Math.Round(d.Average(data => data.Sonar4), 2)
            });
        }

        private int GetExpected(int key)
        {
            return new Dictionary<int, int>
            {
                { 1, 400 },
                { 2, 350 },
                { 3, 300 },
                { 4, 250 },
                { 5, 200 },
                { 6, 150 },
                { 7, 100 },
                { 8, 90 },
                { 9, 80 },
                { 10, 70 },
                { 11, 60 },
                { 12, 50 },
                { 13, 40 },
                { 14, 30 },
                { 15, 20 },
                { 16, 10 }
                
            }.First(d => d.Key == key).Value;
        }
        
    }
}
