using System.IO;
using System.Windows;

namespace ShopProject
{
    public partial class WindowHelp : Window
    {
        public WindowHelp()
        {
            InitializeComponent();
            help.Text = File.ReadAllText("ReadMe.txt");
        }
    }
}