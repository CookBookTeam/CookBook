<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Repeater.aspx.cs" 
    Inherits="Repeater" MasterPageFile="~/MasterPage.master"%>
<asp:Content ID="contentHomepage" ContentPlaceHolderID="mainContent" runat="server">
  <div class="container white-background">
            <br>
            <br>
            <br>
            <br>
            <br>
            <div class="heder-text-heder" style="">
                <span class="span-text-heder" style="">
                    <%# title %>
                </span>
            </div>
            <h4 style="font-size: 15px; color: gray; margin: 20px 20% 10px 20%; text-align: center;">
                <%# description %>
            </h4>
            <br>
            <br>

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
                            <p style="padding-top: .5rem; color: #7f7f7f; clear: both; font-size: 14px;">
                                <%# Eval("Description") %>
                            </p>

                            <hr style="margin-bottom: 3px; margin-top: 0px;" />
                              <div class="row">
                               
                                <div class="col-lg-6">
                                    <div class="pull-right">
                                        <asp:ImageButton ID="ibMark" runat="server" CausesValidation="False" CssClass="auto-style1" Height="20px" Width="20px" ImageUrl="~/Images/full-star.png" />
                                        <asp:Label ID="lblMark" runat="server"><%# Eval("Mark") %></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="pull-right">
                                        <span class="fa fa-comment-o"></span>
                                        <asp:Label ID="Label1" runat="server"><%# Eval("NComments") %></asp:Label>
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
        <br />
        <br />


    </asp:Content>