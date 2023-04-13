using ProvaPub.Extension;
using ProvaPub.Models;
using ProvaPub.Models.Enum;

namespace ProvaPub.Services
{
    public class OrderService
    {
        public async Task<Order> PayOrder(PaymentMethod paymentMethod, decimal paymentValue, int customerId)
        {
            var orderPayment = new OrderPayment(paymentValue, customerId);

            switch (paymentMethod)
            {
                case PaymentMethod.Pix:
                    orderPayment.PayPixTransaction();
                    break;
                case PaymentMethod.CreditCard:
                    orderPayment.PayCreditTransaction();
                    break;
                case PaymentMethod.PayPal:
                    orderPayment.PayPayPalTransaction();
                    break;
            }

            return await Task.FromResult(new Order()
            {
                Value = paymentValue
            });
        }
    }
}
