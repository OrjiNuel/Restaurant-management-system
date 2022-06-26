using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Abstract
{
    public interface IStockRepository
    {
        IQueryable<Stock> Stocks { get; }
        void SaveStock(Stock stock);
        Stock DeleteStock(int id);
        Stock GetStock(int id);
        void Dispose();
    }
}
