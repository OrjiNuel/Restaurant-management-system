using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Abstract
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAllPayments { get; }
        void SavePayment(Payment payment);
        Payment DeletePayment(int Id);
        Payment GetPayment(int Id);
        void Dispose();
    }
}
