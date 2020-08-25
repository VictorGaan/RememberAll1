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
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;

namespace RememberAll
{
    /// <summary>
    /// Логика взаимодействия для ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow()
        {
            InitializeComponent();
            Load();
        }

        RememberAllEntities db = new RememberAllEntities();
        private void Load()
        {
            ChartControl.ChartAreas.Add(new ChartArea("Chart Area"));
            ChartControl.Series.Add(new Series("First Series"));
            ChartControl.Series["First Series"].ChartArea = "Chart Area";
            ChartControl.Series["First Series"].ChartType = SeriesChartType.Pie;
            ChartControl.DataSource = db.Users.ToList();
            ChartControl.Series["First Series"].XValueMember = "LastName";
            ChartControl.Series["First Series"].XValueType = ChartValueType.String;
            ChartControl.Series["First Series"].YValueMembers = "ID";
            ChartControl.Series["First Series"].YValueType = ChartValueType.Int32;

            //var q = db.Users.Select(x => x.LastName);
            //var b = db.Users.Select(x=>x.ID);
            //ChartControl.Series["First Series"].Points.DataBindXY(q,b);
        }
    }
}
