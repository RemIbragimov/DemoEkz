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

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        TradeEntities db = new TradeEntities();

        public ProductListWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = db.Product.ToList();
            List<Product> list = db.Product.ToList();
            List<String> manufactures = new List<string>();
            manufactures.Add("Все производители");
            foreach (Product product in list)
            {
                if (!manufactures.Contains(product.ProductManufacturer))
                {
                    manufactures.Add(product.ProductManufacturer.ToString());
                }
            }
            ComboBox_manu.ItemsSource = manufactures;
            ComboBox_manu.SelectedIndex = 0;
        }

        private void Click_del(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = (Product)dataGrid.SelectedItem;
                db.Product.Remove(p);
                db.SaveChanges();
                dataGrid.ItemsSource = db.Product.ToList();
            }
            catch { }
        }

        private void Click_add(object sender, RoutedEventArgs e)
        {
            ProductEditWindow apw = new ProductEditWindow(true, new Product());
            apw.Show();
            this.Close();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((bool)CheckBox_Edit.IsChecked)
            {
                ProductEditWindow apw = new ProductEditWindow(false, (Product)dataGrid.SelectedItem);
                apw.Show();
                this.Close();
            }
            else { }
        }

        private void ComboBox_manu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Product> products = db.Product.ToList();
            if (ComboBox_manu.SelectedValue != "Все производители")
            {
                products = products.Where(w => w.ProductManufacturer == ComboBox_manu.SelectedValue).ToList();
            }
            else
            {
                products = products.ToList();
            }
            try
            {
                if (ComboBox_Sort.SelectedIndex == 0)
                {
                    products = products.OrderBy(w => w.ProductCost).ToList();
                }
                else if (ComboBox_Sort.SelectedIndex == 1)
                {
                    products = products.OrderByDescending(w => w.ProductCost).ToList();
                }
            }
            catch { }
            dataGrid.ItemsSource = products;
        }

        private void ComboBox_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Sort.SelectedIndex == 0)
            {
                dataGrid.ItemsSource = db.Product.OrderBy(w => w.ProductCost).ToList();
            }
            else
            {
                dataGrid.ItemsSource = db.Product.OrderByDescending(w => w.ProductCost).ToList();
            }
        }

        private void Text_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> products = db.Product.Where(w => w.ProductName.Contains(Text_Search.Text)).ToList();
            try
            {
                if (ComboBox_Sort.SelectedIndex == 0)
                {
                    products = products.OrderBy(w => w.ProductCost).ToList();
                }
                else if (ComboBox_Sort.SelectedIndex == 1)
                {
                    products = products.OrderByDescending(w => w.ProductCost).ToList();
                }
            }
            catch { }
            if (ComboBox_manu.SelectedValue != "Все производители")
            {
                products = products.Where(w => w.ProductManufacturer == ComboBox_manu.SelectedValue).ToList();
            }
            dataGrid.ItemsSource = products;
        }

        private void CheckBox_Edit_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
