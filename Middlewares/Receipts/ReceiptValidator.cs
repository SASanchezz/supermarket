using Customers = supermarket.Models.Customer;

namespace supermarket.Middlewares.Receipts
{
    internal static class ReceiptValidator
    {
        public static string Validate(string cardNumber, string sum)
        {
            if (cardNumber.Length != 0) //if card number is emtpy, don't add it to receipt
            {
                if (cardNumber.Length > 13) return "Завелика довжина номера карти";
                if (cardNumber.Length < 12) return "Замаленька довжина номера карти";

                if (Customers.GetCustomerByCardNumber(cardNumber) == null) return "Нема такого клієнта";
            }
            

            try
            {
                double.Parse(sum);
            } catch { return "Введена некоректна сума"; }

            if (double.Parse(sum) < 0) return "Сума не може бути вуд'ємною";
            if (sum.Split('.')[0].Length > 13) return "Максимум - 13 знаків до точки";
            if (sum.Split('.').Length > 1 && sum.Split('.')[1].Length > 4) return "Максимум - 4 знаків після точки";

            return ""; //OK
        }
    }
}
