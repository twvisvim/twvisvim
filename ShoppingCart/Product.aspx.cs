using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
                Session.Add("Cart", new ShoppingCart<CartItem>());
            else
                this.UpdateCartStatus();

            if (!Page.IsPostBack)
                this.InitializeList();
        }

        protected void cmdPlace_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
                Response.Redirect(Request.RawUrl);

            (Session["Cart"] as ShoppingCart<CartItem>).Place();
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
                Response.Redirect(Request.RawUrl);

            (Session["Cart"] as ShoppingCart<CartItem>).Cancel();

            this.UpdateCartStatus();
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                // find item exists.
                ShoppingCart<CartItem> cart = Session["Cart"] as ShoppingCart<CartItem>;

                if (cart.GetItems().Where(p => p.Id == e.CommandArgument.ToString()).Any())
                    cart.GetItems().Where(p => p.Id == e.CommandArgument.ToString()).First().Qty += 1;
                else
                {
                    (Session["Cart"] as ShoppingCart<CartItem>).Add(new CartItem()
                        {
                            Id = e.CommandArgument.ToString(),
                            Qty = 1
                        });
                }
            }

            this.UpdateCartStatus();
        }

        private void UpdateCartStatus()
        {
            this.lblCartItems.Text =
                (Session["Cart"] as ShoppingCart<CartItem>).GetItems().Count().ToString();
        }

        private void InitializeList()
        {
            List<Models.Product> products = new List<Models.Product>();

            Models.Product prodA = new Models.Product()
            {
                Id = "1",
                Caption = "ItemA"
            };
            Models.Product prodB = new Models.Product()
            {
                Id = "2",
                Caption = "ItemB"
            };
            Models.Product prodC = new Models.Product()
            {
                Id = "3",
                Caption = "ItemC"
            };

            products.Add(prodA);
            products.Add(prodB);
            products.Add(prodC);

            this.gvProducts.DataSource = products;
            this.gvProducts.DataBind();
        }
    }
}