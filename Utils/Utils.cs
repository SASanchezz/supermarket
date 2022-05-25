namespace supermarket.Utils
{
    internal class Utils
    {
        public static string ParseOrder(int startFrom, params string[] sorts)
        {
            string order = "";
            for (int i = startFrom; i < sorts.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    if (sorts[i + 1] == "")
                    {
                        ++i;
                        continue;
                    }
                    order += sorts[i] + ' ';
                }
                else
                {
                    order += sorts[i] + ", ";
                }
            }
            return order;
        }
    }
}
