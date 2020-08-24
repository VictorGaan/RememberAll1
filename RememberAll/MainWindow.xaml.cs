using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RememberAll
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RememberAllEntities db = new RememberAllEntities();
        public MainWindow()
        {
            InitializeComponent();
            UsersListView.ItemsSource = db.Users.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddUser(null).Show();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var user = UsersListView.SelectedItem as Users;
            new AddUser(user).Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var user = UsersListView.SelectedItem as Users;
            if (user==null||user.Image==null)
            {
                MessageBox.Show("Error");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "img";
            saveFileDialog.Filter="Jpg format (*.jpg)|*.jpg";  
            if (saveFileDialog.ShowDialog()==true)
            {
                FileStream fileStream = new FileStream(saveFileDialog.FileName,FileMode.OpenOrCreate);
                fileStream.Write(user.Image,0,user.Image.Length);
            }
        }
    }
}
