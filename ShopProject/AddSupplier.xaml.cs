using ShopCL;
using System.Linq;
using System.Windows;

namespace ShopProject
{
    public partial class AddSupplier : Window
    {
        EfContext context;
        public User root { get; set; }
        public AddSupplier(User us)
        {
            InitializeComponent();
            context = new EfContext();
            root = us;
            this.Title = "Магазин(Новий постачальник) - Користувач: " + root.UserName;
            postName.Focus();
        }

        private void Click_AddPostButton(object sender, RoutedEventArgs e)
        {
            Supplier newsupplier = new Supplier();
            if (context.Suppliers.Any(x => x.Name != postName.Text) && postName.Text != "" || context.Suppliers.Count() == 0)
            {
                newsupplier.Name = postName.Text;
                newsupplier.Phone = postPhone.Text;
                newsupplier.Address = postAdres.Text;
                context.Suppliers.Add(newsupplier);
                context.SaveChanges();
                this.Close();
            }
            else
                stanPost.Content = "Такий постачальник вже існує";
        }

        private void Click_CancelPButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}