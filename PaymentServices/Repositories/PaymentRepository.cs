
using PaymentService.Data;
using PaymentService.Interface;
using PaymentService.Models;

namespace PaymentService.Repositories
{
    public class PaymentRepository : IPayment
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment? GetPaymentById(int id)
        {
            return _context.Payments.FirstOrDefault(x => x.PaymentId == id);
        }

        public Payment CreatePayment(Payment payment)
        {
            
            var reservationExists = _context.Reservations.Any(r => r.ReservationId == payment.ReservationId);
            if (!reservationExists)
            {
                throw new Exception("Invalid Reservation ID.");
            }
            //Extrac and store 4 digits of credit card number
            if (!string.IsNullOrEmpty(payment.CreditCardNumber) && payment.CreditCardNumber.Length >= 4)
            {
                
                payment.CreditCardNumber4Digits = payment.CreditCardNumber.Substring(payment.CreditCardNumber.Length - 4);
            }
            else
            {
                throw new Exception("Invalid credit card number.");
            }

            payment.CreditCardNumber = null;
            payment.PaymentId = 0;
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment UpdatePayment(int id, Payment updatedPayment)
        {
            var paymentDetails = _context.Payments.FirstOrDefault(x => x.PaymentId == id);
            if (paymentDetails == null)
            {
                throw new Exception("Payment not found.");
            }
            if (paymentDetails.Status == "Completed")
            {
                throw new Exception("Cannot update a completed payment.");
            }
            paymentDetails.Amount = updatedPayment.Amount;
            paymentDetails.PaymentDate = updatedPayment.PaymentDate;
            paymentDetails.Status = updatedPayment.Status;
            _context.SaveChanges();
            return paymentDetails;
        }

        public Payment? DeletePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(x => x.PaymentId == id);
            if (payment == null)
            {
                return null;
            }

            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return payment;
        }
    }
}