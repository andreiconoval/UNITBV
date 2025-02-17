﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WindowsSOAClient.PatternAction {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://www.yourcompany.com/webservice/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResponseBase))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RequestBase))]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback LogoutOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCustomersOperationCompleted;
        
        private System.Threading.SendOrPostCallback PersistCustomerOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCustomerOrdersOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::WindowsSOAClient.Properties.Settings.Default.WindowsSOAClient_PatternAction_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event LoginCompletedEventHandler LoginCompleted;
        
        /// <remarks/>
        public event LogoutCompletedEventHandler LogoutCompleted;
        
        /// <remarks/>
        public event GetCustomersCompletedEventHandler GetCustomersCompleted;
        
        /// <remarks/>
        public event PersistCustomerCompletedEventHandler PersistCustomerCompleted;
        
        /// <remarks/>
        public event GetCustomerOrdersCompletedEventHandler GetCustomerOrdersCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yourcompany.com/webservice/Login", RequestNamespace="http://www.yourcompany.com/webservice/", ResponseNamespace="http://www.yourcompany.com/webservice/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LoginResponse Login(LoginRequest request) {
            object[] results = this.Invoke("Login", new object[] {
                        request});
            return ((LoginResponse)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(LoginRequest request) {
            this.LoginAsync(request, null);
        }
        
        /// <remarks/>
        public void LoginAsync(LoginRequest request, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
                        request}, this.LoginOperationCompleted, userState);
        }
        
        private void OnLoginOperationCompleted(object arg) {
            if ((this.LoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginCompleted(this, new LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yourcompany.com/webservice/Logout", RequestNamespace="http://www.yourcompany.com/webservice/", ResponseNamespace="http://www.yourcompany.com/webservice/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LogoutResponse Logout(LogoutRequest request) {
            object[] results = this.Invoke("Logout", new object[] {
                        request});
            return ((LogoutResponse)(results[0]));
        }
        
        /// <remarks/>
        public void LogoutAsync(LogoutRequest request) {
            this.LogoutAsync(request, null);
        }
        
        /// <remarks/>
        public void LogoutAsync(LogoutRequest request, object userState) {
            if ((this.LogoutOperationCompleted == null)) {
                this.LogoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogoutOperationCompleted);
            }
            this.InvokeAsync("Logout", new object[] {
                        request}, this.LogoutOperationCompleted, userState);
        }
        
        private void OnLogoutOperationCompleted(object arg) {
            if ((this.LogoutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogoutCompleted(this, new LogoutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yourcompany.com/webservice/GetCustomers", RequestNamespace="http://www.yourcompany.com/webservice/", ResponseNamespace="http://www.yourcompany.com/webservice/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CustomerResponse GetCustomers(CustomerRequest request) {
            object[] results = this.Invoke("GetCustomers", new object[] {
                        request});
            return ((CustomerResponse)(results[0]));
        }
        
        /// <remarks/>
        public void GetCustomersAsync(CustomerRequest request) {
            this.GetCustomersAsync(request, null);
        }
        
        /// <remarks/>
        public void GetCustomersAsync(CustomerRequest request, object userState) {
            if ((this.GetCustomersOperationCompleted == null)) {
                this.GetCustomersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCustomersOperationCompleted);
            }
            this.InvokeAsync("GetCustomers", new object[] {
                        request}, this.GetCustomersOperationCompleted, userState);
        }
        
        private void OnGetCustomersOperationCompleted(object arg) {
            if ((this.GetCustomersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCustomersCompleted(this, new GetCustomersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yourcompany.com/webservice/PersistCustomer", RequestNamespace="http://www.yourcompany.com/webservice/", ResponseNamespace="http://www.yourcompany.com/webservice/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PersistCustomerResponse PersistCustomer(PersistCustomerRequest request) {
            object[] results = this.Invoke("PersistCustomer", new object[] {
                        request});
            return ((PersistCustomerResponse)(results[0]));
        }
        
        /// <remarks/>
        public void PersistCustomerAsync(PersistCustomerRequest request) {
            this.PersistCustomerAsync(request, null);
        }
        
        /// <remarks/>
        public void PersistCustomerAsync(PersistCustomerRequest request, object userState) {
            if ((this.PersistCustomerOperationCompleted == null)) {
                this.PersistCustomerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPersistCustomerOperationCompleted);
            }
            this.InvokeAsync("PersistCustomer", new object[] {
                        request}, this.PersistCustomerOperationCompleted, userState);
        }
        
        private void OnPersistCustomerOperationCompleted(object arg) {
            if ((this.PersistCustomerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PersistCustomerCompleted(this, new PersistCustomerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.yourcompany.com/webservice/GetCustomerOrders", RequestNamespace="http://www.yourcompany.com/webservice/", ResponseNamespace="http://www.yourcompany.com/webservice/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public OrderResponse GetCustomerOrders(OrderRequest request) {
            object[] results = this.Invoke("GetCustomerOrders", new object[] {
                        request});
            return ((OrderResponse)(results[0]));
        }
        
        /// <remarks/>
        public void GetCustomerOrdersAsync(OrderRequest request) {
            this.GetCustomerOrdersAsync(request, null);
        }
        
        /// <remarks/>
        public void GetCustomerOrdersAsync(OrderRequest request, object userState) {
            if ((this.GetCustomerOrdersOperationCompleted == null)) {
                this.GetCustomerOrdersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCustomerOrdersOperationCompleted);
            }
            this.InvokeAsync("GetCustomerOrders", new object[] {
                        request}, this.GetCustomerOrdersOperationCompleted, userState);
        }
        
        private void OnGetCustomerOrdersOperationCompleted(object arg) {
            if ((this.GetCustomerOrdersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCustomerOrdersCompleted(this, new GetCustomerOrdersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class LoginRequest : RequestBase {
        
        private string userNameField;
        
        private string passwordField;
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderRequest))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PersistCustomerRequest))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomerRequest))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LogoutRequest))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoginRequest))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class RequestBase {
        
        private string securityTokenField;
        
        private string versionField;
        
        private string requestIdField;
        
        /// <remarks/>
        public string SecurityToken {
            get {
                return this.securityTokenField;
            }
            set {
                this.securityTokenField = value;
            }
        }
        
        /// <remarks/>
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        public string RequestId {
            get {
                return this.requestIdField;
            }
            set {
                this.requestIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PersistCustomerResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomerResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LogoutResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoginResponse))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class ResponseBase {
        
        private AcknowledgeType acknowledgeField;
        
        private string correlationIdField;
        
        private string messageField;
        
        private string reservationIdField;
        
        private System.DateTime reservationExpiresField;
        
        private string versionField;
        
        private string buildField;
        
        /// <remarks/>
        public AcknowledgeType Acknowledge {
            get {
                return this.acknowledgeField;
            }
            set {
                this.acknowledgeField = value;
            }
        }
        
        /// <remarks/>
        public string CorrelationId {
            get {
                return this.correlationIdField;
            }
            set {
                this.correlationIdField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string ReservationId {
            get {
                return this.reservationIdField;
            }
            set {
                this.reservationIdField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ReservationExpires {
            get {
                return this.reservationExpiresField;
            }
            set {
                this.reservationExpiresField = value;
            }
        }
        
        /// <remarks/>
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        public string Build {
            get {
                return this.buildField;
            }
            set {
                this.buildField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public enum AcknowledgeType {
        
        /// <remarks/>
        Failure,
        
        /// <remarks/>
        Success,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class OrderResponse : ResponseBase {
        
        private OrderTransferObject[] ordersField;
        
        /// <remarks/>
        public OrderTransferObject[] Orders {
            get {
                return this.ordersField;
            }
            set {
                this.ordersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class OrderTransferObject {
        
        private string orderIdField;
        
        private System.DateTime orderDateField;
        
        private System.DateTime requiredDateField;
        
        private float freightField;
        
        private OrderDetailTransferObject[] orderDetailsField;
        
        /// <remarks/>
        public string OrderId {
            get {
                return this.orderIdField;
            }
            set {
                this.orderIdField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime OrderDate {
            get {
                return this.orderDateField;
            }
            set {
                this.orderDateField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime RequiredDate {
            get {
                return this.requiredDateField;
            }
            set {
                this.requiredDateField = value;
            }
        }
        
        /// <remarks/>
        public float Freight {
            get {
                return this.freightField;
            }
            set {
                this.freightField = value;
            }
        }
        
        /// <remarks/>
        public OrderDetailTransferObject[] OrderDetails {
            get {
                return this.orderDetailsField;
            }
            set {
                this.orderDetailsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class OrderDetailTransferObject {
        
        private string productField;
        
        private int quantityField;
        
        private float unitPriceField;
        
        private float discountField;
        
        /// <remarks/>
        public string Product {
            get {
                return this.productField;
            }
            set {
                this.productField = value;
            }
        }
        
        /// <remarks/>
        public int Quantity {
            get {
                return this.quantityField;
            }
            set {
                this.quantityField = value;
            }
        }
        
        /// <remarks/>
        public float UnitPrice {
            get {
                return this.unitPriceField;
            }
            set {
                this.unitPriceField = value;
            }
        }
        
        /// <remarks/>
        public float Discount {
            get {
                return this.discountField;
            }
            set {
                this.discountField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class PersistCustomerResponse : ResponseBase {
        
        private CustomerTransferObject customerField;
        
        /// <remarks/>
        public CustomerTransferObject Customer {
            get {
                return this.customerField;
            }
            set {
                this.customerField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class CustomerTransferObject {
        
        private string customerIdField;
        
        private string companyField;
        
        private string cityField;
        
        private string countryField;
        
        private int numOrdersField;
        
        private System.DateTime lastOrderDateField;
        
        private OrderTransferObject[] ordersField;
        
        /// <remarks/>
        public string CustomerId {
            get {
                return this.customerIdField;
            }
            set {
                this.customerIdField = value;
            }
        }
        
        /// <remarks/>
        public string Company {
            get {
                return this.companyField;
            }
            set {
                this.companyField = value;
            }
        }
        
        /// <remarks/>
        public string City {
            get {
                return this.cityField;
            }
            set {
                this.cityField = value;
            }
        }
        
        /// <remarks/>
        public string Country {
            get {
                return this.countryField;
            }
            set {
                this.countryField = value;
            }
        }
        
        /// <remarks/>
        public int NumOrders {
            get {
                return this.numOrdersField;
            }
            set {
                this.numOrdersField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime LastOrderDate {
            get {
                return this.lastOrderDateField;
            }
            set {
                this.lastOrderDateField = value;
            }
        }
        
        /// <remarks/>
        public OrderTransferObject[] Orders {
            get {
                return this.ordersField;
            }
            set {
                this.ordersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class CustomerResponse : ResponseBase {
        
        private CustomerTransferObject[] customersField;
        
        /// <remarks/>
        public CustomerTransferObject[] Customers {
            get {
                return this.customersField;
            }
            set {
                this.customersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class LogoutResponse : ResponseBase {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class LoginResponse : ResponseBase {
        
        private string uriField;
        
        private string sessionIdField;
        
        /// <remarks/>
        public string Uri {
            get {
                return this.uriField;
            }
            set {
                this.uriField = value;
            }
        }
        
        /// <remarks/>
        public string SessionId {
            get {
                return this.sessionIdField;
            }
            set {
                this.sessionIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class OrderRequest : RequestBase {
        
        private string customerIdField;
        
        /// <remarks/>
        public string CustomerId {
            get {
                return this.customerIdField;
            }
            set {
                this.customerIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class PersistCustomerRequest : RequestBase {
        
        private PersistType persistActionField;
        
        private CustomerTransferObject customerField;
        
        /// <remarks/>
        public PersistType PersistAction {
            get {
                return this.persistActionField;
            }
            set {
                this.persistActionField = value;
            }
        }
        
        /// <remarks/>
        public CustomerTransferObject Customer {
            get {
                return this.customerField;
            }
            set {
                this.customerField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public enum PersistType {
        
        /// <remarks/>
        Insert,
        
        /// <remarks/>
        Update,
        
        /// <remarks/>
        Delete,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class CustomerRequest : RequestBase {
        
        private string sortExpressionField;
        
        /// <remarks/>
        public string SortExpression {
            get {
                return this.sortExpressionField;
            }
            set {
                this.sortExpressionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.yourcompany.com/webservice/")]
    public partial class LogoutRequest : RequestBase {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public LoginResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LoginResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void LogoutCompletedEventHandler(object sender, LogoutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogoutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogoutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public LogoutResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((LogoutResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetCustomersCompletedEventHandler(object sender, GetCustomersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCustomersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCustomersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CustomerResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CustomerResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void PersistCustomerCompletedEventHandler(object sender, PersistCustomerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PersistCustomerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PersistCustomerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PersistCustomerResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PersistCustomerResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetCustomerOrdersCompletedEventHandler(object sender, GetCustomerOrdersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCustomerOrdersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCustomerOrdersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public OrderResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((OrderResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591