using ShoppingCart.Models;
using ShoppingCart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IRepository<Customer> repository = new CustomerRepository();

                this.gvCustomers.DataSource = repository.Select();
                this.gvCustomers.DataBind();
            }
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "CustomerUpdate":
                    Response.Redirect("~/CustomerForm.aspx?id=" + id.ToString());
                    break;

                case "CustomerDelete":

                    IRepository<Customer> repository = new CustomerRepository();
                    repository.Delete(new Customer()
                    {
                        Id = id
                    });

                    this.gvCustomers.DataSource = repository.Select();
                    this.gvCustomers.DataBind();

                    repository = null;

                    break;
            }
        }

        protected void cmdAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CustomerForm.aspx");
        }
    }
}