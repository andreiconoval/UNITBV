<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="WebShop_Search" Title="Search Products" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Search Products</h1>
    
    <%-- Search criteria --%>

    Product Name: 
     <asp:TextBox ID="TextBoxProductName" runat="server" Width="100"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    Price range: 
    <asp:DropDownList ID="DropDownListRange" runat="server" Width="130" DataSourceID="ObjectDataSourcePriceRange" DataTextField="RangeText" DataValueField="RangeId">
      <asp:ListItem Selected="True" Value="0"></asp:ListItem>
    </asp:DropDownList>
    
    &nbsp;&nbsp;<asp:Button ID="ButtonSearch" runat="server" Text=" Find " UseSubmitBehavior="true" OnClick="ButtonSearch_Click" /><br />

    
    <hr />
    <br />

    <%--  Price range ObjectDataSource --%>

    <asp:ObjectDataSource runat="Server" ID="ObjectDataSourcePriceRange"
        SelectMethod="GetProductPriceRange" TypeName="DoFactory.BusinessLayer.Facade.ProductFacade" >
    </asp:ObjectDataSource>
 
    <%--  Search results --%>
    
    <asp:GridView ID="GridViewProducts" runat="server"
        AutoGenerateColumns="False" Width="600"
        AllowSorting="True"
        OnSorting="GridViewProducts_Sorting"
        OnRowCreated="GridViewProducts_RowCreated" 
        EmptyDataText="No products found. Please try again">
    
        <Columns>
    		<asp:BoundField HeaderText="Id" DataField="ProductId"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="ProductId" />
			<asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" />
			<asp:BoundField HeaderText="Price" DataField="UnitPrice" DataFormatString="{0:c}" SortExpression="UnitPrice" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  />
			<asp:HyperLinkField HeaderText="Details" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="Product.aspx?id={0}" 
                Text="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:HyperLinkField>
        </Columns>
        <EmptyDataRowStyle Font-Bold="True" BackColor="FloralWhite" />
    </asp:GridView>
    
    <br /><br />

</asp:Content>

