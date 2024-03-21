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
using DoFactory.BusinessLayer.BusinessObjects;

public partial class WebAdmin_OrderDetails : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set the selected menu item in Master page.
            Master.TheMenuInMasterPage.SelectedItem = "orders";

            // Save off OrderId for this page.
            OrderId = int.Parse(Request["id"].ToString());

            Bind();
        }
    }


    /// <summary>
    /// Sets datasources and bind data to controls.
    /// </summary>
    private void Bind()
    {
        // Get specific order via Customer Facade.
        CustomerFacade facade = new CustomerFacade();
        Order order = facade.GetOrder(OrderId);

        // Set the date
        LabelCustomerName.Text = "Customer: " + order.Customer.Company + " (" + order.Customer.Country + ")";
        LabelOrderDate.Text = "Order date: " + order.OrderDate.ToShortDateString();

        // "Late binding" of order details using ProxyForOrderDetails.
        // That is, order details are only retrieved when binding takes place.
        GridViewOrderDetails.DataSource = order.OrderDetails;
        GridViewOrderDetails.DataBind();
    }

    /// <summary>
    /// Gets or sets orderId for the page in Session.
    /// </summary>
    private int OrderId
    {
        get { return int.Parse(Session["OrderId"].ToString()); }
        set { Session["OrderId"] = value; }
    }

    #region Sorting

    /// <summary>
    /// Sets sort order and re-binds page.
    /// </summary>
    protected void GridViewOrderDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        //SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
        //SortColumn = e.SortExpression;

        //Bind();
    }

    /// <summary>
    /// Adds glyph to header according to sort settings.
    /// </summary>
    protected void GridViewOrderDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            AddGlyph(this.GridViewOrderDetails, e.Row);
        }
    }

    #endregion
}
