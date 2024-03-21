<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="WebAdmin_OrderDetails" Title="Order Details" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- Content header and back button -->

    <ul class="zone">
      <li class="zoneleft"><h1>Order Items</h1></li>
      <li class="zoneright"><asp:HyperLink ID="HyperLinkBack" runat="server"  NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back"></asp:HyperLink></li>
    </ul>
    <div style="clear:both"></div>
      
    <!-- Customer name and order date -->
     
    <h3><asp:Label ID="LabelCustomerName" Runat="server" /></h3>
    <h3><asp:Label ID="LabelOrderDate" Runat="server" /></h3><br />  
      
    <!-- Order details GridView -->
            
	<asp:GridView id="GridViewOrderDetails" runat="server" 
          AutoGenerateColumns="False" Width="600">
      <Columns>
       <asp:BoundField HeaderText="Product" DataField="Product" />
       <asp:BoundField HeaderText="Quantity" DataField="Quantity"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
       <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right"  />
       <asp:BoundField HeaderText="Discount" DataField="Discount" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" />
      </Columns>
    </asp:GridView>
    
    <br /><br />
    
</asp:Content>

