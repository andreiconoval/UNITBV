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

public partial class WebAdmin_Order : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set the selected menu item in the Master page.
            Master.TheMenuInMasterPage.SelectedItem = "orders";

            // Save off customerId 
            CustomerId = int.Parse(Request["id"].ToString());

            Bind();
        }
    }

    /// <summary>
    /// Sets datasources and bind data to controls.
    /// </summary>
    private void Bind()
    {
        // Get customer via Customer Facade.
        CustomerFacade facade = new CustomerFacade();
        Customer customer = facade.GetCustomer(CustomerId);

        // Set company name
        LabelCustomerName.Text = "Customer: " + customer.Company + " (" + customer.Country + ")";

        // "Late binding" of customer orders using ProxyForOrders.
        // That is, Orders are only retrieved when binding takes place.
        GridViewOrders.DataSource = customer.Orders;
        GridViewOrders.DataBind();
    }

    /// <summary>
    /// Gets or sets customerId for the page in Session.
    /// </summary>
    private int CustomerId
    {
        get { return int.Parse(Session["customerId"].ToString()); }
        set { Session["customerId"] = value; }
    }
}
