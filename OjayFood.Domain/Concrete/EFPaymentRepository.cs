using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFPaymentRepository : IPaymentRepository
    {
        private readonly EFDbContext _dbcontext;
        public EFPaymentRepository()
        {
            _dbcontext = new EFDbContext();
        }

        public IEnumerable<Payment> GetAllPayments
        {
            get { return _dbcontext.Payments; }
        }


        public void SavePayment(Payment payment)
        {
            if (_dbcontext.Payments.Find(payment.Id) == null)
            {
                _dbcontext.Payments.Add(payment);
            }
            else
            {
                var dbEntry = _dbcontext.Payments.Find(payment.Id);
                if (dbEntry != null)
                {

                    dbEntry.Id = payment.Id;
                    dbEntry.PaymentTypeId = payment.PaymentTypeId;
                    dbEntry.Amount = payment.Amount;
                    dbEntry.CardNumber = payment.CardNumber;                   
                }

            }
            _dbcontext.SaveChanges();
        }
        public Payment DeletePayment(int Id)
        {
            var dbEntry = _dbcontext.Payments.Find(Id);
            if (dbEntry != null)
            {
                _dbcontext.Payments.Remove(dbEntry);
                _dbcontext.SaveChanges();
            }
            return dbEntry;
        }
        public Payment GetPayment(int Id)
        {
            return _dbcontext.Payments.Find(Id);
        }
        
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
