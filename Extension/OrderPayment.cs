namespace ProvaPub.Extension
{
    public class OrderPayment
    {
        public string NumberTransaction { get; private set; }
        public decimal PaymentValue { get; private set; }
        public int CustomerId { get; private set; }
        public OrderPayment(decimal paymentValue, int customerId)
        {
            PaymentValue = paymentValue;
            CustomerId = customerId;
        }

        public string FormatTransaction()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            NumberTransaction = new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            // Numero de transacao formatado
            return NumberTransaction;
        }
    }
}
