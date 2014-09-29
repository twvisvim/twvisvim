using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    public interface IShoppingCart<TCart> where TCart: CartItem
    {
        void Add(TCart Item);
        void Remove(TCart Item);
        IEnumerable<TCart> GetItems();
        void Place();
        void Cancel();
    }
}