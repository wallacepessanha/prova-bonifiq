namespace ProvaPub.Extension
{
    public static class PaymentCredit
    {
        public static string PayCreditTransaction(this OrderPayment orderPayment)
        {
            // Implementar pagamento

            return orderPayment.FormatTransaction();
        }
    }
}
