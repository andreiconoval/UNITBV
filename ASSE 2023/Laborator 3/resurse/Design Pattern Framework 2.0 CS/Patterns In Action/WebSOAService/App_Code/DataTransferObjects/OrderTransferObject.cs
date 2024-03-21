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
/// The purpose of the CustomerTransferObject is to facilitate transport of 
/// customer business data in a serializable format. Business data is kept in 
/// publicly accessible field members. This class has no methods or properties. 
/// 
/// Some may frown upon the fact that data members are public, but there is no
/// point in using properties because there is no business logic in this class.
/// The public field names proper-cased, like properties, rather than camel-case
/// which is normally used for data members. 
/// 
/// Pattern: Data Data Transfer Objects.
/// </summary>
/// <remarks>
/// Data Transfer Objects are objects that transfer data between processes, but without behavior.
/// </remarks>
[Serializable]
[XmlInclude(typeof(OrderDetailTransferObject))]
public class OrderTransferObject
{
    /// <summary>
    /// Unique order identifier. This number is encrypted before it reaches 
    /// the client. This will avoid exposing internal identifiers to the outside world. 
    /// Clients cannot guess identifiers that are potentially not 'owned' by them.
    /// </summary>
    public string OrderId;

    /// <summary>
    /// Date the order is placed.
    /// </summary>
    public DateTime OrderDate;

    /// <summary>
    /// Date the order is required for delivery.
    /// </summary>
    public DateTime RequiredDate;

    /// <summary>
    /// Freight or shipping costs for the order.
    /// </summary>
    public float Freight;

    /// <summary>
    /// List of order details (line items) for the order.
    /// </summary>
    public OrderDetailTransferObject[] OrderDetails;
}
