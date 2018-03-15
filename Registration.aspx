<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs"
     Inherits="Register" MasterPageFile="~/MasterPage.master" %>

 <asp:Content ID="contentRegister" ContentPlaceHolderID="mainContent" runat="server" class="container">

    <br />
    <br />
    <br />
    <br />

    <div class="container" style="padding-bottom:300px">
    
        <asp:CreateUserWizard ID="RegisterUser" class="container" runat="server" ContinueDestinationPageUrl="~/Homepage.aspx" OnCreatingUser="RegisterUser_CreatingUser" OnCreatedUser="RegisterUser_CreatedUser" OnDataBinding="RegisterUser_DataBinding" OnActiveStepChanged="RegisterUser_ActiveStepChanged">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="step1" runat="server">
                    <ContentTemplate>

                             <div class="page-header">
                                 <h1>Sign Up and take your favourite recipes, anywhere!</h1>
                            </div>

                        <div class="row">
                            <div class="col-lg-8">
 <div class="form-group">
                                <asp:Label ID="Label11" class="control-label col-xs-3" for="Name" runat="server" AssociatedControlID="Name">First Name:</asp:Label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="Name"  class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name" ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                        <div class="form-group">
                            
                                <asp:Label ID="Label22" class="control-label col-xs-3" for="LastName" runat="server" AssociatedControlID="LastName">Last name:</asp:Label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="LastName"  class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastName" ErrorMessage="Last Name is required." ToolTip="Last Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                          
                            <div class="form-group">
                                <asp:Label ID="UserNameLabel" class="control-label col-xs-3" for="UserName" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="UserName"  class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label class="col-xs-3" ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="Password" runat="server"  class="form-control" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label class="col-xs-3" ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="ConfirmPassword"  class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label class="col-xs-3" ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="Email"  class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            </div>
                            <div class="col-lg-4">
                                  <asp:Image  ID="showProfile" runat="server"/>
                                    <asp:Image ID="loader" runat="server" ImageUrl="~/Images/loader.gif" />

                            </div>

                        </div>



                       
                            <div>
                                <asp:Label class="col-xs-3" ID="lblProfilePicture" runat="server" AssociatedControlID="profilePicture">Profile picture:</asp:Label>
                                <div class="col-xs-9 ">

                                        <asp:FileUpload  CssClass="" ID="profilePicture"  accept="image/*" runat="server"  />
                                     
                                  
                             </div>
                            </div>
                            <div>
                                <div class="col-xs-12">
                                    
                                </div>
                            </div>
                            <div>
                                <div>
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                </div>
                            </div>
                            <div>
                                <div style="color:Red;">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </div>
                            </div>
                        
                   <!-- <asp:Button ID="StepNextButton" runat="server" 
                     CommandName="MoveNext" Text="Create User"
                     ValidationGroup="CreateUserWizard1" />-->
                    </ContentTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server">

                    </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
        
</div>
<script type="text/javascript" src="<%=ResolveUrl("~/js/jquery-2.2.0.min.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/js/registerScript.js")%>"></script>

     </asp:Content>
