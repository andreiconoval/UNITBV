using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Xml.Serialization;

/// <summary>
/// Represents a response to the client for a customer list. 
/// </summary>
[Serializable]
[XmlInclude(typeof(CustomerTransferObject))]
public class CustomerResponse : ResponseBase 
{

    /// <summary>
    /// List of sorted customers. 
    /// </summary>
    public CustomerTransferObject[] Customers;
}
