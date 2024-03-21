<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="WebAdmin_Orders" Title="Customers and Orders" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Orders by Customer</h1>

    <!-- Customers wit Order Summary GridView -->
    
    <asp:GridView ID="GridViewOrders" runat="server" 
        AutoGenerateColumns="False" Width="640"
        AllowSorting="True"
        OnSorting="GridViewOrders_Sorting"
        OnRowCreated="GridViewOrders_RowCreated" >

        <Columns>
    		<asp:BoundField HeaderText="Id" DataField="CustomerId" SortExpression="CustomerId" />
			<asp:BoundField HeaderText="Customer Name" DataField="Company" SortExpression="CompanyName" />
			<asp:BoundField HeaderText="City" DataField="City" SortExpression="City" />
			<asp:BoundField HeaderText="Country" DataField="Country" SortExpression="Country" />
			<asp:BoundField HeaderText="# Orders" DataField="NumOrders" SortExpression="NumOrders" ItemStyle-HorizontalAlign="Center" />
			<asp:BoundField HeaderText="Last Order" DataField="LastOrderDate" SortExpression="LastOrderDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
			<asp:HyperLinkField HeaderText="Orders" DataNavigateUrlFields="CustomerId" DataNavigateUrlFormatString="Order.aspx?id={0}" 
                Text="View" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
        </Columns>
    </asp:GridView>

    <br /><br />

</asp:Content>

