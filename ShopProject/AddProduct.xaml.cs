using ShopCL;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject
{
    public partial class AddProduct : Window
    {
        EfContext context;
        DateTime dateTime;
        public User root { get; set; }
        public AddProduct(User us)
        {
            InitializeComponent();
            context = new EfContext();
            root = us;
            this.Title = "Магазин(Новий товар) - Користувач: " + root.UserName;
            if (context.Categoryes.Count() != 0)
                categProd.ItemsSource = context.Categoryes.Select(x => x.CategoryName).ToList();
            else
                categProd.ItemsSource = "";
            if (context.Suppliers.Count() != 0)
                postProd.ItemsSource = context.Suppliers.Select(x => x.Name).ToList();
            else
                postProd.ItemsSource = "";
            nameProd.Focus();
        }

        private void Date_Changet(object sender, SelectionChangedEventArgs e)
        {
            if (date.SelectedDate <= System.DateTime.Now && date.SelectedDate >= System.DateTime.MinValue)
                dateTime = DateTime.Parse(date.SelectedDate.ToString());
            else
                dateTime = System.DateTime.Now;
        }

        private void Price_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double a = double.Parse(inPrice.Text);
                double b = double.Parse(mark.Text);
                double rez = a + (a * b / 100);
                outPrice.Text = rez.ToString();
            }
            catch { }
        }

        private void Summa_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                summaPrice.Text = (double.Parse(countProd.Text) * double.Parse(outPrice.Text)).ToString();
            }
            catch { }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Product newproduct = new Product();
            if (nameProd.Text != "" && inPrice.Text != "" && countProd.Text != "" && unitProd.Text != "" && outPrice.Text != "")
            {
                newproduct.Date = dateTime;
                newproduct.ProductName = nameProd.Text;
                newproduct.PriceIn = double.Parse(inPrice.Text);
                newproduct.Count = double.Parse(countProd.Text);
                newproduct.Unit = unitProd.Text;
                newproduct.PriceOut = double.Parse(outPrice.Text);
                newproduct.Summa = double.Parse(summaPrice.Text);
                if (categProd.SelectedItem != null && categProd.SelectedItem != null)
                {
                    var listC = context.Categoryes.ToList();
                    foreach (var item in listC)
                        if (item.CategoryName == categProd.SelectedItem.ToString())
                            newproduct.CategoryID = item.ID;
                    var listS = context.Suppliers.ToList();
                    foreach (var item in listS)
                        if (item.Name == postProd.SelectedItem.ToString())
                            newproduct.SupplierID = item.ID;
                }
                else
                    MessageBox.Show("Немає категоріїї або постачальника");
                newproduct.UserID = root.ID;
                newproduct.Note = noteProd.Text;
                context.Products.Add(newproduct);
                context.SaveChanges();
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}