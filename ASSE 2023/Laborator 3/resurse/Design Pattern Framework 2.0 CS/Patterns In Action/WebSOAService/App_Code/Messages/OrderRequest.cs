using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Represents a request for all orders given a customer.
/// </summary>
[Serializable]
public class OrderRequest : RequestBase
{
    /// <summary>
    /// Customer identifier for which orders are requested. 
    /// </summary>
    public string CustomerId;
}
