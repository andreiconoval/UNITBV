using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Net.Mail;

using DoFactory.Framework.Log;
using DoFactory.Framework.Cart;
using DoFactory.DataLayer.DataObjects;
using DoFactory.Framework.Transactions;

//
// Unfortunately the /doc setting is not available in VS 2005 for ASP.NET projects. 
// Therefore, no documentation could be generated for this project.  See also: 
// http://lab.msdn.microsoft.com/productfeedback/viewfeedback.aspx?feedbackid=e9c04230-1d2e-48a3-8e30-9f61f3b4d521
// 

/// <summary>
/// This implementation of Global brings the model back to VS 2003 global.asax.
/// </summary>
public class Global : HttpApplication
{
    /// <summary>
    /// Event handler for application start event. Initializes logging.
    /// </summary>
    protected void Application_Start(Object sender, EventArgs e)
    {
        // Initialize logging facility
        InitializeLogger();
    }

    /// <summary>
    /// Event handler for session start event. Initializes shopping cart.
    /// </summary>
    protected void Session_Start(Object sender, EventArgs e)
    {
        // Each session has its own copy of a shopping cart
        Session["ShoppingCart"] = InitializeShoppingCart();
    }

    /// <summary>
    /// Initializes logging facility with severity level and observer(s).
    /// Private helper method.
    /// </summary>
    private void InitializeLogger()
    {
        // Read and assign application wide logging severity
        string severity = ConfigurationManager.AppSettings.Get("LogSeverity");
        SingletonLogger.Instance.Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), severity, true);

        // Send log messages to database (observer pattern)
        ILog log = new ObserverLogToDatabase();
        SingletonLogger.Instance.Attach(log);

        // Send log messages to email (observer pattern)
        string from = "notification@yourcompany.com";
        string to = "webmaster@yourcompany.com";
        string subject = "Webmaster: please review";
        SmtpClient smtpClient = new SmtpClient("mail.yourcompany.com");

        log = new ObserverLogToEmail(from, to, subject, smtpClient);
        SingletonLogger.Instance.Attach(log);

        // Send log messages to a file
        log = new ObserverLogToFile(@"C:\Temp\DoFactory.log");
        SingletonLogger.Instance.Attach(log);

        // Send log message to event log
        log = new ObserverLogToEventlog();
        SingletonLogger.Instance.Attach(log);
    }

    /// <summary>
    /// Initializes shopping cart. Private helper method.
    /// </summary>
    /// <returns>Shopping cart with default Shipping method.</returns>
    private ShoppingCart InitializeShoppingCart()
    {
        ShoppingCart cart = new ShoppingCart();

        // Read and assign default shipping method (strategy pattern)
        string method = ConfigurationManager.AppSettings.Get("ShippingMethod");
        string typeName = "DoFactory.Framework.Cart.ShippingStrategy" + method;
        IShipping shipping = (IShipping)AppDomain.CurrentDomain.CreateInstance("Cart", typeName).Unwrap();
        cart.ShippingStrategy = shipping;

        return cart;
    }
}
