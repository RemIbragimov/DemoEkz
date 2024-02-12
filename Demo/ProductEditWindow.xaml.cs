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
    /// Логика взаимодействия для ProductEditWindow.xaml
    /// </summary>
    public partial class ProductEditWindow : Window
    {
        TradeEntities db;
        bool add_product;
        Product product;
        public ProductEditWindow(bool add, Product p)
        {
            InitializeComponent();
            db = new TradeEntities();
            add_product = add;
            product = p;
            if (!add)
            {
                Button_Add.Content = "Изменить товар";
                Window_Add.Title = "Изменение товара";
                Text_Art.Text = product.ProductArticleNumber;
                Text_Name.Text = product.ProductName;
                Text_Des.Text = product.ProductDescription;
                Text_Cat.Text = product.ProductCategory;
                Text_Photo.Text = "";
                Text_Manu.Text = product.ProductManufacturer;
                Text_Price.Text = product.ProductCost.ToString();
                Text_Sale.Text = product.ProductDiscountAmount.ToString();
                Text_Count.Text = product.ProductQuantityInStock.ToString();
                Text_Status.Text = product.ProductStatus.ToString();
            }
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            if (add_product)
            {
                try
                {
                    db.Product.Where(w => w.ProductArticleNumber == Text_Art.Text).First();
                    MessageBox.Show("Товар с таким артиклем уже существует");
                }
                catch
                {
                    Product p = new Product()
                    {
                        ProductArticleNumber = Text_Art.Text,
                        ProductName = Text_Name.Text,
                        ProductDescription = Text_Des.Text,
                        ProductCategory = Text_Cat.Text,
                        ProductPhoto = new byte[2],
                        ProductManufacturer = Text_Manu.Text,
                        ProductCost = int.Parse(Text_Price.Text),
                        ProductDiscountAmount = (byte)(int.Parse(Text_Sale.Text)),
                        ProductQuantityInStock = int.Parse(Text_Count.Text),
                        ProductStatus = Text_Status.Text,
                    };
                    db.Product.Add(p);
                    db.SaveChanges();
                    MessageBox.Show("Товар добавлен.");
                }
            }
            else
            {
                Product p = db.Product.Where(w => w.ProductArticleNumber == Text_Art.Text).First();
                p.ProductArticleNumber = Text_Art.Text;
                p.ProductName = Text_Name.Text;
                p.ProductDescription = Text_Des.Text;
                p.ProductCategory = Text_Cat.Text;
                p.ProductPhoto = new byte[2];
                p.ProductManufacturer = Text_Manu.Text;
                p.ProductCost = decimal.Parse(Text_Price.Text);
                p.ProductDiscountAmount = (byte)(int.Parse(Text_Sale.Text));
                p.ProductQuantityInStock = int.Parse(Text_Count.Text);
                p.ProductStatus = Text_Status.Text;
                db.SaveChanges();
            }
        }

        private void Click_Back(object sender, RoutedEventArgs e)
        {
            ProductListWindow dw = new ProductListWindow();
            dw.Show();
            this.Close();
        }
    }
}
