<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="WebAdmin_Order" Title="Customer Orders" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
    <!-- Content header and back button -->
      
    <ul class="zone">
      <li class="zoneleft"><h1>Customer Orders</h1></li>
      <li class="zoneright"><asp:HyperLink ID="HyperLinkBack" runat="server"  NavigateUrl="Orders.aspx" Text="&lt; back"></asp:HyperLink></li>
    </ul>
    <div style="clear:both"></div>
      
    <!-- Customer name label -->
      
    <h3><asp:Label ID="LabelCustomerName" Runat="server" /></h3><br />
      
    <!-- Orders GridView -->
      
    <asp:GridView id="GridViewOrders" runat="server" 
       AutoGenerateColumns="False" Width="600">
      <Columns>
       <asp:BoundField HeaderText="Order Id" DataField="OrderId"/>
       <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
       <asp:BoundField HeaderText="Required Date" DataField="RequiredDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
       <asp:BoundField HeaderText="Shipping" DataField="Freight" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" HtmlEncode="False" />
       <asp:HyperLinkField HeaderText="Items" HeaderStyle-HorizontalAlign="Center" DataNavigateUrlFields="OrderId" DataNavigateUrlFormatString="OrderDetails.aspx?id={0}" Text="View" ItemStyle-HorizontalAlign="Center" />
      </Columns>
    </asp:GridView>
      
    <!-- ObjectDataSource for orders -->
      
    <asp:ObjectDataSource ID="ObjectDataSourceOrders" runat="Server" 
       SelectMethod="GetOrders" TypeName="DoFactory.BusinessLayer.Facade.CustomerFacade">
       <SelectParameters>
          <asp:QueryStringParameter Name="customerId" QueryStringField="id" Type="Int32" />
       </SelectParameters>  
    </asp:ObjectDataSource>

    <br /><br />

</asp:Content>

