using ShopCL;
using System.Linq;
using System.Windows;

namespace ShopProject
{
    public partial class AddCategory : Window
    {
        EfContext context;
        static int idCateg;
        public User root { get; set; }
        public AddCategory(User us)
        {
            InitializeComponent();
            context = new EfContext();
            root = us;
            this.Title = "Магазин(Новова категорія) - Користувач: " + root.UserName;
            categoryName.Focus();
        }

        private void Click_AddCategoryButton(object sender, RoutedEventArgs e)
        {
            Category newCategory = new Category();
            if (context.Categoryes.Any(x => x.CategoryName != categoryName.Text) && categoryName.Text != "" || context.Categoryes.Count() == 0)
            {
                newCategory.CategoryName = categoryName.Text;
                context.Categoryes.Add(newCategory);
                context.SaveChanges();
                this.Close();
            }
            else
                stanCat.Content = "Така категорія вже існує";
        }

        private void Click_CancelCButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}