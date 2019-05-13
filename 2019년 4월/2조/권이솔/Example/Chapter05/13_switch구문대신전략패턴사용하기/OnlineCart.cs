using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._13_switch구문대신전략패턴사용하기
{
    public class OnlineCart
    {
        private readonly Dictionary<PaymentType, IPaymentStrategy> paymentStratergies;

        public OnlineCart()
        {
            paymentStratergies = new Dictionary<PaymentType, IPaymentStrategy>();
            paymentStratergies.Add(PaymentType.CreditCart, new PaypalPaymentStrategy());
            paymentStratergies.Add(PaymentType.GoogleCheckout, new PaypalPaymentStrategy());
            paymentStratergies.Add(PaymentType.AmazonPayments, new PaypalPaymentStrategy());
            paymentStratergies.Add(PaymentType.Paypal, new PaypalPaymentStrategy());
        }


        public void CheckOut(PaymentType paymentType)
        {
            paymentStratergies[paymentType].ProcessPayment();

            //switch(paymentType)
            //{
            //    case PaymentType.CreditCart:
            //        ProcessCreditCardPayment();
            //        break;
            //    case PaymentType.Paypal:
            //        ProcessPaypalPayment();
            //        break;
            //    case PaymentType.GoogleCheckout:
            //        ProcessGooglePayment();
            //        break;
            //    case PaymentType.AmazonPayments:
            //        ProcessAmazonPayment();
            //        break;
            //}
        }

        //private void ProcessAmazonPayment()
        //{
        //    Console.WriteLine("아마존 결제");
        //}

        //private void ProcessGooglePayment()
        //{
        //    Console.WriteLine("구글 결제");
        //}

        //private void ProcessPaypalPayment()
        //{
        //    Console.WriteLine("페이팔 결제");
        //}

        //private void ProcessCreditCardPayment()
        //{
        //    Console.WriteLine("신용카드 결제");
        //}
    }
}
