using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class NWOrderMasterDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var row = (e.Row.DataItem as DataRowView).Row;

                if (row.IsNull("ShippedDate"))
                {
                    e.Row.Cells[4].Text = "(Not Shipped)";
                }
                else
                {
                    DateTime orderDate = Convert.ToDateTime(row["OrderDate"]);
                    DateTime shippedDate = Convert.ToDateTime(row["ShippedDate"]);

                    var ts = shippedDate - orderDate;

                    if (ts.TotalDays <= 2)
                        e.Row.Cells[4].ForeColor = Color.Black;
                    else if (ts.TotalDays > 2 && ts.TotalDays <= 5)
                        e.Row.Cells[4].ForeColor = Color.Green;
                    else if (ts.TotalDays > 5 && ts.TotalDays <= 7)
                        e.Row.Cells[4].ForeColor = Color.Blue;
                    else
                        e.Row.Cells[4].ForeColor = Color.Red;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Pager)
            {
                // 操作 Pager 進行分頁列客制化
                var grid = (sender as GridView);
                var lblPageIndex = e.Row.FindControl("lblPageIndex") as Label;
                var lblPageCount = e.Row.FindControl("lblPageCount") as Label;
                var cboMovePage = e.Row.FindControl("cboMovePage") as DropDownList;
                var cboPageSize = e.Row.FindControl("cboPageSize") as DropDownList;
                var txtPageSize = e.Row.FindControl("txtPageSize") as TextBox;
                var cmdFirstPage = e.Row.FindControl("cmdFirstPage") as Button;
                var cmdPreviousPage = e.Row.FindControl("cmdPreviousPage") as Button;
                var cmdNextPage = e.Row.FindControl("cmdNextPage") as Button;
                var cmdLastPage = e.Row.FindControl("cmdLastPage") as Button;

                lblPageIndex.Text = (grid.PageIndex + 1).ToString();
                lblPageCount.Text = grid.PageCount.ToString();

                for (int i=1; i<= grid.PageCount; i++)
                {
                    cboMovePage.Items.Add(new ListItem(i.ToString(), (i - 1).ToString()));
                }

                cboPageSize.SelectedValue = grid.PageSize.ToString();
                txtPageSize.Text = grid.PageSize.ToString();

                // control navigation buttons.
                if (grid.PageIndex == 0)
                {
                    cmdPreviousPage.Enabled = false;
                    cmdFirstPage.Enabled = false;
                }
                else if (grid.PageIndex == (grid.PageCount - 1))
                {
                    cmdNextPage.Enabled = false;
                    cmdLastPage.Enabled = false;
                }

                // control drop-down list to follow current page index.
                cboMovePage.SelectedValue = grid.PageIndex.ToString();
            }
        }

        protected void gvOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var grid = sender as GridView; 

            switch (e.CommandName)
            {
                case "FirstPage":
                    grid.PageIndex = 0;
                    break;

                case "PreviousPage":
                    grid.PageIndex -= 1;
                    break;

                case "NextPage":
                    grid.PageIndex += 1;
                    break;

                case "LastPage":
                    grid.PageIndex = grid.PageCount - 1;
                    break;

                case "SetPageSize":
                    
                    int pageSize = 10;
                    TextBox txtPageSize = null;
            
                    if (grid.TopPagerRow != null)
                        txtPageSize = grid.TopPagerRow.FindControl("txtPageSize") as TextBox;
                    if (txtPageSize == null && grid.BottomPagerRow != null)
                        txtPageSize = grid.BottomPagerRow.FindControl("txtPageSize") as TextBox;

                    if (txtPageSize == null)
                        return;

                    bool isInt = Int32.TryParse(txtPageSize.Text, out pageSize);

                    if (!isInt)
                        return;
                    else
                    {
                        grid.PageSize = pageSize;
                        grid.PageIndex = 0;
                    }

                    break;
            }
        }

        protected void gvOrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //(sender as GridView).PageIndex = e.NewPageIndex;
        }

        protected void cboMovePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvOrderList.PageIndex = Convert.ToInt32((sender as DropDownList).SelectedValue);
        }

        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvOrderList.PageSize = Convert.ToInt32((sender as DropDownList).SelectedValue);
            this.gvOrderList.PageIndex = 0;
        }

        protected void gvOrderList_PreRender(object sender, EventArgs e)
        {
        }

        protected void gvOrderList_DataBound(object sender, EventArgs e)
        {
            var grid = sender as GridView;
            double amount = 0.0;

            foreach (GridViewRow itemRow in grid.Rows)
            {
                if (itemRow.RowType == DataControlRowType.DataRow)
                    amount += Convert.ToDouble(itemRow.Cells[7].Text);
            }

            var row = new GridViewRow(
                grid.Rows.Count,
                -1,
                DataControlRowType.DataRow,
                DataControlRowState.Normal);

            row.Cells.Add(new TableCell());
            row.Cells[0].ColumnSpan = grid.Columns.Count;
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = string.Format(
                "Amount on this page: {0:$0.00}", amount);

            grid.Controls[0].Controls.AddAt(grid.Rows.Count + 2, row);
        }
    }
}