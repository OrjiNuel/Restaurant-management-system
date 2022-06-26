using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFPaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly EFDbContext _dbcontext;
        public EFPaymentTypeRepository()
        {
            _dbcontext = new EFDbContext();
        }

        public IEnumerable<PaymentType> GetAllPaymentTypes
        {
            get { return _dbcontext.PaymentTypes; }
        }

        public void SavePaymentType(PaymentType paymentType)
        {
            if (_dbcontext.PaymentTypes.Find(paymentType.Id) == null)
            {
                _dbcontext.PaymentTypes.Add(paymentType);
            }
            else
            {
                var dbEntry = _dbcontext.PaymentTypes.Find(paymentType.Id);
                if (dbEntry != null)
                {

                    dbEntry.Id = paymentType.Id;
                    dbEntry.Name = paymentType.Name;
                }

            }
            _dbcontext.SaveChanges();
        }
        public void UpdatePaymentType(PaymentType paymentType)
        {

            _dbcontext.Entry(paymentType).State = System.Data.Entity.EntityState.Modified;

            _dbcontext.SaveChanges();
        }
        public void DeletePaymentType(int id)
        {
            var dbEntry = _dbcontext.PaymentTypes.Find(id);
            if (dbEntry != null)
            {
                _dbcontext.PaymentTypes.Remove(dbEntry);
                _dbcontext.SaveChanges();
            }
        }
        public PaymentType GetPaymentType(int id)
        {
            return _dbcontext.PaymentTypes.Find(id);
        }
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
