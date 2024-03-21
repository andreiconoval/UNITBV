using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DoFactory.BusinessLayer.Facade;
using DoFactory.BusinessLayer.BusinessObjects;

public partial class WebAdmin_Customer : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set the selected menu item in the Master page.
            Master.TheMenuInMasterPage.SelectedItem = "customers";

            CustomerId = int.Parse(Request["id"].ToString());

            // Set DetailsView control in Add or Edit mode.
            if (CustomerId == 0)
            {
                DetailsViewCustomer.ChangeMode(DetailsViewMode.Insert);
                ImageCustomer.Visible = false; // Suppress customer image 
            }
            else
            {
                DetailsViewCustomer.ChangeMode(DetailsViewMode.Edit);
            }
        }
    }

    /// <summary>
    /// Gets or sets the customerId for this page.
    /// </summary>
    private int CustomerId
    {
        get { return int.Parse(Session["CustomerId"].ToString()); }
        set { Session["CustomerId"] = value; }
    }

    /// <summary>
    /// Saves data for new or edited customer to database.
    /// </summary>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        CustomerFacade facade = new CustomerFacade();

        Customer customer;
        if (CustomerId == 0)
            customer = new Customer();
        else
            customer = facade.GetCustomer(CustomerId);

        // Get Company name from page.
        DetailsViewRow row = DetailsViewCustomer.Rows[1];
        TextBox textBox = row.Cells[1].Controls[0] as TextBox;
        customer.Company = textBox.Text.Trim();

        // Get City from page.
        row = DetailsViewCustomer.Rows[2];
        textBox = row.Cells[1].Controls[0] as TextBox;
        customer.City = textBox.Text.Trim();

        // Get Country from page.
        row = DetailsViewCustomer.Rows[3];
        textBox = row.Cells[1].Controls[0] as TextBox;
        customer.Country = textBox.Text.Trim();

        // Validate required entries.
        if (customer.Company.Length == 0 ||
            customer.City.Length == 0 ||
            customer.Country.Length == 0)
        {
            LabelError.Text = " Please note: all fields are required";
            LabelError.Visible = true;
            return;
        }

        // Execute insert or update.
        if (CustomerId == 0)
            facade.AddCustomer(customer);
        else
            facade.UpdateCustomer(customer);
        
        // Return to list of customers.
        Response.Redirect("Customers.aspx");

    }

    /// <summary>
    /// Cancel the page and redirect user to page with list of customers.
    /// </summary>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customers.aspx");
    }
}
