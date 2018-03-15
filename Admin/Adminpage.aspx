<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adminpage.aspx.cs" 
    Inherits="Admin_Adminpage" MasterPageFile="~/MasterPage.master" %>

    <asp:Content ID="contentAdminpage" ContentPlaceHolderID="mainContent" runat="server">

      <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upPnl" runat="server">
                <ContentTemplate>
             <div class="container white-background">
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>

            <asp:GridView CssClass="table table-hover" ID="gvRecipes" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" DataKeyNames="RecipeID" OnPageIndexChanging="gvRecipes_PageIndexChanging" OnRowCancelingEdit="gvRecipes_RowCancelingEdit" OnRowEditing="gvRecipes_RowEditing" OnRowUpdating="gvRecipes_RowUpdating" OnSelectedIndexChanged="gvRecipes_SelectedIndexChanged" PageSize="5">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ButtonField CommandName="select" DataTextField="RecipeID" HeaderText="ID" Text="Button" Visible="False" />
                    <asp:ButtonField CommandName="select" DataTextField="Title" HeaderText="Recipe Title" Text="Button" />
                    <asp:BoundField ItemStyle-VerticalAlign="Middle" ControlStyle-CssClass="col-lg-4" DataField="Description" HeaderText="Description" ReadOnly="True" >
                    <ControlStyle CssClass="col-lg-4" />
                    <ItemStyle VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField ControlStyle-CssClass="col-lg-1" DataField="Servings" HeaderText="Servings" ReadOnly="True" >
                    <ControlStyle CssClass="col-lg-1" />
                    </asp:BoundField>
                    <asp:BoundField ControlStyle-CssClass="col-lg-1" DataField="TimePreparation" HeaderText="Time for preparation" ReadOnly="True" >
                    <ControlStyle CssClass="col-lg-1" />
                    </asp:BoundField>
                    <asp:ImageField ControlStyle-CssClass="img-responsive" DataImageUrlField="ImageURL" DataImageUrlFormatString="~/Images/recipesImages/{0}" HeaderText="Recipe Image" ControlStyle-Height="100" ControlStyle-Width="100" ReadOnly="True">
                        <ControlStyle Height="120px" Width="600px"></ControlStyle>
                    </asp:ImageField>
                    <asp:CheckBoxField ControlStyle-CssClass="col-lg-1" DataField="Status" HeaderText="Approved or not" >
                    <ControlStyle CssClass="col-lg-1" />
                    </asp:CheckBoxField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#8c8a88" Font-Bold="True" ForeColor="White" />

                <PagerStyle BackColor="#ffffff" CssClass="PagerNumbers" ForeColor="#333333" HorizontalAlign="Center" />

                <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />

                <SelectedRowStyle BackColor="#8c8a88" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>

            <br />

            <div class="row">
                <div class="col-lg-6">
                    

                    <asp:GridView CssClass="table table-hover" ID="gvIngredients" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="gvIngredients_PageIndexChanging" PageSize="30">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                         
                            <asp:BoundField DataField="Amount" HeaderText="Amount" NullDisplayText="Not set" />
                            <asp:BoundField DataField="Units" HeaderText="Unit" NullDisplayText="Not set" />
                            <asp:BoundField DataField="IngredientName" HeaderText="Ingregient name" NullDisplayText="Not set" />
                            <asp:BoundField DataField="Notes" HeaderText="Note" NullDisplayText="Not set" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#8c8a88" Font-Bold="True" ForeColor="White" />

                        <PagerStyle BackColor="#ffffff" CssClass="PagerNumbers" ForeColor="#333333" HorizontalAlign="Center" />

                        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />

                        <SelectedRowStyle BackColor="#8c8a88" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>


                </div>
                <div class="col-lg-6">

                    <asp:GridView AutoGenerateColumns="False" CssClass="table table-hover" ID="gvSteps" runat="server" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           
                            <asp:BoundField DataField="Description" HeaderText="Steps" NullDisplayText="Not set" />
                        </Columns>

                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#8c8a88" Font-Bold="True" ForeColor="White" />

                        <PagerStyle BackColor="#ffffff" CssClass="PagerNumbers" ForeColor="#333333" HorizontalAlign="Center" />

                        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />

                        <SelectedRowStyle BackColor="#8c8a88" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </div>
            </div>

              





            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
        </div>
                    </ContentTemplate>
            </asp:UpdatePanel>

    </asp:Content>

