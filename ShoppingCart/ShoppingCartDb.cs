using ShoppingCart.Models;
using ShoppingCart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    public class ShoppingCartDb : IShoppingCart<CartItem>
    {
        public void Add(CartItem Item)
        {
            IRepository<CartItem> repository = new CartRepository();
            repository.Create(Item);
        }

        public void Remove(CartItem Item)
        {
            IRepository<CartItem> repository = new CartRepository();
            repository.Delete(Item);
        }

        public IEnumerable<CartItem> GetItems()
        {
            IRepository<CartItem> repository = new CartRepository();
            return repository.Select();
        }

        public void Place()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            IRepository<CartItem> repository = new CartRepository();
            var items = repository.Select();

            foreach (var item in items)
                repository.Delete(item);
        }
    }
}