<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Homepage.aspx.cs"
    Inherits="Homepage" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"
     %>

<asp:Content ID="contentHomepage" ContentPlaceHolderID="mainContent" runat="server">


    <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#myCarousel" data-slide-to="1"></li>
            <li data-target="#myCarousel" data-slide-to="2"></li>
        </ol>
        <div class="carousel-caption carousel-caption-pading-bottom">
            <h1 class="carousel-h1 centered text-shadow">THE BEST WEB SITE FOR RECIPES</h1>
            <h2 class="centered text-shadow">
                Find all kinds of recipes, search by ingredients and store all of your favourite recipes!
            </h2>
            
        </div>

        <div class="carousel-inner" role="listbox">

            <div class="item active">
                <img class="first-slide" src="Images/categoryImages/carosel4.jpg" alt="First slide" />
                <div class="container">
                </div>
            </div>
            <div class="item">
                <img class="second-slide" src="Images/categoryImages/carosel4.jpg" alt="Second slide" />
                <div class="container">
                </div>
            </div>
            <div class="item">
                <img class="third-slide" src="Images/categoryImages/carosel4.jpg" alt="Third slide" />
                <div class="container">
                </div>
            </div>
        </div>

        <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
    <!-- /.carousel -->




    <div class="container white-background">


        <br>
        <br>
        <br>
        <br>



        <div class="heder-text-heder" style="">
            <span class="span-text-heder" style="">Services
            </span>
        </div>
        <br>
        <br>
        <br>
        <div class="row services-pading-bottom">
            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4" align="center">
                <div class="homepage-category-hero">
                    <a href="Repeater.aspx?type=review">
                        <img class="homepage-category-img" src="Images/categoryImages/1.png" />
                    </a>
                    <div class="homepage-category-icon">
                        <i class="fa fa-star fa-2x homepage-category-pika" style="color: #30c7ff; padding-right: 0px; position: relative; top: -2px; left: 1px;"></i>
                    </div>
                </div>
                <h4>Recipes by rating</h4>
                <h5>Find the top rating recipese, choosed by all cookBook users !
                Welcome to your favorites!
                Tap  to save any recipe you like,
                and it'll show up here.
                </h5>

            </div>
            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4" align="center">
                <div class="homepage-category-hero">
                    <a href="Repeater.aspx?type=recent">
                        <img class="homepage-category-img" src="Images/categoryImages/2.png" />
                    </a>
                    <div class="homepage-category-icon">
                        <i class="fa fa-fire fa-2x homepage-category-pika" style="color: #ff3d4e; padding-right: 0px; position: relative; top: -2px; left: 1px;"></i>
                    </div>
                </div>
                <h4>Recent Recipes</h4>
                <h5>Trending recipes from CookBook !</h5>

            </div>
            <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4" align="center">
                <div class="homepage-category-hero">
                    <a runat="server" href="Categories.aspx" >
                        <img class="homepage-category-img" src="Images/categoryImages/3.png" />
                    </a>
                    <div class="homepage-category-icon">
                        <i class="fa fa-book fa-2x homepage-category-pika" style="color: #c66cff; padding-right: 0px; position: relative; top: -2px; left: 1px;"></i>
                    </div>
                </div>
                <h4>Recipes by Category</h4>
                <h5>Find the top rating recipese, choosed by all cookBook users !</h5>

            </div>
        </div>
        <br>
        <br>
    </div>

    <div class="container">
    <div class="panel-group" id="accordion">
        <div class="panel panel-warning">
            <div class="panel-heading text-center" style="background-color: #feaa26; " >
                <h2 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" style="color:#fff;">Search recipes by ingrediant</a>
                </h2>
            </div>
            <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="upPnl" runat="server">
                <ContentTemplate>
                    <div id="collapse1" class="panel-collapse collapse in">
                <div class="panel-body row">
                    <div class="col-xs-6">
                        <div class="input-group">
                            <asp:TextBox runat="server" UseSubmitBehavior="false" ID="searchIngrPlus" type="text" class="form-control" placeholder="Include ingredients" />
                            <span class="input-group-btn">
                                <asp:Button ID="btnPlus"  UseSubmitBehavior="false" class="btn btn-default" Text="+" runat="server" OnClick="btnPlus_Click"></asp:Button>
                            </span>
                        </div>
                        <br />
                        <asp:PlaceHolder ID="ingrPH" runat="server">
                            
                        </asp:PlaceHolder>

                        <!--<asp:ListBox style="OVERFLOW:auto;" ID="plusIngr" runat="server" Enabled="False" ></asp:ListBox>-->
                  
                             </div>
                    <div class="col-xs-6">
                        <div class="input-group">
                            <asp:TextBox runat="server" UseSubmitBehavior="false" ID="searchIngrMinus" type="text" class="form-control" placeholder="Exclude ingredients" />
                            <span class="input-group-btn">
                                <asp:Button ID="btnMinus" UseSubmitBehavior="false" class="btn btn-default" Text="-" runat="server" OnClick="btnMinus_Click"></asp:Button>
                            </span>
                        </div>
                        <br />
                         <asp:PlaceHolder ID="ingrPHMinus" runat="server"></asp:PlaceHolder>

                       <!-- <asp:ListBox ID="minusIngr" style="OVERFLOW:auto;" runat="server" Enabled="False"></asp:ListBox>-->
                    </div>
                </div>
                <div class="panel-footer text-center" style="background-color:#fff;">
                    <asp:Button ID="btnSearchIngr" runat="server"  CssClass="btn up-header-edit-profile-button" Text="Search" UseSubmitBehavior="false" OnClick="btnSearchIngr_Click"/>
                    
                    <asp:GridView ID="gv" runat="server">
                    </asp:GridView>
                   
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            

        </div>
    </div>
        </div>



    <section class="marquee" >
    <div class="container">
         <div class="row">
            <div class="col-lg-12 text-center">
                <h2 class="section-heading" style="color: #FFFFFF;"><b>Contact Us</b></h2>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                
                    <div class="row" align="center" style="padding: 0px 30% 0px 30%;">

                        <div class="form-group">
                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Your Name *" id="name" required data-validation-required-message="Please enter your name.">
                                </asp:TextBox>
                            <p class="help-block text-danger"></p>
                        </div>
                       
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control"  placeholder="Your Message *" required data-validation-required-message="Please enter a message."  id="TextArea1" TextMode="multiline" Columns="50" Rows="5" runat="server"></asp:TextBox>

                            <p class="help-block text-danger"></p>
                        </div>
                        <div id="success"></div>

                        
                   <asp:Button CssClass="btn up-header-edit-profile-button"  Text="Send Message" ID="Button1" runat="server"  OnClick="Button1_Click" />

                     
                        
                        
                          </div>
               
            </div>
        </div>
    </div>
</section>

</asp:Content>
