using MarketAnalyst.Core.Data.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.DataService
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        Task<T> SaveAsync<T>(AddItemResult<T> item);
        General.StockService StockService { get; }

        General.StockGroupService StockGroupService { get; }
    }

    public class AddItemResult<T>
    {
        public T Domain { get; set; }
        public Entity<int> Result { get; set; }
    }
}
