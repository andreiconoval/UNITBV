using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Security;
using System.Collections;

using System.Collections.Generic;

using DoFactory.BusinessLayer.Facade;
using DoFactory.BusinessLayer.BusinessObjects;
using DoFactory.Framework.Encryption;

//
// Unfortunately the /doc setting is not available in VS 2005 for ASP.NET projects. 
// Therefore, no documentation could be generated for this project.  See also: 
// http://lab.msdn.microsoft.com/productfeedback/viewfeedback.aspx?feedbackid=e9c04230-1d2e-48a3-8e30-9f61f3b4d521
// 

/// <summary>
/// Defines the exposed XML Web Services methods for Patterns in Action.
/// 
/// SOA Patterns: Document Centric, Reservation, Unit Of Work, 
/// </summary>
[WebService(Namespace = "http://www.yourcompany.com/webservice/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    /// <summary>
    /// Logs user into the web service. 
    /// Note: Credentials are hardcoded in this example. Normally you 
    /// would need to validate against a database or Active Directory.
    /// </summary>
    /// <param name="request">Login request message.</param>
    /// <returns>Login response message.</returns>
    [WebMethod(EnableSession=true)]
    public LoginResponse Login(LoginRequest request)
    {
        LoginResponse response = new LoginResponse();

        // Set correlation Id
        response.CorrelationId = request.RequestId;
        
        try
        {
            // Validate credentials 
            if (request.SecurityToken != "ABC123")
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid Security Token.";
            }

            else if (!(request.UserName == "Jill" && request.Password == "LochNess24"))
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid Username or Password.";
            }

            else 
            {
                // Creates authentication ticket and adds it to cookies
                FormsAuthentication.SetAuthCookie(request.UserName, false);

                // Return Uri (could be used for a redirect to another server)
                response.Uri = Context.Request.Url.AbsoluteUri;

                // Return new SessionId
                response.SessionId = Context.Session.SessionID;
            }
        }
        catch // (Exception ex)
        {
            response.Acknowledge = AcknowledgeType.Failure;
            response.Message = "Login request cannot be handled at this time.";
        }

        return response;
    }

    /// <summary>
    /// Logs user out from the web service.
    /// </summary>
    /// <param name="request">Logout request message.</param>
    /// <returns>Logout response message.</returns>
    [WebMethod(EnableSession = true)]
    public LogoutResponse Logout(LogoutRequest request)
    {
        LogoutResponse response = new LogoutResponse();

        // Set correlation Id
        response.CorrelationId = request.RequestId;

        try
        {
            if (request.SecurityToken != "ABC123")
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid Security Token";
            }

            FormsAuthentication.SignOut();
        }
        catch // (Exception ex)
        {
            response.Acknowledge = AcknowledgeType.Failure;
            response.Message = "Logout request cannot be handled at this time.";
        }

        return response;
    }

    /// <summary>
    /// Gets a list of all customers in a given sort order.
    /// </summary>
    /// <param name="request">Customer request message.</param>
    /// <returns>Customer response message.</returns>
    [WebMethod(EnableSession = true)]
    public CustomerResponse GetCustomers(CustomerRequest request)
    {
        CustomerResponse response = new CustomerResponse();

        // Set correlation Id
        response.CorrelationId = request.RequestId;

        try
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Session expired. Please login.";
            }

            else if (request.SecurityToken != "ABC123")
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid Security Token.";

                // Forcibly abandon session
                Context.Session.Abandon();
            }

            else  
            {

                // Get customer list via Customer Facade.
                CustomerFacade facade = new CustomerFacade();
                IList<Customer> list = facade.GetCustomersWithOrderStatistics(request.SortExpression);

                response.Customers = new CustomerTransferObject[list.Count];

                // Package customer data in Customer Transfer Object
                int index = 0;
                foreach (Customer customer in list)
                {
                    CustomerTransferObject transfer = new CustomerTransferObject();

                    transfer.CustomerId = EncryptId(customer.CustomerId, "Customer");
                    transfer.Company = customer.Company;
                    transfer.City = customer.City;
                    transfer.Country = customer.Country;
                    transfer.NumOrders = customer.NumOrders;
                    transfer.LastOrderDate = customer.LastOrderDate;

                    response.Customers[index++] = transfer;
                }
            }
        }
        catch // (Exception ex)
        {
            response.Acknowledge = AcknowledgeType.Failure;
            response.Message = "Request cannot be handled at this time.";
        }

        return response;
    }

    /// <summary>
    /// Inserts, Updates, or Detetes a customer from the database.
    /// </summary>
    /// <param name="request">Persist customer request message.</param>
    /// <returns>Persist customer response message.</returns>
    [WebMethod(EnableSession = true)]
    public PersistCustomerResponse PersistCustomer(PersistCustomerRequest request)
    {
        PersistCustomerResponse response = new PersistCustomerResponse();

        // Set correlation Id
        response.CorrelationId = request.RequestId;

        try
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Session expired. Please login.";
            }

            else if (request.SecurityToken != "ABC123")
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid Security Token.";

                // Forcibly abandon session
                Context.Session.Abandon();
            }

            else if (PreviouslyRequested(request.RequestId))
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Request has been handled previously.";
            }

            else
            {
                // Call persistence request via Customer Facade.
                CustomerFacade facade = new CustomerFacade();

                switch (request.PersistAction)
                {
                    case PersistType.Insert:
                    {
                        Customer customer = new Customer();
                        customer.Company = request.Customer.Company;
                        customer.City = request.Customer.City;
                        customer.Country = request.Customer.Country;

                        facade.AddCustomer(customer);

                        response.Customer = request.Customer;
                        response.Customer.CustomerId = EncryptId(customer.CustomerId, "Customer");

                        break;
                    }
                    case PersistType.Update:
                    {
                        Customer customer = new Customer();

                        customer.CustomerId = DecryptId(request.Customer.CustomerId);
                        customer.Company = request.Customer.Company;
                        customer.City = request.Customer.City;
                        customer.Country = request.Customer.Country;

                        facade.UpdateCustomer(customer);

                        response.Customer = request.Customer;
                        break;
                    }
                    case PersistType.Delete:
                    {
                        int customerId = DecryptId(request.Customer.CustomerId);
                        int rowsAffected = facade.DeleteCustomer(customerId);
                        if (rowsAffected == 0)
                            throw new Exception("Customer has orders and therefore cannot be deleted. ");

                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            response.Acknowledge = AcknowledgeType.Failure;
            response.Message = ex.Message;
        }

        return response;
    }

    /// <summary>
    /// Gets all orders for a given customer.
    /// </summary>
    /// <param name="request">Order request message.</param>
    /// <returns>Order response message.</returns>
    [WebMethod(EnableSession = true)]
    public OrderResponse GetCustomerOrders(OrderRequest request)
    {
        OrderResponse response = new OrderResponse();

        // Set correlation Id
        response.CorrelationId = request.RequestId;

        try
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Session expired. Please login.";
            }

            else if (request.SecurityToken != "ABC123")
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid Security Token.";

                // Forcibly abandon session
                Context.Session.Abandon();
            }

            else
            {
                // Get orders via call into Customer Facade.
                CustomerFacade facade = new CustomerFacade();
                Customer customer = facade.GetCustomer(DecryptId(request.CustomerId));

                response.Orders = new OrderTransferObject[customer.Orders.Count];

                // Package order data in Order Transfer Object
                int i = 0;
                foreach (Order order in customer.Orders)
                {
                    OrderTransferObject transferOrder = new OrderTransferObject();

                    transferOrder.OrderId = EncryptId(order.OrderId, "Order");
                    transferOrder.OrderDate = order.OrderDate;
                    transferOrder.RequiredDate = order.RequiredDate;
                    transferOrder.Freight = order.Freight;

                    // Get order details via Customer Facade
                    Order details = facade.GetOrder(order.OrderId);
                    transferOrder.OrderDetails = new OrderDetailTransferObject[details.OrderDetails.Count];

                    // Package order detail data in Order Detail Transfer Object
                    int j = 0;
                    foreach (OrderDetail detail in details.OrderDetails)
                    {
                        OrderDetailTransferObject transferDetail = new OrderDetailTransferObject();

                        transferDetail.Product = detail.Product;
                        transferDetail.Quantity = detail.Quantity;
                        transferDetail.UnitPrice = detail.UnitPrice;
                        transferDetail.Discount = detail.Discount;

                        transferOrder.OrderDetails[j++] = transferDetail;
                    }

                    response.Orders[i++] = transferOrder;
                }
            }
        }
        catch // (Exception ex)
        {
            response.Acknowledge = AcknowledgeType.Failure;
            response.Message = "Request cannot be handled at this time.";
        }

        return response;
    }

    /// <summary>
    /// Determines if a UnitOfWork request was requested previously or not.
    /// In the 'real world' you would probably maintain UnitOfWork data in a 
    /// database. Private helper method.
    /// </summary>
    /// <param name="unitOfWork">Unit of work number.</param>
    /// <returns>Value indicating previously requested or not.</returns>
    private bool PreviouslyRequested(string unitOfWork)
    {
        // Check if UnitOfWork hashtable has already been established
        if (Context.Session["UnitOfWork"] == null)
        {
            Context.Session["UnitOfWork"] = new Hashtable();
        }

        Hashtable ht = Context.Session["UnitOfWork"] as Hashtable;
        if (ht.Contains(unitOfWork)) return true;

        ht.Add(unitOfWork, DateTime.Now);
        return false;
    }

    /// <summary>
    /// Encrypts internal identifier before sending it out to client.
    /// Private helper method.
    /// </summary>
    /// <param name="id">Identifier to be encrypted.</param>
    /// <param name="tableName">Database table in which identifier resides.</param>
    /// <returns>Encrypted stringified identifier.</returns>
    private string EncryptId(int id, string tableName)
    {
        string s = id.ToString() + "|" + tableName;
        return Crypto.ActionEncrypt(s);
    }

    /// <summary>
    /// Decrypts identifiers that come back from client.
    /// Private helper method.
    /// </summary>
    /// <param name="sid">Stringified, encrypted identifier.</param>
    /// <returns>Internal identifier.</returns>
    private int DecryptId(string sid)
    {
        string s = Crypto.ActionDecrypt(sid);
        s = s.Substring(0, s.IndexOf("|"));
        return int.Parse(s);
    }
}
