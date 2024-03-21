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
/// The purpose of the OrderTransferObject is to facilitate transport of 
/// customer order business data in a serializable format. Business data is kept in 
/// publicly accessible field members. This class has no methods or properties. 
/// 
/// Some may frown upon the fact that data members are public, but there is no
/// point in using properties because there is no business logic in this class.
/// The public field names proper-cased, like properties, rather than camel-case
/// which is normally used for data members. 
/// 
/// Pattern: Data Transfer Objects.
/// </summary>
/// <remarks>
/// Data Transfer Objects are objects that transfer data between processes, but without behavior.
/// </remarks>
[Serializable]
public class OrderDetailTransferObject
{
    /// <summary>
    /// Product name ordered.
    /// </summary>
    public string Product;

    /// <summary>
    /// Quantity ordered.
    /// </summary>
    public int Quantity;

    /// <summary>
    /// Unit price for product at order time.
    /// </summary>
    public float UnitPrice;

    /// <summary>
    /// Discount applied to unit price. 
    /// </summary>
    public float Discount;
}
