<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Patterns In Action: Design Patterns" %>
<%@ MasterType TypeName="MasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Welcome</h1>
    
    <!-- image -->
    
    <div class="floatright">
      <asp:Image ID="ImageDefault" runat="server" ImageUrl="~/Images/Site/default.jpg" Width="162" Height="232" BorderWidth="0" />
    </div>
        
	<p>
	Thank you for purchasing the Design Pattern Framework.
	You are currently running the <strong>Patterns in Action</strong> ASP.NET reference
	application which demonstrates when, where, and how design patterns are 
	most effectively used in a modern 3-tier e-commerce web application. 
	</p>
	
	<p>
	This application has been built around the most frequently used 
	Gang of Four (GoF) design patterns and associated best practice architectures. 
	Also included are numerous Enterprise Design Patterns 
	as documented in Martin Fowler's book titled: 
     "<a href="JavaScript:openwindow('http://www.martinfowler.com/books.html#eaa','mf0','790','460');">Patterns of Enterprise Application Architecture</a>".
	</p> 
	<br />
    <b>Functionality, Design, & Patterns</b>
    <br /><br />
	<p>
    As a first step, we recommend that you familiarize yourself with the 
    functionality of this application. Select the menu items on the left and 
    explore the options.  Secondly, we suggest you analyze the .NET Solution 
    and Project structure. This will provide a glimpse at the 3-tier architecture 
    and some of the major pattern used to build this reference application. Once you 
    have an understanding of the overall design and architecture you'll want to explore  
    the details which includes numerous design patterns used in the application.  
    As a last step, you can explore the Web Service with associated 
    Windows SOA client application.
    </p>
    
    <p>
    <b>Documentation</b>
    <br /><br />
    
    Setup, functionality, design, architecture, and design patterns are discussed in
    the accompanying document named: <b>PatternsInAction.pdf</b>. 
    Furthermore, in the .NET Solution under a folder named \Solution Items\Documention\ you'll find 
    <b>PatternsInAction.chm</b>, which is a reference guide of all types
    (classes, methods, interfaces, enums, etc) used in the application. 
    The code itself is well commented. Finally, all projects come with 
    a class diagram. They are located in a folder named \_UML Diagram\.
    </p>
    
    <p>
    We are hopeful that <strong>Patterns in Action</strong> will be a great 
    learning experience on design patterns in the real world. 
    </p>
	<br /><br />

</asp:Content>

