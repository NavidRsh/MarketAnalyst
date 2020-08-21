using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Data.General
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
    public class Entity<T> : IEntity<T>
    {
        dynamic Item { get; }

        public Entity(dynamic element)
        {
            Item = element;
        }
        public T Id
        {
            get
            {
                var a = (T)Item.Entity.Id;
                return a;
                //  return (T)Item.GetType().GetProperty(PropertyName).GetValue(Item, null);
            }
        }
    }
}
