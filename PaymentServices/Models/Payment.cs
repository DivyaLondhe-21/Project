using System;
using System.Collections.Generic;

namespace PaymentServices.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ReservationId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PayTime { get; set; }
    public string Status { get; set; } = "Pending";
    public string CreditCardNumber4Digits { get; set; } = null!;
    [NotMapped]  // This prevents data from being stored in the database
    public string CreditCardNumber { get; set; } = null!;

    public DateTime CreditCardStartDate { get; set; }

    public DateTime CreditCardEndDate { get; set; }
}
