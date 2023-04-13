namespace ProvaPub.Extension
{
    public static class PaymentPix
    {
        public static string PayPixTransaction(this OrderPayment orderPayment)
        {
            // Implementar pagamento

            return orderPayment.FormatTransaction();
        }
    }
}
