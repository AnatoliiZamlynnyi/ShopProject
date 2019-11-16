namespace ShopCL
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return $"{CategoryName}";
        }
    }
}