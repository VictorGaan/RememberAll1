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

namespace RememberAll
{
    /// <summary>
    /// Логика взаимодействия для Session6.xaml
    /// </summary>
    public partial class Session6 : Window
    {
        public Session6()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            RememberAllEntities db = new RememberAllEntities();
            var q = db.Orders.Where(x => x.EmergencyMaintenancesID != null)
                .GroupBy(x => x.Date.Year)
                .Select(x => x.FirstOrDefault());

            foreach (var item in q)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = "Department";
                column.Binding = new Binding("EmergencyMaintenances.Assets.DepartmentLocations.Departments.Name");
                EMDataGrid.Columns.Add(column);
                break;
            }
            foreach (var item in q)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = item.Date.Year + "—" + item.Date.Month;
                column.Binding = new Binding("Price");
                EMDataGrid.Columns.Add(column);
            }

            EMDataGrid.ItemsSource = q.ToList();
        }
    }
}
