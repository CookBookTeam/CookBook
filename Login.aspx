<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="contentLogin" ContentPlaceHolderID="mainContent" runat="server" class="container">
    
    <br />
    <br />
    <br />
    <br />

    <div class="container">

        <asp:Login ID="loginComp" runat="server" CreateUserUrl="~/Register.aspx" DestinationPageUrl="~/Homepage.aspx" class="container">
            <LayoutTemplate>

                <div class="page-header">
                    <h1>Log in <span style="font-style: oblique">your</span> personal CookBook</h1>
                </div>

                <div class="form-group">
                    <asp:Label ID="UserNameLabel" for="UserName" class="control-label col-xs-2" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="UserName" runat="server" class="form-control" placeholder="Username"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="loginComp">*</asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="form-group">
                    <asp:Label ID="PasswordLabel" runat="server" for="Password" class="control-label col-xs-2" AssociatedControlID="Password">Password:</asp:Label>

                    <div class="col-xs-10">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" class="form-control" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="loginComp">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-xs-offset-2 col-xs-10">
                        <asp:CheckBox ID="RememberMe" class="checkbox" runat="server" Text="Remember me next time." />
                    </div>
                </div>

                <div>
                    <div style="color: Red;">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-xs-offset-2 col-xs-10">
                        <asp:HyperLink ID="hlRegister" runat="server" NavigateUrl="~/Registration.aspx">Register</asp:HyperLink>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="LoginButton" runat="server" style="align-content:flex-end" class="btn up-header-edit-profile-button" CommandName="Login" Text="Log In" ValidationGroup="loginComp" />
                    </div>
                </div>


            </LayoutTemplate>
        </asp:Login>

    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
