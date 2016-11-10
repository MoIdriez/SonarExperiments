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
using SonarExperiments.Helpers;

namespace SonarExperiments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            new Item { Name = "1m2m Static Corner", Category = "Static Corner" },
            new Item { Name = "2m1m Static Corner", Category = "Static Corner" },
            new Item { Name = "3m Static Corner", Category = "Static Corner" },
            new Item { Name = "1m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "2m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "1m2m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "2m1m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "3m Static Corner Elv", Category = "Elivated Corner" },
            new Item { Name = "1m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "1m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "2m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "2m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "1m2m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "1m2m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "2m1m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "2m1m Scan Corner 90 - 0", Category = "Scan Corner" },
            new Item { Name = "3m Scan Corner 0 - 90", Category = "Scan Corner" },
            new Item { Name = "3m Scan Corner 90 - 0", Category = "Scan Corner" }
        };
        #endregion

    }
}
