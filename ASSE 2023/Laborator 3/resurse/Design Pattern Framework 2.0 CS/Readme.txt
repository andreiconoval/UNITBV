Design Pattern Framework. Release 2.0. C# edition.

This file contains setup instructions, but first please read this 
brief copyright and license notice.

==================================================================
Copyright and license:
------------------------------------------------------------------

Copyright (C) 2006 Data & Object Factory.
All rights reserved. www.dofactory.com

If this is a single user license the product can only be used 
by a single user.  If you purchased the 16 user license then the 
product can be used by up to 16 developers concurrently.

You are free to use the source code in your own business 
application development, but remember that this product and the 
source code are intended for educational purposes only.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY 
OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT 
LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
FITNESS FOR A PARTICULAR PURPOSE.

==================================================================
Versions of VS and .NET:
------------------------------------------------------------------
The programs require Visual Studio 2005 and .NET 2.0.


==================================================================
Setup of the Design Pattern Framework:
------------------------------------------------------------------

Extract your zip file to the following location: 
C:\Program Files\DoFactory\Design Pattern Framework 2.0 CS\ 

Within this folder you will find 4 subfolders. They are: 

==================================================================
1) Gang of Four:
------------------------------------------------------------------

Contains 69 Gang-of-Four design pattern projects, i.e. Structural, 
Real world, and .NET optimized.

Create a shortcut to "DoFactory.GangOfFour.sln" on your desktop. 
Double click and you're ready to go.

Documentation for these patterns is available in the 
"Gang Of Four.pdf" document.

==================================================================
2) Head First:
------------------------------------------------------------------

Contains 46 Head First design pattern projects. 

Create a shortcut to "DoFactory.HeadFirst.sln" on your desktop. 
Double click and you're ready to go.

Documentation for these patterns is available in the 
"Head First.pdf" document.

==================================================================
3) Patterns In Action:
------------------------------------------------------------------

Contains a reference e-commerce application that demonstrates the 
use of design patterns in a Web and SOA environment. Contains 
12 projects including Class libraries, Web Site, Web Service, and 
a Windows Application. 

Create a shortcut to "Patterns In Action.sln" on your desktop. 
Double click and you're ready to go.

This application uses the ASP.NET 2.0 built-in web server. If you 
want to use IIS you need to setup two new IIS virtual directories, 
one for the web site, the other for the web service. Also adjust 
the Web and the WebSOAService project Start Options accordingly.

Documentation for this application is available in the 
"Patterns In Action.pdf" document. 

==================================================================
4) Visio UML Diagrams:
------------------------------------------------------------------

Contains 23 Visio diagrams of the GoF design patterns.


==================================================================
Database setup in web.config:
------------------------------------------------------------------

By default the Patterns in Action application runs against an 
MS Access database (action.mdb) located in \App_Data\.

If you wish to use Sql Server create a new database named 'Action' 
and run the action.sql script. In web.config change the 
DataProvider to 'System.Data.SqlClient'.  Also confirm that the   
related ConnectionString in web.config is correct. The application 
will now run against Sql Server.

==================================================================
Additional help: contact help@dofactory.com
==================================================================