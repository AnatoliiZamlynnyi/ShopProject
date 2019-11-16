using ShopCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ShopProject
{
    public partial class WindowProg : Window
    {
        EfContext context;
        Product product;
        static Category category;
        Supplier supplier;
        static DateTime dateStart;
        static DateTime dateStop;
        List<Product> tmProd;

        public User root { get; set; }
        public WindowProg(User us)
        {
            InitializeComponent();

            context = new EfContext();
            root = us;
            this.Title = "Магазин - Користувач: " + root.UserName + " (Для довідки натисніть F1)";
            dateStart = DateTime.Parse("01/01/2019");
            dateStop = DateTime.Parse(dateFirst.DisplayDate.ToString());
            dgProduct.ItemsSource = context.Products.ToList();
            dgCategory.ItemsSource = context.Categoryes.ToList();
            dgSupplier.ItemsSource = context.Suppliers.ToList();
            dgSaleProduct.ItemsSource = context.SaleProducts.ToList();
            dgUtilProduct.ItemsSource = context.UtilProducts.ToList();
            if (root.Access != 1)
            {
                addProduct.IsEnabled = false;
            }
            if (context.Categoryes.Count() > 0)
                categoryCount.Content = "Категорії товарів: " + context.Categoryes.Count();
            if (context.Suppliers.Count() > 0)
                supplierCount.Content = "Постачальники: " + context.Suppliers.Count();
            if (context.Products.Count() > 0)
                summ.Content = context.Products.Count() + " одиниць товару на загальну суму: " + context.Products.Sum(x => x.Summa) + "грн.";
            if (context.UtilProducts.Count() > 0)
                util.Content = "На суму: " + context.UtilProducts.Sum(x => x.Summa) + "грн.";
            if (context.SaleProducts.Count() > 0)
                sale.Content = "На суму: " + context.SaleProducts.Sum(x => x.Summa) + "грн.";
        }

        private void UploadProduct_Click(object sender, RoutedEventArgs e)
        {
            dgProduct.ItemsSource = context.Products.ToList();
            summDate.Content = "";
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProduct = new AddProduct(root);
            addProduct.ShowDialog();
            context.SaveChanges();
            dgProduct.ItemsSource = context.Products.ToList();
        }

        private void dgProduct_MouseUp(object sender, MouseButtonEventArgs e)
        {
            product = new Product();
            product = dgProduct.SelectedItem as Product;
        }

        private void Product_KeyUp(object sender, KeyEventArgs e)
        {
            if (root.Access == 1)
            {
                if (e.Key == Key.Enter)
                {
                    try
                    {
                        context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        dgProduct.ItemsSource = context.Products.ToList();
                    }
                    catch { }
                }
                if (e.Key == (Key.RightCtrl | Key.Delete))
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    dgProduct.ItemsSource = context.Products.ToList();
                }
            }
        }

        private void UploadCategory_Click(object sender, RoutedEventArgs e)
        {
            dgCategory.ItemsSource = context.Categoryes.ToList();
            categoryCount.Content = "Категорії товарів: " + context.Categoryes.Count();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategory = new AddCategory(root);
            addCategory.ShowDialog();
            dgCategory.ItemsSource = context.Categoryes.ToList();
            categoryCount.Content = "Категорії товарів: " + context.Categoryes.Count();
        }
        private void dgCategory_MouseUp(object sender, MouseButtonEventArgs e)
        {
            category = new Category();
            category = dgCategory.SelectedItem as Category;
        }
        private void Category_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    dgCategory.ItemsSource = context.Categoryes.ToList();
                }
                catch { }
            }
            if (e.Key == (Key.RightCtrl | Key.Delete))
            {
                context.Categoryes.Remove(category);
                context.SaveChanges();
                dgCategory.ItemsSource = context.Categoryes.ToList();
                categoryCount.Content = "Категорії товарів: " + context.Categoryes.Count();
            }
        }

        private void UploadSupplier_Click(object sender, RoutedEventArgs e)
        {
            dgSupplier.ItemsSource = context.Suppliers.ToList();
            supplierCount.Content = "Постачальники: " + context.Suppliers.Count();
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var addSupplier = new AddSupplier(root);
            addSupplier.ShowDialog();
            dgSupplier.ItemsSource = context.Suppliers.ToList();
            supplierCount.Content = "Постачальники: " + context.Suppliers.Count();
        }
        private void dgSupplier_MouseUp(object sender, MouseButtonEventArgs e)
        {
            supplier = new Supplier();
            supplier = dgSupplier.SelectedItem as Supplier;
        }
        private void Supplier_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    context.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    dgSupplier.ItemsSource = context.Suppliers.ToList();
                }
                catch { }
            }
            if (e.Key == (Key.RightCtrl | Key.Delete))
            {
                context.Suppliers.Remove(supplier);
                context.SaveChanges();
                dgSupplier.ItemsSource = context.Suppliers.ToList();
                supplierCount.Content = "Постачальники: " + context.Suppliers.Count();
            }
        }

        private void ButtonDataSet_Click(object sender, RoutedEventArgs e)
        {
            tmProd = new List<Product>();
            tmProd = context.Products.Where(x => x.Date >= dateStart && x.Date <= dateStop).ToList();
            dgProduct.ItemsSource = tmProd.ToList();
            summDate.Content = tmProd.Count() + " одиниць товару на суму: " + tmProd.Sum(x => x.Summa) + "грн.";
        }

        private void DateStart_Changet(object sender, SelectionChangedEventArgs e)
        {
            if (dateFirst.SelectedDate <= System.DateTime.Now && dateFirst.SelectedDate >= System.DateTime.MinValue)
                dateStart = DateTime.Parse(dateFirst.SelectedDate.ToString());
            else
                dateStart = System.DateTime.MinValue;
        }

        private void DateStop_Changet(object sender, SelectionChangedEventArgs e)
        {
            if (dateSecond.SelectedDate <= System.DateTime.Now && dateSecond.SelectedDate >= System.DateTime.MinValue)
                dateStop = DateTime.Parse(dateSecond.SelectedDate.ToString());
            else
                dateStop = System.DateTime.Now;
        }

        private void dgProduct_MouseDoubleClic(object sender, MouseButtonEventArgs e)
        {
            var posProduct = new PosProduct(root, product);
            posProduct.ShowDialog();
            context.Products.Attach(product);
            context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            if (product.Count == 0)
            {
                context.Products.Remove(product);
            }
            context.SaveChanges();
            dgProduct.ItemsSource = context.Products.ToList();
            dgSaleProduct.ItemsSource = context.SaleProducts.ToList();
            dgUtilProduct.ItemsSource = context.UtilProducts.ToList();
            if (context.Products.Count() > 0)
                summ.Content = context.Products.Count() + " одиниць товару на загальну суму: " + context.Products.Sum(x => x.Summa) + "грн.";
            if (context.UtilProducts.Count() > 0)
                util.Content = "На суму: " + context.UtilProducts.Sum(x => x.Summa) + "грн.";
            if (context.SaleProducts.Count() > 0)
                sale.Content = "На суму: " + context.SaleProducts.Sum(x => x.Summa) + "грн.";
        }

        private void dgCategory_MouseDoubleClic(object sender, MouseButtonEventArgs e)
        {
            tmProd = new List<Product>();
            tmProd = context.Products.Where(x => x.CategoryID == category.ID).Where(x => x.Date >= dateStart && x.Date <= dateStop).ToList();
            dgProduct.ItemsSource = tmProd.ToList();
            summDate.Content = tmProd.Count() + " одиниць товару на суму: " + tmProd.Sum(x => x.Summa) + "грн.";
        }

        private void dgSupplier_MouseDoubleClic(object sender, MouseButtonEventArgs e)
        {
            tmProd = new List<Product>();
            tmProd = context.Products.Where(x => x.SupplierID == supplier.ID).Where(x => x.Date >= dateStart && x.Date <= dateStop).ToList();
            dgProduct.ItemsSource = tmProd.ToList();
            summDate.Content = tmProd.Count() + " одиниць товару на суму: " + tmProd.Sum(x => x.Summa) + "грн.";
        }

        private void F1_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                var winHelp = new WindowHelp();
                winHelp.ShowDialog();
            }
        }
    }
}