using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Abstract
{
    public interface IPaymentTypeRepository
    {
        IEnumerable<PaymentType> GetAllPaymentTypes { get; }
        void SavePaymentType(PaymentType paymentType);
        void UpdatePaymentType(PaymentType paymentType);
        void DeletePaymentType(int id);
        PaymentType GetPaymentType(int id);
        void Dispose();
    }
}
