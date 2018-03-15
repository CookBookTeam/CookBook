<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage"
       MasterPageFile="~/MasterPage.master" EnableEventValidation="false"%>


 <asp:Content ID="contentChange" ContentPlaceHolderID="mainContent" runat="server" class="container">


    <div class="container-fluid user-profile-header-container" style="padding-top: 7%;">
        <div class="container-fluid up-header-social-and-edit-profile-section" runat="server"> 
            <asp:Panel ID="editPanel" runat="server" Visible="true">
            <a class="btn btnstateless hidden-xs up-header-edit-profile-button pull-right" id="btnEdit" data-toggle="modal" data-target="#myModal" runat="server">Edit Profile</a>
        </asp:Panel>
                </div>
        <div class="container">
 
  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog"> 
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Update your profile</h4>
        </div>
          <div class="modal-body">

              <!-- TITLES -->
              <ul class="nav nav-tabs">
                  <li role="presentation" class="active">
                      <a data-toggle="tab" href="#aboutme">About me</a>
                  </li>
                  <li role="presentation">
                      <a data-toggle="tab" href="#pic">Profile picture</a>
                  </li>
                  <li role="presentation">
                      <a data-toggle="tab" href="#security">Security</a>
                  </li>
              </ul>
              <!-- TITLES -->

              <!-- CONTENT -->
              <div class="tab-content">
                  
                  
                  <!-- ABOUT ME -->
                  <div id="aboutme" class="tab-pane fade in active">
                     <br />
                      <br />
                      <asp:Label ID="Label1" runat="server" Text="Culinary Expertise"></asp:Label>
                      <asp:RadioButtonList ID="culExpertise" runat="server" RepeatDirection="Horizontal">
                          <asp:ListItem Text="Low" Value="1"></asp:ListItem>
                          <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                          <asp:ListItem Text="High" Value="3"></asp:ListItem>
                      </asp:RadioButtonList>
                      <br />
                      <asp:Label ID="Label2" runat="server" Text="About me panel"></asp:Label>
                      <br />
                      <asp:TextBox ID="aboutMe"  CssClass="form-control" TextMode="multiline" Columns="50" Rows="5" runat="server"></asp:TextBox>
                      <br />
                      <div class="row">
                          <div class="col-lg-1">
                              <i class="fa fa-facebook-official fa-2x" style="color:#0066ff"></i>
                          </div>
                           <div class="col-lg-11">
                               <div class="input-group">
                              <span class="input-group-addon" id="basic-addon3">https://www.facebook.com/</span>
                              <asp:TextBox ID="fb" runat="server" CssClass="form-control" aria-describedby="basic-addon3"/>
                          </div>
                          </div>
                      </div>
                      <br />
                        <div class="row">
                          <div class="col-lg-1">
                              <i class="fa fa-twitter fa-2x" style="color:#0066ff"></i>
                          </div>
                           <div class="col-lg-11">
                              <div class="input-group">
                              <span class="input-group-addon" id="basic-addon4">https://www.twitter.com/</span>
                              <asp:TextBox ID="twitter" runat="server" CssClass="form-control" aria-describedby="basic-addon4"/>
                          </div>
                          </div>
                      </div>
                      <br />
                          <div class="row">
                          <div class="col-lg-1">
                              <i class="fa fa-linkedin fa-2x" style="color:#0066ff"></i>
                          </div>
                           <div class="col-lg-11">
                                  <div class="input-group ">
                              <span class="input-group-addon" id="basic-addon5">https://www.linkedin.com/</span>
                              <asp:TextBox ID="linkedin" runat="server" CssClass="form-control" aria-describedby="basic-addon5"/>
                          </div>
                          </div>
                      </div>
                    <br />
                    
                     <div class="row">
                          <div class="col-lg-1">
                                <i class="fa fa-youtube fa-2x" style="color:#0066ff"></i>
                          </div>
                           <div class="col-lg-11">
                                     <div class="input-group ">
                              <span class="input-group-addon" id="basic-addon6">https://www.youtube.com/</span>
                              <asp:TextBox ID="youtube" runat="server" CssClass="form-control" aria-describedby="basic-addon6"/>
                          </div>
                          </div>
                      </div>
 <br />
                      <div class="row">
                          <div class="col-lg-1">
                                 <i class="fa fa-skype fa-2x" style="color:#0066ff"></i>
                          </div>
                           <div class="col-lg-11">
                                       <asp:TextBox ID="skype" runat="server" CssClass="form-control"></asp:TextBox> <br />
                   
                          </div>
                      </div>
                    

                     
                    
                  </div>


                  <!-- PROFILE PICTURE -->
                  <div id="pic" class="tab-pane fade">
                      <br />
                      <br />
                      <div class="row">
                          <div class="col-lg-12" style="padding-left:30%; padding-right:30%;">
                              <asp:Image CssClass="img-responsive"  ID="showPic" runat="server" />
                          <%--<asp:Image ID="loader" runat="server" ImageUrl="~/Images/loader.gif"/>--%>
                          </div>
                          
                      </div>
                     <br />
                      <!-- kopcheto za upload-->
                     <label runat="server" class="label-update-file icon--picture-update button button-primary button-box button-longshadow-right">
                          <i runat="server" class="fa fa-refresh "></i>   
                         <asp:FileUpload  CssClass="photoUpload" ID="fileUploadPic"  accept="image/*" runat="server"  />
                     </label>
                  </div>

                  <!-- CHANGE PASSWORD -->
                  <div id="security" class="tab-pane fade">
                       <br />
                      <br />

                        <asp:ChangePassword ID="ChangePassword1" runat="server" Width="100%" >
        <ChangePasswordTemplate>


            <div class="page-header">
                <h1>Change Your Password</h1>
            </div>

            <div class="row">
                <div class="col-lg-2">
      <asp:Label for="CurrentPassword" ID="CurrentPasswordLabel" class="control-label " runat="server" AssociatedControlID="CurrentPassword">Password:</asp:Label>
            
                </div>
               <div class="col-lg-10">
                 <asp:TextBox ID="CurrentPassword" runat="server" class="form-control"  TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                </div>

            </div>



                  <div class="row">
                <div class="col-lg-2">
       <asp:Label for="NewPassword" class="control-label " ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                  
                </div>
               <div class="col-lg-10">
                      <asp:TextBox class="form-control" ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
              </div>

            </div>
            
               <div class="row">
                <div class="col-lg-2">
          <asp:Label for="ConfirmNewPassword" class="control-label " ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                   
                </div>
               <div class="col-lg-10">
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
                    <asp:Button ID="ChangePasswordPushButton" class="btn up-header-edit-profile-button col-xs-2" runat="server" CommandName="ChangePassword" Text="Apply" ValidationGroup="ChangePassword1" />
                    <span class="col-xs-1"></span>
                    
            </div>
            

        </ChangePasswordTemplate>
    </asp:ChangePassword>


                  </div>
              </div>
              <!-- CONTENT -->

          </div>
        <div class="modal-footer">
          <asp:Button CssClass="btn btn-default" ID="btnClose" OnClick="btnClose_Click" data-dismiss="modal" Text="Close" runat="server"/>
          <asp:Button ID="btnUpdate" CssClass="btn btn-default" OnClick="updateProfile" Text="Update" runat="server"/>
        </div>
      </div>
    </div>
  </div>
</div>
        <div class="container-fluid up-header-avatar-section image-circle-cropper">
            <asp:Image class="i-up-header-avatar center-block"  ID="profilePicture" runat="server" OnDataBinding="profilePicture_DataBinding" OnLoad="profilePicture_Load"/>
        </div>
        <div class="container-fluid up-header-cook-name-section">
            <h1 id="name" runat="server"></h1>
        </div>
        <div class="container-fluid up-header-cook-bio-section">
        </div>
    </div>


















    <!-- ************************************** BODY OF PROFILE ************************************** -->







     <div class="container">
         
            <ul class="nav nav-tabs">
             
              <li role="presentation">
                      <a data-toggle="tab" href="#myRecipes" class="active"><span class="fa fa-file-text"></span> My Recipes</a>
                  </li>
                
            <li role="presentation">
                    <a data-toggle="tab" href="#favRecipes"><span class="fa fa-heart"></span> Favourite recipes</a>
                </li>
                 <li role="presentation" >
                      <a data-toggle="tab" href="#addRecipe"> <span class="fa fa-plus"></span> Add recipe</a>
                  </li>
            </ul>


         
            <div class="tab-content">

                <!-- NEW RECIPE -->
                  <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
                    
                 <div id="addRecipe"  class="tab-pane fade"> 
                   
                    
                           <div class="container white-background" style="margin-top: 2px;">

        <div class="row div-add-recipe">
     
                <div class="col-lg-4">


                    <br>
                    <div class="form-group">
                        <label for="preptime">Preparation time:</label>
                        <asp:TextBox ID="preptime" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="numberservings">Number of servings:</label>
                        <asp:TextBox ID="numberservings" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="numberservings">Category:</label>
                        <asp:DropDownList CssClass="form-control" ID="categoryChooser" runat="server"></asp:DropDownList>
                    </div>

                       <div class=" pull-left">
           
                            <asp:FileUpload runat="server" id="recipePicFile" /> <br />
                            <asp:Image CssClass="center-block"  ID="recipePicShow" runat="server" />
                       
                    </div>

                </div>
                <div class="col-lg-8">
                    <div class="form-group">
                        <label for="recipetitle">Recipe title:</label>
                        <asp:TextBox ID="recipetitle" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                     <div class="form-group">
                        <label for="recipetitle">Description:</label>
                        <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>


                    <div class="form-group">
                        <label for="directions">Steps for preparation:</label>
                        <asp:TextBox TextMode="MultiLine" ID="directions" CssClass="form-control" placeholder="Put each step in a new line." runat="server"></asp:TextBox>
                    </div>

                    <asp:UpdatePanel ID="upPnlNewRecipe" runat="server">
                        <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label for="amount">Amount:</label>
                               
                                    <asp:TextBox runat="server" CssClass="form-control" ID="amount" placeholder="1 1/2"></asp:TextBox>
                                <br />
                                 <asp:Panel ID="pnlAmount" runat="server"></asp:Panel>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="measure">Measure:</label>
                                
                                <asp:TextBox  CssClass="form-control" ID="measure" placeholder="cups" runat="server"></asp:TextBox>
                          <br /><asp:Panel ID="pnlMeasure" runat="server"></asp:Panel>
                                      </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="ingredient_name">Ingredient:</label>
                               
                                <asp:TextBox  runat="server" CssClass="form-control" ID="ingredient_name" placeholder="Flour"></asp:TextBox>
                                <br /> <asp:Panel ID="pnlIngrName" runat="server"></asp:Panel>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="notes">Notes:</label>
                                
                                <asp:TextBox CssClass="form-control" ID="notes" placeholder="sifted" runat="server"></asp:TextBox>
                                <br /><asp:Panel ID="pnlNotes" runat="server"></asp:Panel>
                            </div>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                          <asp:Button ID="btnAddIngr" class="btn btn-default" runat="server" Text="Add ingredient line"  OnClick="btnAddIngr_OnClick"/>

                 </ContentTemplate>
                        </asp:UpdatePanel>
                    
                </div>
        
        </div>
        <div class="row">
            <div runat="server" visible="false" id="alertNoEnteredIngr" class="alert alert-danger" style="width:200px">
                <span class="close" data-dismiss="alert">×</span>
                <span><strong>Atention!</strong> You need to specify all the fields for the new ignredient.</span>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <asp:Button class="btn btnstateless hidden-xs hidden-sm up-header-edit-profile-button pull-right" runat="server" ID="btnSaveRecipe" OnClick="btnSaveRecipe_OnClick" Text="Save Recipe"/>
                <%--<asp:Button class="btn btnstateless hidden-md hidden-lg up-header-edit-profile-button center-block">Save Recipe</asp:Button>--%>
            </div>
        </div>


        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
        <br>
    </div>
                 
                    
                 </div>

                <!-- MY RECIPES -->
                 
                    
                <div id="myRecipes" class="tab-pane fade in active">
 
                       <section id="SectionID">
                <asp:Repeater ID="Repeater1" runat="server" >
                    <ItemTemplate>
                        <article runat="server" class="white-panel">


                            <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="LinkButton1_Command" CommandName="SelectRecipe" CommandArgument='<%# Eval("RecipeID") %>'>
                        <img  src="Images/recipesImages/<%# Eval("ImageURL") %>"/>

                            </asp:LinkButton>

                            <h4>
                                <a href="#" style="color: #4d4d4d; font-size: 17px;">
                                    <%# Eval("Title") %>
                                </a>

                            </h4>
                            

                            <hr style="margin-bottom: 3px; margin-top: 0px;" />
                            <div class="row">
                                
                                <div class="col-lg-10">
                                    <div class="pull-right">
                                        <asp:ImageButton ID="ibMark" runat="server" CausesValidation="False" CssClass="auto-style1" Height="20px" Width="20px" ImageUrl="~/Images/full-star.png" />
                                        <asp:Label ID="lblMark" runat="server"><%# Eval("Mark") %></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <br />


                
            </section>
      <!--paging-->
      <div class="text-center">
          <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPage"
                            Style="padding: 8px; margin: 2px; background: orange; font: 12px;"
                            CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                            runat="server" ForeColor="White" Font-Bold="True" CausesValidation="false"><%# Container.DataItem %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
      </div>
                 </div>
                


                <!--FAVOURITE RECIPES -->
                
                        <div id="favRecipes" class="tab-pane fade">
                      <section id="SectionID2" style="  position: relative; max-width: 100%; width: 100%;">
                        <asp:Repeater ID="Repeater2" runat="server" >
                         <ItemTemplate>
                        <article runat="server" class="white-panel">


                            <asp:LinkButton ID="LinkButton2" runat="server" OnCommand="LinkButton2_Command" CommandName="SelectRecipe" CommandArgument='<%# Eval("RecipeID") %>'>
                        <img  src="Images/recipesImages/<%# Eval("ImageURL") %>"/>

                            </asp:LinkButton>

                            <h4>
                                <a href="#" style="color: #4d4d4d; font-size: 17px;">
                                    <%# Eval("Title") %>
                                </a>

                            </h4>
                            

                            <hr style="margin-bottom: 3px; margin-top: 0px;" />
                            <div class="row">
                                
                                <div class="col-lg-10">
                                    <div class="pull-right">
                                        <asp:ImageButton ID="ibMark2" runat="server" CausesValidation="False" CssClass="auto-style1" Height="20px" Width="20px" ImageUrl="~/Images/full-star.png" />
                                        <asp:Label ID="lblMark2" runat="server"><%# Eval("Mark") %></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <br />


                
            </section>
      <!--paging-->
      <div class="text-center">
          <asp:Repeater ID="rptPaging2" runat="server" OnItemCommand="rptPaging2_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPage2"
                            Style="padding: 8px; margin: 2px; background: orange; font: 12px;"
                            CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                            runat="server" ForeColor="White" Font-Bold="True" CausesValidation="false"><%# Container.DataItem %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
      </div>
                 </div>
              

            

                </div>
 

   
     </div>





    <script type="text/javascript" src="<%=ResolveUrl("~/js/jquery-2.2.0.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/js/profileScript.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/js/newpinterest.js")%>"></script>
</asp:Content>