using DemoExzam.DB;
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
using System.Windows.Shapes;

namespace DemoExzam.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditAgentWindow.xaml
    /// </summary>
    public partial class EditAgentWindow : Window
    {
        Product post_product;

        OpenFileDialog openFileDialog = new OpenFileDialog();
        public EditAgentWindow(Product product)
        {
            InitializeComponent();
            TypeProduct.ItemsSource = App.DemoDb.ProductType.ToList();
            TypeProduct.DisplayMemberPath = "Title";
            post_product = product;
            this.DataContext = post_product;
            //TypeProduct.SelectedIndex = Convert.ToInt32(post_product.ProductTypeID) - 1;
        }

        private void BEditLogo_Click(object sender, RoutedEventArgs e)
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

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var prod = App.DemoDb.Product.Where(x => x.ID == post_product.ID).FirstOrDefault();
                prod.Title = TitleTxt.Text;
                var idType = TypeProduct.SelectedItem;
                prod.ProductTypeID = ((ProductType)idType).ID;
                prod.ArticleNumber = ArticleNum.Text;
                prod.ProductionPersonCount = Convert.ToInt32(CountHumProd.Text);
                prod.ProductionWorkshopNumber = Convert.ToInt32(NumbProd.Text);
                prod.MinCostForAgent = Convert.ToInt32(MinCostFA.Text);
                prod.Image = openFileDialog.FileName;
                App.DemoDb.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }
    }
}
