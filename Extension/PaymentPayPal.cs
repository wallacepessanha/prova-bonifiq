namespace ProvaPub.Extension
{
    public static class PaymentPayPal
    {
        public static string PayPayPalTransaction(this OrderPayment orderPayment)
        {
            // Implementar pagamento

            return orderPayment.FormatTransaction();
        }
    }
}
