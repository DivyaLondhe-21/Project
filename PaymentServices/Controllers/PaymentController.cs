
using PaymentService.Interface;
using PaymentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/<PaymentController>
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            var payments = _paymentService.GetPayments();
            return Ok(payments);
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public ActionResult<Payment> GetPaymentById(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            return Ok(payment);
        }

        // POST api/<PaymentController>
        [HttpPost]
        public ActionResult<Payment> CreatePayment([FromBody] Payment payment)
        {
            //check if reservation is there for the payment
            if (payment == null || payment.Amount <= 0)
            {
                return BadRequest("Invalid payment details.");
            }

            var result = _paymentService.CreatePayment(payment);
            return Ok(new { PaymentId = result.PaymentId });
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public ActionResult<Payment> UpdatePayment(int id, [FromBody] Payment updatedPayment)
        {
            var payment = _paymentService.UpdatePayment(id, updatedPayment);
            if (payment == null)
            {
                return NotFound("Payment not found");
            }
            return Ok(payment);
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public ActionResult<Payment> DeletePayment(int id)
        {
            var payment = _paymentService.DeletePayment(id);
            if (payment == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            return Ok(payment);
        }
    }
}