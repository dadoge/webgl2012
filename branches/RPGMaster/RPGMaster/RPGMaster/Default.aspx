<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RPGMaster._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
<div class="container account-wrapper">
    <div class="container container-back account-main-container">
        <div class="account-inner-container text-center">
        <h3><%: Title %> to RPG Master</h3>
            <p>Please <a runat="server" href="~/Account/LogIn">Log in</a> or <a runat="server" href="~/Account/Register">Register</a> a new account!</p>
        </div>
    </div>
</div>

</asp:Content>
