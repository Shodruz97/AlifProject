using System;

namespace AlifProject
{
    class Program
    {

        public class Product
        {
            public string Name { get; set; }
            public double Amount { get; set; }
            public int[] InstallmentRange { get; set; }
        }

        public class PaymentDetails
        {
            public Product Product { get; set; }
            public string PhoneNumber { get; set; }
            public int Installment { get; set; }
            public double TotalAmount { get; set; }
        }

        public class PaymentService
        {
            public static PaymentDetails CalculatePayment(Product product, string phoneNumber, int installment)
            {
                double totalAmount = product.Amount;
                int[] installmentRange = product.InstallmentRange;

                if (!IsInRange(installment, installmentRange))
                {
                    Console.WriteLine("Invalid installment period");
                    return null;
                }

                if (product.Name == "Смартфон")
                {

                    totalAmount += totalAmount * 0.03 * (installment - 3);
                }
                else if (product.Name == "Компьютер")
                {
                    totalAmount += totalAmount * 0.04 * (installment - 3);
                }
                else if (product.Name == "Телевизор")
                {
                    totalAmount += totalAmount * 0.05 * (installment - 3);
                }

                PaymentDetails paymentDetails = new PaymentDetails
                {
                    Product = product,
                    PhoneNumber = phoneNumber,
                    Installment = installment,
                    TotalAmount = totalAmount
                };

                SendSMS(paymentDetails);

                return paymentDetails;
            }

            public static bool IsInRange(int installment, int[] installmentRange)
            {
                foreach (int value in installmentRange)
                {
                    if (value == installment)
                    {
                        return true;
                    }
                }
                return false;
            }

            public static void SendSMS(PaymentDetails paymentDetails)
            {
                string message = string.Format("Уважаемый клиент, вы совершили покупку {0} на сумму {1:F2} сомони. Сумма платежа по рассрочке на {2} месяцев составляет {3:F2} сомони.", paymentDetails.Product.Name, paymentDetails.Product.Amount, paymentDetails.Installment, paymentDetails.TotalAmount);
                Console.WriteLine("Отправлено SMS на номер {0}: {1}", paymentDetails.PhoneNumber, message);
            }


            public static void Main()
            {
                string[] Name_product = new string[] { "Смартфон", "Компьютер", "Телевизор" };

                for (int i = 0; i < Name_product.Length; ++i)
                {
                    Console.WriteLine(Name_product[i] + " {" + i + "}");

                }
                Console.Write("Выбор товар по соответсвующий номеров: ");
                int numberOfProduct = int.Parse(Console.ReadLine());

                Console.Write("Введите номер телефон: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Введите цена товар ");
                double price = double.Parse(Console.ReadLine());
                Console.Write("Введите диапазон расрочки ");
                int installment = int.Parse(Console.ReadLine());



                Product product = new Product
                {
                    Name = Name_product[numberOfProduct],
                    Amount = price,
                    InstallmentRange = new int[] { 3, 6, 9, 12, 18, 24 }
                };


                PaymentDetails paymentDetails = CalculatePayment(product, phoneNumber, installment);
                Console.WriteLine("Общая сумма платежа: {0:F2} сомони", paymentDetails.TotalAmount);
            }
        }
    }
}
