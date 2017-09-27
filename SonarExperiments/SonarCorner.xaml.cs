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
    /// Interaction logic for SonarCorner.xaml
    /// </summary>
    public partial class SonarCorner : IUserControl
    {
        private readonly SonarCornerData _data;
        private readonly int _wx;
        private readonly int _wy;

        public SonarCorner(int wx, int wy, string fileName)
        {
            _wx = wx;
            _wy = wy;
            _data = new SonarCornerData(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Data\\" + fileName);
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
                Title = "Raw Data vs Expected Result x = y / cox(a)",
                Series =
                {
                    PlotHelper.GetLineSeries("sonar", _data.Samples.Select(d => d.Sonar)),
                    PlotHelper.GetLineSeries("expected", _data.Samples.Select(d => GetExpected(d.Position)))
                }
            };

            MidPlotView.Model = new PlotModel
            {
                Title = "Running Average(100)",
                Series =
                {
                    PlotHelper.GetRunningAverage("sonar1", _data.Samples.Select(d => d.Sonar), 100),
                    PlotHelper.GetRunningAverage("Expected", _data.Samples.Select(d => GetExpected(d.Position)), 100)
                }
            };
        }

        private void DisplayMainDataTable()
        {
            MainDataGrid.ItemsSource = _data.Samples.GroupBy(d => d.Position).Select(d => new
            {
                ID = d.Key,
                Expected = GetExpected(d.Key),
                Sonar1 = Math.Round(d.Average(data => data.Sonar), 2)
            });
        }

        private double GetExpected(int key)
        {
            var angle = new Dictionary<int, double> { {0, 0}, {1, 20}, {2, 45}, {3, 25}, {4, 0} }.First(k => k.Key == key).Value;
            return MathHelper.GetHypotenuse(_wx, angle);
        }
    }
}
