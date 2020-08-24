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
    /// Логика взаимодействия для DragAndDrop.xaml
    /// </summary>
    public partial class DragAndDrop : Window
    {
        public DragAndDrop()
        {
            InitializeComponent();
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = e.Source as Image;
            DataObject data = new DataObject(DataFormats.Text,image.Source);
            DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            // drop from desktop
            //foreach (string pic in (string[])e.Data.GetData(DataFormats.FileDrop))
            //{
            //    Img.Source = new BitmapImage(new Uri(pic));
            //}

            //drop from another image control
            //Img.Source = (BitmapSource)e.Data.GetData(DataFormats.Text);
        }
    }
}
