<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Categories.aspx.cs"
     Inherits="Categories" MasterPageFile="~/MasterPage.master"%>
<asp:Content ID="contentHomepage" ContentPlaceHolderID="mainContent" runat="server">

 <div class=" container_small white-background">

        <br>
        <br>
        <br>
        <br>
        <br>
        <div class="heder-text-heder" style="">
            <span class="span-text-heder" style="">Recipe categories
            </span>
        </div>
        <br>
        <br>

            <div>
                <asp:ListView ID="lvCategorii" runat="server" DataSourceID="SqlDataSource1" GroupItemCount="6" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" DataKeyNames="CategoryID" OnSelectedIndexChanging="lvCategorii_SelectedIndexChanging">
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>

                    <EmptyItemTemplate>
                        <td runat="server" />
                    </EmptyItemTemplate>

                    <GroupTemplate>
                        <tr id="itemPlaceholderContainer" runat="server">
                            <td id="itemPlaceholder" runat="server"></td>
                        </tr>
                    </GroupTemplate>



                    <ItemTemplate>
                        <div runat="server" class="col-xs-6 col-sm-4 col-md-3 col-lg-2 center-block " align="center" style="padding: 0px 5px 20px 5px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select">
                       <img   class="img-responsive img-circle img-thumbnail" src="Images/categoryImages/<%# Eval("CategoryID") %>.png"/>
                            </asp:LinkButton>

                            <span class="category-item">
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />

                            </span>

                        </div>

                    </ItemTemplate>


                    <LayoutTemplate>

                        <div id="groupPlaceholderContainer" class="row" runat="server">
                            <div id="groupPlaceholder" runat="server">
                            </div>
                            <br />
                        </div>

                        <table runat="server" align="center">


                            <tr runat="server">
                                <td runat="server" style="text-align: center; background-color: #FFF; font-family: Verdana, Arial, Helvetica, sans-serif; color: #333333;">
                                    <asp:DataPager ID="DataPager1" runat="server" PageSize="12">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-default" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>


                    </LayoutTemplate>

                    <SelectedItemTemplate>
                    </SelectedItemTemplate>

                </asp:ListView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conString %>" SelectCommand="SELECT * FROM [Category]"></asp:SqlDataSource>
                <br />
                <br />
                <asp:Label ID="lblKategorija" runat="server"></asp:Label>
                <br />

            </div>
    </div>
    </asp:Content>