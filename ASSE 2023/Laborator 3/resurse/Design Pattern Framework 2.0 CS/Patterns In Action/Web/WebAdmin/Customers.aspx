<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="WebAdmin_Customers" Title="Customers" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <h1>Customers</h1>
    
    <!-- Error message and Add new Customer button -->
    
    <ul id="customers">
      <li id="customerserror"><asp:Label ID="LabelError" runat="server" Text="" EnableViewState="false"></asp:Label></li>
      <li id="addnewcustomer"><asp:HyperLink ID="HyperLinkNewCustomer" runat="Server"
          text="Add new Customer" NavigateUrl="~/WebAdmin/Customer.aspx?id=0" /></li>
    </ul>
    
    <!-- Customer GridView -->
    
    <asp:GridView ID="GridViewCustomers" runat="server"
        AutoGenerateColumns="False" Width="600"
        AllowSorting="True"
        OnSorting="GridViewCustomers_Sorting"
        OnRowCreated="GridViewCustomers_RowCreated" 
        OnRowDeleting="GridViewCustomers_RowDeleting" >
        <Columns>
    		<asp:BoundField HeaderText="Id" DataField="CustomerId" SortExpression="CustomerId" />
			<asp:BoundField HeaderText="Customer Name" DataField="Company" SortExpression="CompanyName" />
			<asp:BoundField HeaderText="City" DataField="City" SortExpression="City" />
			<asp:BoundField HeaderText="Country" DataField="Country" SortExpression="Country" />
			<asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="CustomerId" DataNavigateUrlFormatString="Customer.aspx?id={0}" 
                Text="Edit" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
			<asp:CommandField HeaderText="Delete" ButtonType="Link"  
			    ShowDeleteButton="True" ControlStyle-ForeColor="#990000" />
        </Columns>
    </asp:GridView>

    <br /><br />

</asp:Content>

