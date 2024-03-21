<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="WebShop_Products" Title="Products" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Products</h1>

    <%-- Product Category --%>
    
    Select a Category:&nbsp; <asp:DropDownList ID="DropDownListCategories" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceCategories"
        DataTextField="Name" DataValueField="CategoryId" Width="130" OnSelectedIndexChanged="DropDownListCategories_SelectedIndexChanged"></asp:DropDownList>

    <asp:ObjectDataSource ID="ObjectDataSourceCategories" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetCategories" TypeName="DoFactory.BusinessLayer.Facade.ProductFacade" >
    </asp:ObjectDataSource><br /><br />

     
    <%-- Product List --%>
    
    <asp:GridView ID="GridViewProducts" runat="server" 
       AutoGenerateColumns="False" Width="600" 
       AllowSorting="True"   
       OnSorting="GridViewProducts_Sorting"
       OnRowCreated="GridViewProducts_RowCreated" >
       <Columns>
    	  <asp:BoundField HeaderText="Id" DataField="ProductId"  SortExpression="ProductId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
		  <asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" />
		  <asp:BoundField HeaderText="Weight" DataField="Weight" SortExpression="Weight" />
		  <asp:BoundField HeaderText="Price" DataField="UnitPrice" SortExpression="UnitPrice" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  />
		  <asp:HyperLinkField HeaderText="Details" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="Product.aspx?id={0}" 
               Text="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:HyperLinkField>
       </Columns>
    </asp:GridView>

    <br /><br />

</asp:Content>

