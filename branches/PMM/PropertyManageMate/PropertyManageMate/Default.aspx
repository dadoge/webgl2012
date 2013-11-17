<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Themes/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PropertyManageMate._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Promo Here</h2>
            </hgroup>
            <p>
                    Lorem Ipsum Solor dit amet...
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Feature One</h5>
            Sweet Interface
        </li>
        <li class="two">
            <h5>Feature Two</h5>
            Large network of contractors
        </li>
        <li class="three">
            <h5>Feature Three</h5>
            Great Support
        </li>
    </ol>
</asp:Content>
