using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace RememberAll
{
    /// <summary>
    /// Логика взаимодействия для Documents.xaml
    /// </summary>
    public partial class Documents : System.Windows.Window
    {

        List<string> Products = new List<string>()
        {
            "Apple","Juice","Ice","Pineapple","Banana","Potato",
            "Eggs","Bottle Water"
        };
        public Documents()
        {
            InitializeComponent();
            RememberAllEntities db = new RememberAllEntities();
            ProductListView.ItemsSource = db.Users.ToList();
        }

        List<Product> products = new List<Product>();
        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(openFile.FileName);
                Excel.Worksheet worksheet = workbook.Sheets[1];

                int rows = worksheet.UsedRange.Rows.Count;
                //int columns = worksheet.UsedRange.Columns.Count;

                for (int i = 1; i <= rows; i++)
                {
                    //for (int j = 1; j <= columns; j++)
                    //{

                    //}
                    Product product = new Product
                    {
                        Count = ((Range)worksheet.Cells[i, 1]).Value.ToString(),
                        Name = ((Range)worksheet.Cells[i, 2]).Value.ToString(),
                    };
                    products.Add(product);

                }
            }
            foreach (var item in products)
            {
                MessageBox.Show($"{item.Name}—{item.Count}");
            }
        }
        class Product
        {
            public string Count { get; set; }
            public string Name { get; set; }
        }
        private void MenuItemExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemPdf_Click(object sender, RoutedEventArgs e)
        {

        }



        private void BagListView_Drop(object sender, DragEventArgs e)
        {
            Users user = (Users)e.Data.GetData(e.Data.GetFormats()[0]);
            foreach (Users item in BagListView.Items)
            {
                if (user == item)
                {
                    BagListView.Items.Remove(item);
                    item.Count += 1;
                    break;
                }
            }
            BagListView.Items.Add(user);
        }



        private void ProductListView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedItem != null)
            {
                DragDrop.DoDragDrop(listView, listView.SelectedItem as Users, DragDropEffects.Copy);
            }
        }
    }
}
