using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DoFactory.BusinessLayer.Facade;

public partial class WebAdmin_Customers : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set the selected menu item in the Master page.
            Master.TheMenuInMasterPage.SelectedItem = "customers";

            // Set default sort settings.
            SortColumn = "CustomerId";
            SortDirection = "ASC";

            Bind();
        }
    }

    /// <summary>
    /// Sets datasources and bind data. 
    /// </summary>
    private void Bind()
    {
        CustomerFacade facade = new CustomerFacade();
        GridViewCustomers.DataSource = facade.GetCustomers(SortExpression);
        GridViewCustomers.DataBind();
    }

    #region Sorting

    /// <summary>
    /// Sets sort order and re-binds page.
    /// </summary>
    protected void GridViewCustomers_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
        SortColumn = e.SortExpression;

        Bind();
    }

    /// <summary>
    /// Adds glyphs to gridview header according to sort order.
    /// </summary>
    protected void GridViewCustomers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            AddGlyph(this.GridViewCustomers, e.Row);
        }
    }

    #endregion

    /// <summary>
    /// Deletes selected customer from database.
    /// </summary>
    protected void GridViewCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridViewCustomers.Rows[e.RowIndex];
        int customerId = int.Parse(row.Cells[0].Text);

        CustomerFacade facade = new CustomerFacade();
        int rowsAffected = facade.DeleteCustomer(customerId);
        if (rowsAffected == 0)
        {
            string customerName = row.Cells[1].Text;
            LabelError.Text = "Cannot delete " + customerName + " because they have existing orders!";
        }
        else
        {
            Bind();
        }
    }
}
