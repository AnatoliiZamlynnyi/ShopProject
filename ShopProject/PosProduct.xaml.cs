using ShopCL;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject
{
    public partial class PosProduct : Window
    {
        EfContext context;
        Product product;
        DateTime dateTime;
        public User root { get; set; }
        public PosProduct(User us, Product prod)
        {
            InitializeComponent();
            context = new EfContext();
            root = us;
            product = prod;
            this.Title = "Магазин - Користувач: " + root.UserName;
            if (root.Access != 1)
            {
                util.IsEnabled = false;
            }
            dateP.SelectedDate = product.Date;
            nameP.Text = product.ProductName;
            inPriceP.Text = product.PriceIn.ToString();
            countProdP.Text = product.Count.ToString();
            unitProdP.Text = product.Unit;
            markP.Text = ((product.PriceOut - product.PriceIn) / product.PriceIn * 100).ToString();
            outPriceP.Text = product.PriceOut.ToString();
            summaPriceP.Text = product.Summa.ToString();
            categProdP.Text = product.Categoryes.CategoryName;
            postProdP.Text = prod.Suppliers.Name;
        }

        private void CancelP_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Util_Click(object sender, RoutedEventArgs e)
        {
            if (product.Count >= double.Parse(countProdP.Text))
            {
                UtilProduct utilProduct = new UtilProduct();
                utilProduct.Date = DateTime.Parse(dateP.SelectedDate.ToString());
                utilProduct.ProductName = nameP.Text;
                utilProduct.PriceIn = double.Parse(inPriceP.Text);
                utilProduct.Count = double.Parse(countProdP.Text);
                utilProduct.Unit = unitProdP.Text;
                utilProduct.PriceOut = double.Parse(outPriceP.Text);
                utilProduct.Summa = double.Parse(summaPriceP.Text);
                utilProduct.CategoryID = product.CategoryID;
                utilProduct.SupplierID = product.SupplierID;
                utilProduct.UserID = root.ID;
                utilProduct.Note = noteProdP.Text;
                context.UtilProducts.Add(utilProduct);
                product.Count -= double.Parse(countProdP.Text);
                product.Summa -= double.Parse(summaPriceP.Text);
                context.SaveChanges();
            }
            else
                MessageBox.Show("Неможливо списати таку кількість товару!");
            this.Close();
        }

        private void Sale_Click(object sender, RoutedEventArgs e)
        {
            if (product.Count >= double.Parse(countProdP.Text))
            {
                SaleProduct saleProduct = new SaleProduct();
                saleProduct.Date = DateTime.Parse(dateP.SelectedDate.ToString());
                saleProduct.ProductName = nameP.Text;
                saleProduct.PriceIn = double.Parse(inPriceP.Text);
                saleProduct.Count = double.Parse(countProdP.Text);
                saleProduct.Unit = unitProdP.Text;
                saleProduct.PriceOut = double.Parse(outPriceP.Text);
                saleProduct.Summa = double.Parse(summaPriceP.Text);
                saleProduct.CategoryID = product.CategoryID;
                saleProduct.SupplierID = product.SupplierID;
                saleProduct.UserID = root.ID;
                saleProduct.Note = noteProdP.Text;
                context.SaleProducts.Add(saleProduct);
                product.Count -= double.Parse(countProdP.Text);
                product.Summa -= double.Parse(summaPriceP.Text);
                context.SaveChanges();
            }
            else
                MessageBox.Show("Неможливо продати таку кількість товару!");
            this.Close();
        }

        private void DateP_Changet(object sender, SelectionChangedEventArgs e)
        {
            if (dateP.SelectedDate <= System.DateTime.Now && dateP.SelectedDate >= System.DateTime.MinValue)
                dateTime = DateTime.Parse(dateP.SelectedDate.ToString());
            else
                dateTime = System.DateTime.Now;
        }

        private void PriceP_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double a = double.Parse(inPriceP.Text);
                double b = double.Parse(markP.Text);
                double rez = a + (a * b / 100);
                outPriceP.Text = rez.ToString();
            }
            catch { }
        }

        private void SummaP_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                summaPriceP.Text = (double.Parse(countProdP.Text) * double.Parse(outPriceP.Text)).ToString();
            }
            catch { }
        }
    }
}