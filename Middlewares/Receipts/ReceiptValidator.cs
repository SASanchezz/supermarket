using System;

namespace supermarket.Middlewares.Receipts
{
    public static class ReceiptValidator
    {
        public static void Validate(string cardNumber, string sum)
        {
            if (cardNumber.Length > 13)
            {
                throw new Exception("завелика довжина номера карти");
            }

            if (cardNumber.Length < 12)
            {
                throw new Exception("замаленька довжина номера карти");
            }

            try
            {
                double.Parse(sum);
            }
            catch
            {
                throw new Exception("Введена некоректна сума");
            }

            if (double.Parse(sum) < 0)
            {
                throw new Exception("Сума не може бути вуд'ємною");
            }

            if (sum.Split('.')[0].Length > 13)
            {
                throw new Exception("Максимум - 13 знаків до точки");
            }

            if (sum.Split('.')[1].Length > 4)
            {
                throw new Exception("Максимум - 4 знаків після точки");
            }
        }
    }
}
