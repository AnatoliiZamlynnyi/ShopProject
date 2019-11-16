using ShopCL;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ShopProject
{
    public partial class MainWindow : Window
    {
        EfContext context;
        User user;
        public MainWindow()
        {
            InitializeComponent();
            context = new EfContext();
            context.Database.CreateIfNotExists();
            login.Focus();
        }

        public static string CodingGetHash(string password)
        {
            using (var hash = SHA1.Create())
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        }

        private void Click_EnterButton(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string password = CodingGetHash(pass.Password);
            user = context.Users.FirstOrDefault(x => x.Login == log && x.Password == password);
            if (user == null)
            {
                stan.Content = "Ім'я або пароль невірнi.";
                login.Clear();
                pass.Clear();
            }
            else
            {
                login.Clear();
                pass.Clear();
                var windowProg = new WindowProg(user);
                windowProg.Show();
                this.Close();
            }
        }

        private void Click_RegButton(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void Click_RegisterButton(object sender, RoutedEventArgs e)
        {
            User newUser = new User();
            if (fullName.Text != "" && rLogin.Text != "" && pass1.Password != "" && pass2.Password != "")
            {
                if (context.Users.Any(x => x.Login == rLogin.Text))
                {
                    stanReg.Content = "Оберіть інший логін!";
                    fullName.Clear();
                    rLogin.Clear();
                    pass1.Clear();
                    pass2.Clear();
                }
                if (pass1.Password == pass2.Password)
                {
                    newUser.UserName = fullName.Text;
                    newUser.Login = rLogin.Text;
                    newUser.Password = CodingGetHash(pass1.Password);
                    if (admin.IsChecked == true)
                        newUser.Access = 1;
                    else
                        newUser.Access = 0;
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    fullName.Clear();
                    rLogin.Clear();
                    pass1.Clear();
                    pass2.Clear();
                    popup.IsOpen = false;
                }
                if (pass1.Password != pass2.Password)
                {
                    stanReg.Content = "Паролі не співпадають!";
                    pass1.Clear();
                    pass2.Clear();
                }
            }
            else
                stanReg.Content = "Незаповненні усі поля!";
        }

        private void Click_CancelRButton(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
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