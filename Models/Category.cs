namespace supermarket.Models
{
    internal interface ICategory
    {
        int CategoryNumber();
        string CategoryName();
    }

    public class Category
    {
        int category_number;
        string category_name;

        public Category(int categoryNumber, string categoryName)
        {
            category_number = categoryNumber;
            category_name = categoryName;
        }
    }
}
