<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recipe.aspx.cs"
    Inherits="Recipe" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="contentHomepage" ContentPlaceHolderID="mainContent" runat="server">
                         <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <div>
        <div class="container" style="background-color: #fff;">
            <br>
            <br>
            <br>
            <br>

            <div class="row">
                <div class="col-lg-4">
                    <h3><%# Recipetitle %></h3>
                    <h4><small>Recipe by:</small><%# AuthorName %> <%# AuthorLastName %></h4>
                    <h5>
                        <%# RecipeDescription %>
                    </h5>

                    <div class="sidebar-headings">
                        <span style="font-size: 23px;" class="glyphicon glyphicon-hourglass"></span>
                        <span class="sidebar-heading-text" style="margin-bottom: 10px;">Total time:
                        </span>
                        <span style="font-size: 23px;">
                            <%# TotalTime %>
                        </span>
                    </div>
                    <div class="sidebar-headings">
                        <span style="font-size: 23px;" class="fa fa-cutlery"></span>
                        <span class="sidebar-heading-text" style="margin-bottom: 10px;">Servings:
                        </span>
                        <span style="font-size: 23px;">
                            <%# Servings %>
                        </span>
                    </div>

                    <div class="sidebar-headings">
                        <span style="font-size: 23px;" class="glyphicon glyphicon-grain"></span>
                        <span class="sidebar-heading-text" style="margin-bottom: 10px;">Ingredients:
                        </span>
                    </div>
                    <ul class="list-unstyled" style="margin-left: 30px;">

                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSourceForIngredients">
                            <ItemTemplate>
                                <li><span style="color: #f77204;" class="fa fa-plus-circle"></span>
                                    <%# Eval("Amount").ToString().Trim() %> &nbsp; 
                             <%# Eval("Units").ToString().Trim() %>&nbsp; 
                             <%# Eval("IngredientName").ToString().Trim() %>&nbsp; 
                             <%# Eval("Notes").ToString().Trim() %>
                           

                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:SqlDataSource ID="SqlDataSourceForIngredients" runat="server" ConnectionString="<%$ ConnectionStrings:conString %>"></asp:SqlDataSource>

                    </ul>



                </div>




                <div class="col-lg-8 ">

                    <img class="pull-right img-thumbnail img-responsive" src="Images/RecipesImages/<%# ImageURL %>" width="350" />


                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-10">

                            <asp:ImageButton ID="ibMark1" runat="server" CausesValidation="False" CssClass="auto-style1" Height="30px" OnClick="ibMark1_Click" Width="32px" ImageUrl="~/Images/empty-star.png" />
                            &nbsp;<asp:ImageButton ID="ibMark2" runat="server" CausesValidation="False" Height="30px" OnClick="ibMark2_Click" Width="30px" ImageUrl="~/Images/empty-star.png" />
                            &nbsp;<asp:ImageButton ID="ibMark3" runat="server" CausesValidation="False" Height="30px" OnClick="ibMark3_Click" Width="30px" ImageUrl="~/Images/empty-star.png" />
                            &nbsp;<asp:ImageButton ID="ibMark4" runat="server" CausesValidation="False" Height="30px" OnClick="ibMark4_Click" Width="30px" ImageUrl="~/Images/empty-star.png" />
                            &nbsp;<asp:ImageButton ID="ibMark5" runat="server" CausesValidation="False" Height="30px" OnClick="ibMark5_Click" Width="30px" ImageUrl="~/Images/empty-star.png" />

                        </div>
                        <br />
                        <br />
                        <div class="col-lg-2">
                          <!--  <span style="color: #ff3d4e;" class="fa fa-heart-o fa-3x pull-right"></span>-->
                              <asp:ImageButton ID="ib_Like" runat="server" ImageUrl="~/Images/heart.png" Height="31px" Width="34px" OnClick="ib_Like_Click" CausesValidation="False"/>
                              <asp:Label ID="lblNumberLikes" runat="server"></asp:Label>
                        </div>



                    </div>
</ContentTemplate>
                            </asp:UpdatePanel>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="sidebar-headings">
                            <span style="font-size: 23px;" class="fa fa-list-alt"></span>
                            <span class="sidebar-heading-text" style="margin-bottom: 10px;">Directions:
                            </span>
                        </div>
                        <ul class="list-unstyled" style="margin-left: 30px;">
                            <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSourceGetSteps">
                                <ItemTemplate>
                                    <li style="padding: 10px 0 10px 0;">
                                        <div class="row">
                                            <span style="color: #f77204; font-size: 20px; width: 25px; padding-top: 5px;" class="fa fa-pencil col-lg-1"></span>
                                            <span class="col-lg-11">

                                                <%# Eval("Description").ToString().Trim() %>

                                            </span>
                                        </div>
                                    </li>
                                </ItemTemplate>



                            </asp:Repeater>
                            <asp:SqlDataSource ID="SqlDataSourceGetSteps" runat="server" ConnectionString="<%$ ConnectionStrings:conString %>"></asp:SqlDataSource>
                        </ul>
                    </div>
                </div>

                <!--  COMMENTS -->

                <div class="row">
                    <div class="col-lg-12">
                        <div class="sidebar-headings">
                            <span style="font-size: 23px;" class="fa fa-comments"></span>
                            <span class="sidebar-heading-text" style="margin-bottom: 10px;">Cooments:
                            </span>
                        </div>


                        <asp:UpdatePanel ID="upPnl" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    
                                    <div class="col-lg-9">
                                        <asp:TextBox class="form-control" placeholder="Enter your comment here!" ID="txtComment" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtComment" ErrorMessage="Please Provide Comment" ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Button class="btn btn-block btn-warning" ID="btn_Submit" runat="server" Text="Post Comment" OnClick="btn_Submit_Click" />
                                    </div>
                                </div>
                                <asp:Repeater ID="repComments" runat="server">

                                    <ItemTemplate>
                                        <div class="commentbox">


                                            <div class="row">
                                                <div class="col-lg-1">
<img class="img-thumbnail img-responsive"  src="User/ProfilePictures/<%# Eval("ProfilePicture") %>"/>

                                                </div>
                                                <div class="col-lg-11">
                                                   
                                                <h3 style="margin-bottom:0px;margin-top: 0px;">
                                                      <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name")%> ' ></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("LastName")%> ' ></asp:Label>
                                                    <br />
                                               <small >
                                                    <asp:Label Font-Size="Small" ID="Label2" runat="server" Text='<%#Eval("Date") %>'>'></asp:Label>
                                               </small>
                                                     </h3>
                                                <div class="well" style=" background-color: #ffffff; border: 1px solid #ff8229;">
                                                         
                                                 <asp:Label class="small" Font-Size="Medium" ID="Label3" runat="server" Text='<%#Eval("Comment") %>'></asp:Label>
                                            
                                                </div>
                                                
                                            
                                                </div>
                                            </div>


                                            
                                        
                                           
                                            
                                            
                                           

                                        </div>
                                    </ItemTemplate>

                                </asp:Repeater>
                                <div style="overflow: hidden;">
                                    <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPage"
                                                Style="padding: 8px; margin: 2px; background: orange; font: 8px;"
                                                CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                                runat="server" ForeColor="White" Font-Bold="True" CausesValidation="false"><%# Container.DataItem %>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


                <!--  COMMENTS -->

                <hr />
                <asp:Label ID="lblShare" runat="server" Text=""></asp:Label>
                <br>
                <br>
                <br>
                <br>
            </div>

        </div>
    </div>
</asp:Content>
