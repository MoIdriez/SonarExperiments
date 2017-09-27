using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using SonarExperiments.Helpers;

namespace SonarExperiments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ListCollectionView lcv = new ListCollectionView(_experiments);
            lcv.GroupDescriptions?.Add(new PropertyGroupDescription("Category"));
            ExperimentCombBox2.ItemsSource = lcv;
        }

        private void ExperimentCombBox2_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ExperimentCombBox2.SelectedIndex)
            {
                case 0:
                    ChangeView(new DualSonarStraight());
                    break;
                case 1:
                    ChangeView(new DualAlternatingSonarStraight());
                    break;
                case 2:
                    ChangeView(new SonarCorner(100, 100, "Trial 3 - Single Sonar (EZ1) 1m Corner.txt"));
                    break;
                case 3:
                    ChangeView(new SonarCorner(200, 200, "Trial 9 - Single Sonar (EZ1) 2m Corner.txt"));
                    break;
                case 4:
                    ChangeView(new SonarCorner(300, 300, "Trial 11 - Single Sonar (EZ1) 3m Corner.txt"));
                    break;
                case 7:
                    ChangeView(new SonarCorner(100, 100, "Trial 4 - Single Sonar (EZ1) 1m Corner with Elivation.txt"));
                    break;
                case 8:
                    ChangeView(new SonarCorner(200, 200, "Trial 10 - Single Sonar (EZ1) 2m Corner with Elivation.txt"));
                    break;
                case 9:
                    ChangeView(new SonarCorner(300, 300, "Trial 12 - Single Sonar (EZ1) 3m Corner with Elivation.txt"));
                    break;
                case 12:
                    ChangeView(new SonarScan(100, 100, "Trial 13 - Single Sonar (EZ1) 1m Corner Scan 0 till 90.txt"));
                    break;
                case 13:
                    ChangeView(new SonarScan(100, 100, "Trial 14 - Single Sonar (EZ1) 1m Corner Scan 90 till 0.txt"));
                    break;
                case 14:
                    ChangeView(new SonarScan(200, 200, "Trial 15 - Single Sonar (EZ1) 2m Corner Scan 0 till 90.txt"));
                    break;
                case 15:
                    ChangeView(new SonarScan(200, 200, "Trial 16 - Single Sonar (EZ1) 2m Corner Scan 90 till 0.txt"));
                    break;
                case 16:
                    ChangeView(new SonarScan(300, 300, "Trial 21 - Single Sonar (EZ1) 3m Corner Scan 0 till 90.txt"));
                    break;
                case 21:
                    ChangeView(new DScaling(40, "D:\\Git\\Data\\Experiments\\20161117\\1 - Scaling\\D"));
                    break;
                case 22:
                    ChangeView(new DScaling(80, "D:\\Git\\Data\\Experiments\\20161117\\1 - Scaling\\D2"));
                    break;
                case 23:
                    ChangeView(new DScaling(120, "D:\\Git\\Data\\Experiments\\20161117\\1 - Scaling\\D3"));
                    break;
                default:
                    MainGrid.Children.Clear();
                    break;
                
            }


        }

        public void ChangeView(IUserControl userControl)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add((UserControl) userControl);

            //var rawData = userControl.GetRawData();
            //ExperimentHeader.Content = string.Join(Environment.NewLine, rawData.HeaderInfo());
            //ExperimentBody.Content = string.Join(Environment.NewLine, rawData.BodyInfo());
        }

        #region ExperimentData

        public class Item
        {
            public string Name { get; set; }
            public string Category { get; set; }
        }
        private readonly List<Item> _experiments = new List<Item>
        {
            new Item { Name = "Dual Straight Line", Category = "Straight Line" },
            new Item { Name = "Alternating Straight Line", Category = "Straight Line" },
            new Item { Name = "1m Static Corner", Category = "Static Corner" },
            new Item { Name = "2m Static Corner", Category = "Static Corner" },
            new Item { Name = "3m Static Corner", Category = "Static Corner" },
            new Item { Name = "1m2m Static Corner", Category = "Static Corner" },
            new Item { Name = "2m1m Static Corner", Category = "Static Corner" },
            new Item { Name = "1m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "2m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "3m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "1m2m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "2m1m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "1m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "1m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "2m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "2m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "3m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "1m2m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "1m2m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "2m1m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "2m1m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "d scalling", Category = "D Scaling" },
            new Item { Name = "d2 scalling", Category = "D Scaling" },
            new Item { Name = "d3 scalling", Category = "D Scaling" }
        };
        #endregion

    }
}
