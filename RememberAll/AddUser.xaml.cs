using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private Users u = new Users();
        public AddUser(Users u)
        {
            InitializeComponent();
            if (u != null)
                this.u = u;
            DataContext = this.u;
        }
        string imagePath = string.Empty;
        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                imagePath = openFile.FileName;
            }
        }
        RememberAllEntities db = new RememberAllEntities();
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (TbFN.Text == "") sb.Append("TextBox FirstName is empty\n");
            if (TbLN.Text == "") sb.Append("TextBox LastName is empty\n");
            if (TbEmail.Text == "") sb.Append("TextBox Email is empty\n");
            if (TbPhone.Text == "") sb.Append("TextBox Phone is empty\n");
            if (!IsValidEmail(TbEmail.Text)) sb.Append("TextBox Email is not valid\n");
            if (!IsValidPhone(TbPhone.Text)) sb.Append("TextBox Phone is not valid\n");
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }

            if (u.ID == 0)
            {
                if (imagePath != string.Empty)
                {
                    u.Image = File.ReadAllBytes(imagePath);
                }
                db.Users.Add(u);
                db.SaveChanges();
            }
            else if (u.ID != 0)
            {

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        bool IsValidEmail(string email)
        {
            bool result = false;
            try
            {
                result = new EmailAddressAttribute().IsValid(email);
            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }


        string Code()
        {
            string alf = "QWERASDZXC";
            string text = string.Empty;
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                text += alf[rnd.Next(0, alf.Length)];
            }
            return text;
        }
        string Hash(string password)
        {
            MD5 MD5 = new MD5CryptoServiceProvider();
            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] array = MD5.Hash;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString("X2"));
            }
            return sb.ToString();
        }
        bool IsValidPhone(string phone)
        {
            bool result = false;
            try
            {
                result = new PhoneAttribute().IsValid(phone);
            }
            catch (Exception)
            {

                result = false;
            }
            return result;
        }
    }
}
