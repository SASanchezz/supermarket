using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.Middlewares.Receipts
{
    internal static class ReceiptValidator
    {
        public static string Validate(string cardNumber, string sum)
        {
            if (cardNumber.Length > 13) return "завелика довжина номера карти";
            if (cardNumber.Length < 12) return "замаленька довжина номера карти";

            try
            {
                double.Parse(sum);
            } catch { return "Введена некоректна сума"; }

            if (double.Parse(sum) < 0) return "Сума не може бути вуд'ємною";
            if (sum.Split('.')[0].Length > 13) return "Максимум - 13 знаків до точки";
            if (sum.Split('.')[1].Length > 4) return "Максимум - 4 знаків після точки";

            return ""; //OK
        }
    }
}
