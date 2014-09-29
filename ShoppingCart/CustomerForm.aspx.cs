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
    public partial class CustomerForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // 檢查 URL 的 QueryString 中是否有包含 id=xxx
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    IRepository<Customer> repository = new CustomerRepository();
                    var customers = repository.Select();

                    if (customers.Where(c => c.Id == Convert.ToInt32(Request.QueryString["id"])).Any())
                    {
                        var customer = customers.Where(
                            c => c.Id == Convert.ToInt32(Request.QueryString["id"])).First();

                        this.txtName.Text = customer.Name;
                        this.txtEmail.Text = customer.Email;
                        this.txtBillAddress.Text = customer.BillAddress;
                        this.txtCellphone.Text = customer.Cellphone;
                        this.txtShipAddress.Text = customer.ShipAddress;
                        this.cboGender.SelectedValue = (customer.Gender) ? "1" : "0";
                    }

                    customers = null;
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            IRepository<Customer> repository = new CustomerRepository();

            // 檢查 URL 的 QueryString 中是否有包含 id=xxx
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                var customer = repository.Select()
                    .Where(c => c.Id == Convert.ToInt32(Request.QueryString["id"])).First();

                customer.Name = this.txtName.Text;
                customer.BillAddress = this.txtBillAddress.Text;
                customer.ShipAddress = this.txtShipAddress.Text;
                customer.Cellphone = this.txtCellphone.Text;
                customer.Gender = (this.cboGender.SelectedValue == "0") ? false : true;
                customer.Email = this.txtEmail.Text;

                repository.Update(customer);
            }
            else
            {
                var model = new Customer()
                {
                    Id = repository.GetLatestIdentity(),
                    Name = this.txtName.Text,
                    BillAddress = this.txtBillAddress.Text,
                    ShipAddress = this.txtShipAddress.Text,
                    Cellphone = this.txtCellphone.Text,
                    Gender = (this.cboGender.SelectedValue == "0") ? false : true,
                    Email = this.txtEmail.Text,
                    Credit = 0
                };

                repository.Create(model);
            }

            repository = null;

            Response.Redirect("~/CustomerList.aspx");            
        }
    }
}