using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WindowsSOAClient.PatternAction;

namespace WindowsSOAClient
{
    /// <summary>
    /// Main window for windows SOA client. All business logic resides 
    /// in this window as it responds to local control events, menu events, and 
    /// closed dialog events. There is usually the preferred model, unless the 
    /// child windows have significant processing requirements, then they handle 
    /// that themselves. 
    /// 
    /// All communications required for this application runs via the Web Service; 
    /// there is no need for partitioning the client side up in logical layers because
    /// there is just one: the presentation layer.
    /// 
    /// If the need arises to store data locally (database or disk on the client side)
    /// then you would design this application using a 3-tier model similar to what 
    /// exists in the web application.
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// The (all important) Web service proxy object.
        /// Design Pattern: Proxy Design Pattern. 
        /// </summary>
        private PatternAction.Service proxy;

        /// <summary>
        /// Security token used for every request going to web service.
        /// </summary>
        private string SecurityToken;

        /// <summary>
        /// Default form constructor. 
        /// </summary>
        public FormMain()
        {
            // Create web service proxy object 
            proxy = new PatternAction.Service();

            // Proxy must accept and hold cookies
            proxy.CookieContainer = new System.Net.CookieContainer();
            proxy.Url = new Uri(proxy.Url).AbsoluteUri;

            // Save current web service url in ServiceUrl helper class.
            ServiceUrl.Url = proxy.Url;

            InitializeComponent();

            // Setup root node in customer tree
            TreeNode node = new TreeNode();
            node.Text = "Customers";
            node.Tag = "root";
            this.treeViewCustomer.Nodes.Add(node);
        }

        /// <summary>
        /// Displays login dialog box and loads customer list in treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormLogin form = new FormLogin())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Set url in case it was changed by user
                    proxy.Url = ServiceUrl.Url;

                    // Save off security token
                    this.SecurityToken = form.SecurityToken;

                    LoginRequest request = new LoginRequest();
                    request.UserName = form.UserName;
                    request.Password = form.Password;
                    request.SecurityToken = SecurityToken;
                    request.RequestId = Guid.NewGuid().ToString();

                    // Issue login request to web service
                    LoginResponse response = proxy.Login(request);

                    if (response.Acknowledge == AcknowledgeType.Success)
                    {
                        CustomerRequest cRequest = new CustomerRequest();
                        cRequest.SecurityToken = this.SecurityToken;
                        cRequest.SortExpression = "CompanyName ASC";

                        // Issue customer list request to web service
                        CustomerResponse cResponse = proxy.GetCustomers(cRequest);

                        if (cResponse.Acknowledge == AcknowledgeType.Success)
                        {
                            // Unpack customer transfer objects into customer business objects
                            IList<Customer> list = new List<Customer>();
                            foreach (CustomerTransferObject transferObject in cResponse.Customers)
                            {
                                Customer customer = new Customer();
                                customer.CustomerId = transferObject.CustomerId;
                                customer.Company = transferObject.Company;
                                customer.City = transferObject.City;
                                customer.Country = transferObject.Country;
                                customer.Orders = null;

                                list.Add(customer);
                            }

                            // Clear nodes under root of tree
                            TreeNode root = treeViewCustomer.Nodes[0];
                            root.Nodes.Clear();

                            // Build the customer tree
                            foreach (Customer customer in list)
                            {
                                AddCustomerToTree(customer);
                            }

                            // Display tree expanded
                            treeViewCustomer.ExpandAll();

                            // Enable customer add/edit/delete menu items.
                            this.addToolStripMenuItem.Enabled = true;
                            this.editToolStripMenuItem.Enabled = true;
                            this.deleteToolStripMenuItem.Enabled = true;
                        }
                        else
                        {
                            // Customer retrieval failure
                            MessageBox.Show("Customer retrieval failed", cResponse.Message);
                        }
                    }
                    else
                    {
                        // Login failure
                        MessageBox.Show("Login failed", response.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Logs user off, empties datagridviews, and disables menus.
        /// </summary>
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogoutRequest request = new LogoutRequest();
            request.SecurityToken = SecurityToken;
            request.RequestId = Guid.NewGuid().ToString();

            // Issue logout request to web service
            LogoutResponse response = proxy.Logout(request);

            if (response.Acknowledge == AcknowledgeType.Failure)
            {
                MessageBox.Show("Logout failed: " + response.Message);
                return;
            }

            // Clear form elements
            treeViewCustomer.Nodes[0].Nodes.Clear();
            dataGridViewOrders.DataSource = null;
            dataGridViewOrderDetails.DataSource = null;

            // Disable menu items.
            this.addToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Enabled = false;
            this.deleteToolStripMenuItem.Enabled = false;

        }

        /// <summary>
        /// Exits application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Retrieves order data from the web service for the customer currently selected.
        /// If, however, orders were retrieved previously, then these will be displayed. 
        /// The effect this has is that the client application speeds up over time. 
        /// </summary>
        private void treeViewCustomer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Customer customer = treeViewCustomer.SelectedNode.Tag as Customer;
            
            // Check for root node, which does not have a customer record
            if (customer == null) return;

            // Check if orders were already retrieved for customer
            if (customer.Orders != null)
            {
                BindOrders(customer.Orders);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            OrderRequest request = new OrderRequest();
            request.SecurityToken = this.SecurityToken;
            request.RequestId = Guid.NewGuid().ToString();
            request.CustomerId = customer.CustomerId;

            // Issue order request to web service
            OrderResponse response = proxy.GetCustomerOrders(request);
            if (response.Acknowledge == AcknowledgeType.Failure)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Order retrieve error: " + response.Message);
                return;
            }

            // Unpack order transfer objects into order business objects.
            IList<Order> orders = new List<Order>();
            foreach (OrderTransferObject transfer in response.Orders)
            {
                Order order = new Order();
                order.OrderId = transfer.OrderId;
                order.OrderDate = transfer.OrderDate;
                order.RequiredDate = transfer.RequiredDate;
                order.Freight = transfer.Freight;

                // Unpack order detail tranfer objects into order detail business objects
                IList<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (OrderDetailTransferObject transferDetail in transfer.OrderDetails)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.Quantity = transferDetail.Quantity;
                    orderDetail.Product = transferDetail.Product;
                    orderDetail.UnitPrice = transferDetail.UnitPrice;
                    orderDetail.Discount = transferDetail.Discount;

                    orderDetails.Add(orderDetail);
                }

                order.OrderDetails = orderDetails;
                orders.Add(order);
                
            }

            // Store orders for next time this customer is selected.
            customer.Orders = orders;

            BindOrders(orders);

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Databinds orders to dataGridView control.
        /// Private helper method.
        /// </summary>
        /// <param name="orders">Order list.</param>
        private void BindOrders(IList<Order> orders)
        {
            dataGridViewOrders.DataSource = orders;

            dataGridViewOrders.Columns["Customer"].Visible = false;
            dataGridViewOrders.Columns["OrderDetails"].Visible = false;

            dataGridViewOrders.Columns["Freight"].DefaultCellStyle.Format = "C";
            dataGridViewOrders.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.BackColor = Color.Cornsilk;
            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.Font = new Font(dataGridViewOrders.Font,FontStyle.Bold);

            dataGridViewOrders.Columns["OrderDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Displays order details (line items) that are part of the currently 
        /// selected order. 
        /// </summary>
        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewOrderDetails.DataSource = null;

            if (dataGridViewOrders.SelectedRows.Count == 0) return;
            
            DataGridViewRow row = dataGridViewOrders.SelectedRows[0];
            if (row == null) return;
            
            string orderId = row.Cells["OrderId"].Value as String;

            // Get customer record from treeview control.
            Customer customer = treeViewCustomer.SelectedNode.Tag as Customer;
            
            // Check for root node. It does not have a customer record
            if (customer == null) return;

            // Locate order record
            foreach (Order order in customer.Orders)
            {
                if (order.OrderId == orderId)
                {
                    if (order.OrderDetails.Count == 0) return;
                    
                    dataGridViewOrderDetails.DataSource = order.OrderDetails;
                    
                    dataGridViewOrderDetails.Columns["Order"].Visible = false;

                    dataGridViewOrderDetails.Columns["Discount"].DefaultCellStyle.Format = "C";
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "C";
                    dataGridViewOrderDetails.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataGridViewOrderDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.BackColor = Color.Cornsilk;
                    
                    
                    return;
                }
            }
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormCustomer form = new FormCustomer())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Setup request
                    PersistCustomerRequest request = new PersistCustomerRequest();
                    request.SecurityToken = SecurityToken;
                    request.RequestId = Guid.NewGuid().ToString();
                    request.PersistAction = PersistType.Insert;

                    // Package customer data in customer transfer object
                    CustomerTransferObject customerTransfer = new CustomerTransferObject();
                    customerTransfer.Company = form.Company;
                    customerTransfer.City = form.City;
                    customerTransfer.Country = form.Country;
                    request.Customer = customerTransfer;

                    // Issue persist request to web service
                    PersistCustomerResponse response = proxy.PersistCustomer(request);

                    if (response.Acknowledge == AcknowledgeType.Success)
                    {
                        // Unpack customer transfer object into customer business object
                        Customer customer = new Customer();
                        customer.CustomerId = response.Customer.CustomerId;
                        customer.Company = response.Customer.Company;
                        customer.City = response.Customer.City;
                        customer.Country = response.Customer.Country;
                        customer.Orders = null;

                        TreeNode node = AddCustomerToTree(customer);

                        // Highlight new node
                        treeViewCustomer.SelectedNode = node;

                    }
                    else
                    {
                        MessageBox.Show("Error saving customer: " + response.Message,
                            "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Private helper function that appends a customer to the next node
        /// in the treeview
        /// </summary>
        private TreeNode AddCustomerToTree(Customer customer)
        {
            TreeNode node = new TreeNode();
            node.Text = customer.Company + " (" + customer.Country + ")";
            node.Tag = customer;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            this.treeViewCustomer.Nodes[0].Nodes.Add(node);

            return node;
        }

        /// <summary>
        /// Edits an existing customer record.
        /// </summary>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a node is selected (and not the root)
            if (treeViewCustomer.SelectedNode == null || 
                treeViewCustomer.SelectedNode.Text == "Customers")
            {
                MessageBox.Show("No customer is currently selected",
                            "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (FormCustomer form = new FormCustomer())
            {
                Customer cust = treeViewCustomer.SelectedNode.Tag as Customer;
                form.Company = cust.Company;
                form.City = cust.City;
                form.Country = cust.Country;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Setup request
                    PersistCustomerRequest request = new PersistCustomerRequest();
                    request.SecurityToken = SecurityToken;
                    request.RequestId = Guid.NewGuid().ToString();
                    request.PersistAction = PersistType.Update;

                    // Package customer data in customer transfer object.
                    CustomerTransferObject customerTransfer = new CustomerTransferObject();
                    customerTransfer.CustomerId = cust.CustomerId;
                    customerTransfer.Company = form.Company;
                    customerTransfer.City = form.City;
                    customerTransfer.Country = form.Country;
                    request.Customer = customerTransfer;

                    // Issue persist request to web service.
                    PersistCustomerResponse response = proxy.PersistCustomer(request);

                    if (response.Acknowledge == AcknowledgeType.Success)
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = response.Customer.CustomerId;
                        customer.Company = response.Customer.Company;
                        customer.City = response.Customer.City;
                        customer.Country = response.Customer.Country;
                        customer.Orders = null;

                        // Update selected node
                        treeViewCustomer.SelectedNode.Text = customer.Company + " (" + customer.Country + ")";
                        treeViewCustomer.SelectedNode.Tag = customer;
                    }
                    else
                    {
                        MessageBox.Show("Error saving customer: " + response.Message,
                            "Edit error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a node is selected (and not the root)
            if (treeViewCustomer.SelectedNode == null ||
                treeViewCustomer.SelectedNode.Text == "Customers")
            {
                MessageBox.Show("No customer is currently selected",
                            "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (FormCustomer form = new FormCustomer())
            {
                // Setup request
                PersistCustomerRequest request = new PersistCustomerRequest();
                request.SecurityToken = SecurityToken;
                request.RequestId = Guid.NewGuid().ToString();
                request.PersistAction = PersistType.Delete;

                // Package customer data in customer transfer object.
                Customer customer = treeViewCustomer.SelectedNode.Tag as Customer;
                CustomerTransferObject customerTransfer = new CustomerTransferObject();
                customerTransfer.CustomerId = customer.CustomerId;
                customerTransfer.Company = customer.Company;
                customerTransfer.City = customer.City;
                customerTransfer.Country = customer.Country;
                request.Customer = customerTransfer;

                // Issue persist request to web service.
                PersistCustomerResponse response = proxy.PersistCustomer(request);

                if (response.Acknowledge == AcknowledgeType.Success)
                {
                    // Remove deleted node from tree
                    treeViewCustomer.Nodes[0].Nodes.Remove(treeViewCustomer.SelectedNode);
                }
                else
                {
                    MessageBox.Show( response.Message,
                       "Delete customer", MessageBoxButtons.OK, 
                       MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Redirects add customer request to equivalent menu event handler.
        /// </summary>
        private void addNewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addToolStripMenuItem.Enabled)
                addToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// Redirects edit customer request to equivalent menu event handler.
        /// </summary>
        private void editCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Enabled) 
                editToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// Redirects delete customer request to equivalent menu event handler.
        /// </summary>
        private void deleteCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Enabled) 
                deleteToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// Selects clicked node and then displays context menu. The tree node selection
        /// is important here because this does not happen by default in this event. 
        /// </summary>
        private void treeViewCustomer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeViewCustomer.SelectedNode =
                    treeViewCustomer.GetNodeAt(e.Location);

                contextMenuStripCustomer.Show((Control)sender, e.Location);
            }
        }

        /// <summary>
        /// Redirects login request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonLogin_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects logout request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonLogout_Click(object sender, EventArgs e)
        {
            logoutToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects add customer request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (addToolStripMenuItem.Enabled)
                addToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects edit customer request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Enabled)
                editToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects delete customer request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (deleteToolStripMenuItem.Enabled)
                deleteToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Opens the about dialog window.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Not implemented... ", "Help");
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Not implemented... ", "Help");
        }
    }
}