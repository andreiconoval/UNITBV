<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="WebShop_Checkout" Title="Checkout" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Checkout</h1>

    <p>
    This is where you would proceed to a checkout process. <br /> 
    Checkout includes collecting information on address, shipping, payment, etc.
    </p>
	
    <ul>
     <li>
	   <asp:HyperLink ID="HyperLinkHome" runat="server" NavigateUrl="~/Default.aspx" Text="Click here"></asp:HyperLink> to return to home page
     </li>
    </ul>

</asp:Content>

