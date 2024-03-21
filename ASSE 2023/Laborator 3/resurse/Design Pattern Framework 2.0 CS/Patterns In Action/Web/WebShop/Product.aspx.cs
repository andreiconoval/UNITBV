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

using DoFactory.BusinessLayer.BusinessObjects;
using DoFactory.BusinessLayer.Facade;
using DoFactory.Framework.Cart;

public partial class WebShop_Product : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set the selected menu item in Master page.
            Master.TheMenuInMasterPage.SelectedItem = "products";

            // Save off ProductId for this page.
            ProductId = int.Parse(Request["id"].ToString());
        }
    }

    /// <summary>
    /// Adds item to shopping cart and redirect to shopping cart page.
    /// </summary>
    protected void ButtonAddToCart_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        // Retrieve product via Product Facade.
    	ProductFacade facade = new ProductFacade();
        Product product = facade.GetProduct(ProductId);

        // Get prodcut details and add information to cart.
        string name = product.ProductName;
        double unitPrice = product.UnitPrice;

        int quantity;
        if (!int.TryParse(TextBoxQuantity.Text.Trim(), out quantity))
            quantity = 1;

		Cart.AddItem(ProductId, name, quantity, unitPrice);

        // Show shopping cart to user.
		Response.Redirect("Cart.aspx");
    }

    /// <summary>
    /// Gets and sets productId to Session.
    /// </summary>
    private int ProductId
    {
        set { Session["productId"] = value; }
        get { return int.Parse(Session["productId"].ToString()); }
    }
}
