using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;

namespace ShopCL
{
    public class EfContext : DbContext
    {
        static SqlConnectionStringBuilder connectDB = new SqlConnectionStringBuilder();
        static string[] file = File.ReadAllLines(@"configDB.cfg");
        public EfContext() : base(ConnectServer(file)) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<UtilProduct> UtilProducts { get; set; }

        static private string ConnectServer(string[] file)
        {
            connectDB.DataSource = file[0].Substring(14);
            connectDB.InitialCatalog = file[1].Substring(9);
            if (file[2].Substring(30) == "0")
                connectDB.IntegratedSecurity = true;
            else
            {
                connectDB.IntegratedSecurity = false;
                connectDB.UserID = file[3].Substring(9);
                connectDB.Password = file[4].Substring(13);
            }
            return connectDB.ConnectionString;
        }
    }
}