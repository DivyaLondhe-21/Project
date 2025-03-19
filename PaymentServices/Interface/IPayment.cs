
using PaymentService.Models;

namespace PaymentService.Interface
{
    public interface IPayment
    {
        IEnumerable<Payment> GetPayments();
        Payment? GetPaymentById(int id);
        Payment CreatePayment(Payment payment);
        Payment UpdatePayment(int id, Payment updatedPayment);
        Payment? DeletePayment(int id);
    }
}