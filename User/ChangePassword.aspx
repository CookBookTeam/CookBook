<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs"
Inherits="User_ChangePassword" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="contentChange" ContentPlaceHolderID="mainContent" runat="server" class="container">
    <br />
    <br />
    <br />
    <br />

<div class="container">

    <asp:ChangePassword ID="ChangePassword1" runat="server" >
        <ChangePasswordTemplate>


            <div class="page-header">
                <h1>Change Your Password</h1>
            </div>

            <div class="form-group">

                <asp:Label for="CurrentPassword" ID="CurrentPasswordLabel" class="control-label col-xs-2" runat="server" AssociatedControlID="CurrentPassword">Password:</asp:Label>

                <div class="col-xs-10">
                    <asp:TextBox ID="CurrentPassword" runat="server" class="form-control"  TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
                
                    <asp:Label for="NewPassword" class="control-label col-xs-2" ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                
                <div class="col-xs-10">
                    <asp:TextBox class="form-control" ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
               
                    <asp:Label for="ConfirmNewPassword" class="control-label col-xs-2" ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
          
                <div class="col-xs-10">
                    <asp:TextBox class="form-control"  ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </div>
                
            </div>

         
            <div class="form-group">
            
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangePassword1"></asp:CompareValidator>
                
            </div>

            <div class="form-group">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                
            </div>

            <div class="form-group">
                    
                    <span class="col-xs-6"></span>
                    <asp:Button ID="ChangePasswordPushButton" class="btn up-header-edit-profile-button col-xs-2" runat="server" CommandName="ChangePassword" Text="OK" ValidationGroup="ChangePassword1" />
                    <span class="col-xs-1"></span>
                    <asp:Button ID="CancelPushButton" class="btn up-header-edit-profile-button col-xs-2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    <span class="col-xs-1"></span>
            </div>
            

        </ChangePasswordTemplate>
    </asp:ChangePassword>
    <br />
            <br />
            <br />
            <br />
</div>
</asp:Content>
