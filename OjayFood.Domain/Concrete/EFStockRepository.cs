using System;
using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFStockRepository : IStockRepository
    {
        private readonly EFDbContext _dbcontext;
        public EFStockRepository()
        {
            _dbcontext = new EFDbContext();
        }

        public IQueryable<Stock> Stocks => _dbcontext.Stocks;

        public void SaveStock(Stock stock)
        {
            if (_dbcontext.Stocks.Find(stock.Id) == null)
            {
                _dbcontext.Stocks.Add(stock);
            }
            else
            {
                var dbEntry = _dbcontext.Stocks.Find(stock.Id);
                if (dbEntry != null)
                {

                    dbEntry.Id = stock.Id;
                    dbEntry.ProductId = stock.ProductId;
                    dbEntry.Quantity = stock.Quantity;
                    dbEntry.Stock_Detail = stock.Stock_Detail;
                }

            }
            _dbcontext.SaveChanges();
        }
        public Stock DeleteStock(int id)
        {
            var dbEntry = _dbcontext.Stocks.Find(id);
            if (dbEntry != null)
            {
                _dbcontext.Stocks.Remove(dbEntry);
                _dbcontext.SaveChanges();
            }
            return dbEntry;
        }
        public Stock GetStock(int id)
        {
            return _dbcontext.Stocks.Find(id);
        }
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
