using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

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
            BagListView.Background = new SolidColorBrush(Properties.Settings.Default.MyColor);
            ProductListView.ItemsSource = db.Users.ToList();
        }
        RememberAllEntities db = new RememberAllEntities();

        List<Product> products = new List<Product>();
        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(openFile.FileName);
                Excel.Worksheet worksheet = app.Worksheets[1];

                int rows = worksheet.UsedRange.Rows.Count;
                //int columns = worksheet.UsedRange.Columns.Count;

                for (int i = 1; i <= rows; i++)
                {
                    //for (int j = 1; j <= columns; j++)
                    //{

                    //}
                    Product product = new Product
                    {
                        Count = ((Excel.Range)worksheet.Cells[i, 1]).Value.ToString(),
                        Name = ((Excel.Range)worksheet.Cells[i, 2]).Value.ToString(),
                    };
                    products.Add(product);

                }
                workbook.Close();
                app.Quit();
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
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "report";
            saveFile.Filter = "Excel files |*.xlsx";
            if (saveFile.ShowDialog() == true)
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Add();
                Excel.Worksheet worksheet = app.Worksheets[1];

                worksheet.Range["A1"].Value = "First Name";
                worksheet.Range["B1"].Value = "Last Name";
                worksheet.Range["C1"].Value = "Middle Name";

                int i = 1;
                foreach (var item in db.Users.ToList())
                {
                    i++;
                    worksheet.Range[$"A{i}"].Value = item.FirstName;
                    worksheet.Range[$"B{i}"].Value = item.LastName;
                    worksheet.Range[$"C{i}"].Value = item.MiddleName;
                }
                workbook.SaveAs(saveFile.FileName);
                workbook.Close();
                app.Quit();
            }
        }

        private void MenuItemWord_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "report";
            saveFile.Filter = "Docx files |*.docx";
            if (saveFile.ShowDialog() == true)
            {
                Word.Application app = new Word.Application();
                Word.Document doc = app.Documents.Add();
                Word.Paragraph paragraph = doc.Paragraphs.Add();
                paragraph.Range.Text = "Sample text\t tabulation\n" +
                    "New stroke\t tabulation\n";
                doc.SaveAs2(saveFile.FileName);
                doc.Close();
                app.Quit();
            }
        }

        private void MenuItemPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "report";
            saveFile.Filter = "Pdf files |*.pdf";
            if (saveFile.ShowDialog() == true)
            {
                Word.Application app = new Word.Application();
                Word.Document doc = app.Documents.Add();
                Word.Paragraph paragraph = doc.Paragraphs.Add();
                paragraph.Range.Text = "Sample text\t tabulation\n" +
                    "New stroke\t tabulation\n";
                doc.SaveAs2(saveFile.FileName, WdSaveFormat.wdFormatPDF);
                doc.Close();
                app.Quit();
            }
        }



        private void BagListView_Drop(object sender, System.Windows.DragEventArgs e)
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

        private void MenuItemColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var argb = colorDialog.Color;
                BagListView.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(argb.A, argb.R, argb.G, argb.B));
                Properties.Settings.Default.MyColor = System.Windows.Media.Color.FromArgb(argb.A, argb.R, argb.G, argb.B);
                Properties.Settings.Default.Save();
            }
        }

        private void MenuItemFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(folderBrowser.SelectedPath);
            }
        }

        private void MenuItemPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                TextBlock textBlock = new TextBlock
                {
                    Text = "sample text to print",
                    TextWrapping = TextWrapping.Wrap,
                };
                printDialog.PrintVisual(textBlock, "ListView");
            }

        }

        private void MenuItemRead_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemWrite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
