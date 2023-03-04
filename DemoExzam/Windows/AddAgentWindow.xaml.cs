using DemoExzam.DB;
using DemoExzam.View;
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

namespace DemoExzam.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        public AddAgentWindow()
        {
            InitializeComponent();
            TypeProduct.ItemsSource = App.DemoDb.ProductType.ToList();
            TypeProduct.DisplayMemberPath = "Title";
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            Product prod = new Product();
            {
                try
                {
                    prod.Title = TitleTxt.Text;
                    var idType = TypeProduct.SelectedItem;
                    prod.ProductTypeID = ((ProductType)idType).ID;
                    prod.ArticleNumber = ArticleNum.Text;
                    prod.ProductionPersonCount = Convert.ToInt32(CountHumProd.Text);
                    prod.ProductionWorkshopNumber = Convert.ToInt32(NumbProd.Text);
                    prod.MinCostForAgent = Convert.ToInt32(MinCostFA.Text);
                    prod.Image = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
            App.DemoDb.Product.Add(prod);
            App.DemoDb.SaveChanges();
            this.Close();
        }

        private void BAddLogo_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();
                LogoFrame.Source = image;
            }
        }
    }
}
