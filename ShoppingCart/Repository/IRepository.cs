using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Repository
{
    public interface IRepository<TModel>
    {
        void Create(TModel Model);
        void Update(TModel Model);
        void Delete(TModel Model);
        IEnumerable<TModel> Select();
        int GetLatestIdentity();
    }
}