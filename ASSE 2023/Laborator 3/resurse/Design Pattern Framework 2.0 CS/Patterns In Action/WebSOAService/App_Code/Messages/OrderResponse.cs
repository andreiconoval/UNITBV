using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Xml.Serialization;

/// <summary>
/// Respresents a response with a list of orders for a given customer.
/// </summary>
[Serializable]
[XmlInclude(typeof(OrderTransferObject))]
public class OrderResponse : ResponseBase
{
    /// <summary>
    /// List of orders for a given customer.
    /// </summary>
    public OrderTransferObject[] Orders;
}
