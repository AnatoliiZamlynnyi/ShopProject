namespace ShopCL
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Access { get; set; }

        public override string ToString()
        {
            return $"{UserName}";
        }
    }
}