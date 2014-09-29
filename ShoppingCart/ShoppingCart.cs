using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    public class ShoppingCart<TCart> : IShoppingCart<TCart> 
        where TCart: CartItem 
    {
        private List<TCart> _cartItems = new List<TCart>();

        public void Add(TCart Item)
        {
            this._cartItems.Add(Item);
        }

        public void Remove(TCart Item)
        {
            this._cartItems.Remove(Item);
        }

        public IEnumerable<TCart> GetItems()
        {
            return this._cartItems;
        }

        public void Place()
        {
            // do place order operation.
        }

        public void Cancel()
        {
            this._cartItems.Clear();
        }
    }
}