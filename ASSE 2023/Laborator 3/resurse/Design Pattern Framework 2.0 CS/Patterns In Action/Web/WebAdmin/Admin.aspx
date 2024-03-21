<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="WebAdmin_Admin" Title="Administration" %>
<%@ MasterType TypeName="MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Administration</h1>
    
    <p>Administration pages like these are usually not available to the public and need
       to be protected. You do this by building a password protected
       administration area accessible only to personnel in your company. The 
       new membership and roles functionality in .NET 2.0 is great for 
       this purpose. There are two main tasks in the administration module: 
    </p>
    
    <br />
    
    <ul>
     <li><a href="Customers.aspx">Click here</a> to maintain customers</li>
     <li><a href="Orders.aspx">Click here</a> to view orders</li>
	</ul>

</asp:Content>

