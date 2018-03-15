using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Web.Security;





public partial class Recipe : System.Web.UI.Page
{
    public String Recipetitle;
    public String TotalTime;
    public String Servings;
    public String ImageURL;
    public String RecipeDescription;
    public String AuthorID;
    public String AuthorName;
    public String AuthorLastName;
    string cs = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
    int RecipeID;
    int mark;
    public int numberOfLikes;
    //public int UserID = 2322;
    public object UserID = null;
    

    protected void Page_Load(object sender, EventArgs e)
    {
  
        if (Request.QueryString["r"] != null)
        {
            try
            {
                RecipeID = Convert.ToInt32(Request.QueryString["r"]);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Homepage.aspx");
            }
            
            getInfoRecipe(Request.QueryString["r"].ToString());
            getInfoIngredients(Request.QueryString["r"].ToString());
            getInfoSteps(Request.QueryString["r"].ToString());
            getRecipeAuthor();
            this.DataBind();
            fillComments();
        }
     
        // ako e logiran:
        if (Membership.GetUser(User.Identity.Name) != null)
        {
            UserID = Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey;
        }

        checkLikes(); // proveri dali tuka

        if (!Page.IsPostBack)
        {
            fillComments();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Not a postback')", true);

        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('a postback')", true);
        }

        calculateReviewForAGivenRecipe();

        // facebook
        lblShare.Text = "<a name=\"fb_share\" type=\"button\"></a>" +
                   "<script " +
                   "src=\"http://static.ak.fbcdn.net/connect.php/js/FB.Share\" " +
                   "type=\"text/javascript\"></script>";
        // facebook

        numOfComments();
    }

    // ako se javi exception vo toj slucaj vrati error strana
    // ne postoi takov recept ili da te ostavi na istata strana
    protected void getInfoRecipe(String RecipeID)
    {
        SqlConnection konekcija = new SqlConnection();
     
        konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand komanda = new SqlCommand();
        komanda.Connection = konekcija;
        komanda.CommandText = "SELECT * FROM [Recipes] WHERE [Recipes].RecipeID='" + RecipeID + "'";
        try
        {
            konekcija.Open();
            SqlDataReader reader = komanda.ExecuteReader();
            
            while (reader.Read())
            {
                Recipetitle = reader["Title"].ToString();
                TotalTime = reader["TimePreparation"].ToString();
                Servings = reader["Servings"].ToString();
                ImageURL = reader["ImageURL"].ToString();
                RecipeDescription = reader["Description"].ToString();
                AuthorID= reader["UserID"].ToString();
                //lblRecipeID.Text = Recipetitle;

            }
            reader.Close();
        }
        catch (Exception err)
        {
            // lblRecipeID.Text = err.ToString();
        }
        finally
        {
            konekcija.Close();
        }

    }


    protected void getInfoIngredients(String RecipeID)
    {
        SqlDataSourceForIngredients.SelectCommand = "SELECT * FROM [RecipeIngredients] WHERE  [RecipeIngredients].RecipeID='" + RecipeID + "'";
    }

    protected void getInfoSteps(String RecipeID)
    {
        SqlDataSourceGetSteps.SelectCommand = "SELECT * FROM [StepsPreparation] WHERE  [StepsPreparation].RecipeID='" + RecipeID + "'";
    }



    //kodot za komentarite
    private void fillComments()
    {
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "Select aspnet_Users.UserName, aspnet_Users.Name, aspnet_Users.LastName, aspnet_Users.ProfilePicture, Comment.Date, Comment.Comment from Comment INNER JOIN aspnet_Users ON Comment.UserID=aspnet_Users.UserId WHERE RecipeID=@RecipeID ORDER BY CommentID DESC";
        command.Parameters.AddWithValue("@RecipeID", RecipeID);
        DataTable dt = new DataTable();
        SqlDataAdapter adapt = new SqlDataAdapter();
        adapt.SelectCommand = command;
        adapt.Fill(dt);
        con.Close();
        PagedDataSource pds = new PagedDataSource();
        DataView dv = new DataView(dt);
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.PageSize = 4;
        pds.CurrentPageIndex = PageNumber;
        if (pds.PageCount > 1)
        {
            rptPaging.Visible = true;
            ArrayList arraylist = new ArrayList();
            for (int i = 0; i < pds.PageCount; i++)
                arraylist.Add((i + 1).ToString());
            rptPaging.DataSource = arraylist;
            rptPaging.DataBind();
        }
        else
        {
            rptPaging.Visible = false;
        }
        repComments.DataSource = pds;
        repComments.DataBind();
    }
    //btn_Submit Click Event
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("Insert into Comment(RecipeID,UserID,Comment,Date) values(@RecipeID,@UserID,@Comment,@Date)", con);
        cmd.Parameters.AddWithValue("@RecipeID", RecipeID);
        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@Comment", txtComment.Text);
        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
        cmd.ExecuteNonQuery();
        con.Close();
        //Displaying Javascript alert Comment Posted Successfully
        string alert = "alert('Comment Posted Successfully.'); window.location = 'Recipe.aspx?r=" + RecipeID + "';";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", alert, true);
        fillComments();
    
        txtComment.Text = "";
        numOfComments();
    }
    public int PageNumber
    {
        get
        {
            if (ViewState["PageNumber"] != null)
                return Convert.ToInt32(ViewState["PageNumber"]);
            else
                return 0;
        }
        set
        {
            ViewState["PageNumber"] = value;
        }
    }
    protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
        fillComments();
    }

    protected void review()
    {
        bool flag = false;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT * FROM Reviews";
        try
        {
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((reader["UserID"].Equals(UserID)) && (reader["RecipeID"].ToString().Equals(RecipeID.ToString())))
                {
                    flag = true; //ima dadeno ocenka za taj recept
                    break;
                }
            }
            reader.Close();
            //ako userot ima ocenka za toj recept
            if (flag == true)
            {
                string alert = "alert('You have already gave a mark for this recipe.'); window.location = 'Recipe.aspx?r=" + RecipeID + "';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", alert, true);
            }
            else
            {
                SqlCommand insert = new SqlCommand();
                insert.Connection = con;
                insert.CommandText = "INSERT INTO Reviews(UserID, RecipeID, Review) VALUES(@UserID, @RecipeID, @Review) ";
                insert.Parameters.AddWithValue("@UserID", UserID);
                insert.Parameters.AddWithValue("@RecipeID", RecipeID);
                if (mark == 1)
                {
                    insert.Parameters.AddWithValue("@Review", 1);
                }
                else if (mark == 2)
                {
                    insert.Parameters.AddWithValue("@Review", 2);
                }
                else if (mark == 3)
                {
                    insert.Parameters.AddWithValue("@Review", 3);
                }
                else if (mark == 4)
                {
                    insert.Parameters.AddWithValue("@Review", 4);
                }
                else if (mark == 5)
                {
                    insert.Parameters.AddWithValue("@Review", 5);
                }
                insert.ExecuteNonQuery();
                string alert = "alert('Thank you, your mark has been saved.'); window.location = 'Recipe.aspx?r=" + RecipeID + "';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", alert, true);
                updateReviewMarkTable();
            }
        }
        catch (Exception err)
        {
            Debug.WriteLine("1234567543256432546***********   " + err.Message);
        }
        finally
        {
            con.Close();
        }
    }

    protected void ibMark1_Click(object sender, ImageClickEventArgs e)
    {
        mark = 1;
        review();
    }

    protected void ibMark2_Click(object sender, ImageClickEventArgs e)
    {
        mark = 2;
        review();
    }

    protected void ibMark3_Click(object sender, ImageClickEventArgs e)
    {
        mark = 3;
        review();
    }

    protected void ibMark4_Click(object sender, ImageClickEventArgs e)
    {
        mark = 4;
        review();
    }

    protected void ibMark5_Click(object sender, ImageClickEventArgs e)
    {
        mark = 5;
        review();
    }

    protected void calculateReviewForAGivenRecipe()
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand com = new SqlCommand();
        com.Connection = con;
        com.CommandText = "SELECT Review FROM Reviews WHERE RecipeID=@RecipeID";
        com.Parameters.AddWithValue("@RecipeID", RecipeID);
        double sumMarks = 0;
        int numMarks = 0;
        double mark;
        try
        {
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                sumMarks += Convert.ToInt32(reader["Review"]);
                numMarks++;
            }
            reader.Close();
        }
        catch (Exception err)
        {
            Debug.WriteLine("**********" + err.Message);
        }
        finally
        {
            con.Close();
            Debug.WriteLine(numMarks + " " + sumMarks / numMarks);
        }
        mark = sumMarks / numMarks;
        if (mark == 1.0)
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = ibMark3.ImageUrl = ibMark4.ImageUrl = ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if ((mark > 1) && (mark < 2))
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/half-full-star.png";
            ibMark3.ImageUrl = ibMark4.ImageUrl = ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if (mark == 2)
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = ibMark4.ImageUrl = ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if ((mark > 2) && (mark < 3))
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = "~/Images/half-full-star.png";
            ibMark4.ImageUrl = ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if (mark == 3)
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = "~/Images/full-star.png";
            ibMark4.ImageUrl = ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if ((mark > 3) && (mark < 4))
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = "~/Images/full-star.png";
            ibMark4.ImageUrl = "~/Images/half-full-star.png";
            ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if (mark == 4)
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = "~/Images/full-star.png";
            ibMark4.ImageUrl = "~/Images/full-star.png";
            ibMark5.ImageUrl = "~/Images/empty-star.png";
        }
        else if ((mark > 4) && (mark < 5))
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = "~/Images/full-star.png";
            ibMark4.ImageUrl = "~/Images/full-star.png";
            ibMark5.ImageUrl = "~/Images/half-full-star.png";
        }
        else if (mark == 5)
        {
            ibMark1.ImageUrl = "~/Images/full-star.png";
            ibMark2.ImageUrl = "~/Images/full-star.png";
            ibMark3.ImageUrl = "~/Images/full-star.png";
            ibMark4.ImageUrl = "~/Images/full-star.png";
            ibMark5.ImageUrl = "~/Images/full-star.png";
        }
    }

    protected void updateReviewMarkTable()
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand update = new SqlCommand();
        update.Connection = con;
        update.CommandText = "UPDATE Recipes SET Mark=@Mark WHERE RecipeID=@RecipeID";
        update.Parameters.AddWithValue("@RecipeID", RecipeID);
        SqlCommand com = new SqlCommand();
        com.Connection = con;
        com.CommandText = "SELECT Review FROM Reviews WHERE RecipeID=@RecipeID";
        com.Parameters.AddWithValue("@RecipeID", RecipeID);
        double sumMarks = 0;
        int numMarks = 0;
        double mark;
        try
        {
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                sumMarks += Convert.ToInt32(reader["Review"]);
                numMarks++;
            }
            reader.Close();
            mark = sumMarks / numMarks;
            update.Parameters.AddWithValue("@Mark", mark);
            update.ExecuteNonQuery();
        }
        catch (Exception err)
        {
        }
        finally
        {
            con.Close();
            Debug.WriteLine(numMarks + " " + sumMarks / numMarks);
        }
    }


    // kodot za komentarite

    // LIKES 


    protected void checkLikes()
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand com = new SqlCommand();
        SqlCommand com1 = new SqlCommand();

        com1.Connection = con;
        com.Connection = con;

        com1.CommandText = "SELECT UserID FROM Likes WHERE RecipeID=@RecipeID";
        com.CommandText = "SELECT * FROM Likes";

        numberOfLikes = 0;

        // recipeID kje mi bide od query string-ot vo load page postaven
        com1.Parameters.AddWithValue("@RecipeID", RecipeID);
        bool liked = false;
        try
        {
            con.Open();
            SqlDataReader reader = com1.ExecuteReader();
            while (reader.Read())
            {

                numberOfLikes++;
            }
            reader.Close();
            if (UserID != null)
            {
                // ako e logiran korisnik
                SqlDataReader reader1 = com.ExecuteReader();
                while (reader1.Read())
                {
                    if ((reader1["UserID"].Equals(UserID)) && (reader1["RecipeID"].ToString().Equals(RecipeID.ToString())))
                    {
                        liked = true;
                    }
                }
                reader1.Close();
            } 
                // inaku, nema potreba da se proveri sostojba na like bidejkji
                // onie koi ne se korisnici ne mozhe da lajknuvaat
            
        }
        catch (Exception err)
        {
            Debug.WriteLine(err.Message);
        }
        finally
        {
            con.Close();
            Debug.WriteLine(numberOfLikes);
        }

        if (liked)//ako ima lajknato
        {
            ib_Like.ImageUrl = "~/Images/heart-full.png";

        }
        else
        {
            ib_Like.ImageUrl = "~/Images/heart.png";
        }

        if (numberOfLikes < 0)
        {
            numberOfLikes = 0;
        }
        lblNumberLikes.Text = numberOfLikes.ToString();
    }

    protected void like()
    {
        bool flag = false;
        if (UserID != null)
        {
            // ako e logiran korisnik
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = "SELECT * FROM Likes";
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if ((reader["UserID"].Equals(UserID)) && (reader["RecipeID"].ToString().Equals(RecipeID.ToString())))
                    {
                        flag = true; //ima staveno like
                        break;
                    }
                }
                reader.Close();
                //ako userot ima staveno like, sega samo izbrisi go
                if (flag == true)
                {

                    ib_Like.ImageUrl = "~/Images/heart.png";

                    SqlCommand delete = new SqlCommand();
                    delete.Connection = con;
                    delete.CommandText = "DELETE FROM Likes WHERE UserID=@UserID AND RecipeID=@RecipeID";
                    delete.Parameters.AddWithValue("@UserID", UserID);
                    delete.Parameters.AddWithValue("@RecipeID", RecipeID);
                    delete.ExecuteNonQuery();
                    int nLikes = Convert.ToInt32(lblNumberLikes.Text);
                    lblNumberLikes.Text = (nLikes - 1).ToString();
                }
                else
                {
                    SqlCommand insert = new SqlCommand();
                    insert.Connection = con;
                    insert.CommandText = "INSERT INTO Likes(UserID, RecipeID) VALUES(@UserID, @RecipeID) ";
                    insert.Parameters.AddWithValue("@UserID", UserID);
                    insert.Parameters.AddWithValue("@RecipeID", RecipeID);
                    insert.ExecuteNonQuery();
                    ib_Like.ImageUrl = "~/Images/heart-full.png";
                    int nLikes = Convert.ToInt32(lblNumberLikes.Text);
                    lblNumberLikes.Text = (nLikes + 1).ToString();

                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Nope, not a user')", true);
            // Redirektiranje da se logira
            Response.Redirect("~/Login.aspx");
        }
        
    }

    protected void ib_Like_Click(object sender, ImageClickEventArgs e)
    {

        like();
    }

    // LIKES


    protected void numOfComments()
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand com = new SqlCommand();
        com.Connection = con;
        com.CommandText = "SELECT * FROM Comment WHERE RecipeID=@RecipeID";
        com.Parameters.AddWithValue("@RecipeID", RecipeID);
        int numComments = 0;
        try
        {
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                numComments++;
            }
            reader.Close();
            SqlCommand update = new SqlCommand();
            update.Connection = con;
            update.CommandText = "UPDATE Recipes SET NComments=@NumComments WHERE RecipeID=@RecipeID";
            update.Parameters.AddWithValue("@NumComments", numComments);
            update.Parameters.AddWithValue("@RecipeID", RecipeID);
            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                Debug.WriteLine("********" + err.Message);
            }
            Debug.WriteLine("******" + numComments);
        }
        catch (Exception err)
        {
            Debug.WriteLine("***************" + err.Message);
        }
        finally
        {
            con.Close();
        }
    }


    protected void getRecipeAuthor()
    {

        SqlConnection konekcija = new SqlConnection();
        konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand komanda = new SqlCommand();
        komanda.Connection = konekcija;
        komanda.CommandText = "SELECT * FROM [Recipes] WHERE [Recipes].RecipeID='" + RecipeID + "'";
        komanda.CommandText = "SELECT aspnet_Users.* FROM Recipes INNER JOIN aspnet_Users ON Recipes.UserID = aspnet_Users.UserID WHERE aspnet_Users.UserID ='"+AuthorID+"'";
        try
        {
            konekcija.Open();
            SqlDataReader reader = komanda.ExecuteReader();

            while (reader.Read())
            {
                AuthorName = reader["Name"].ToString();
                AuthorLastName = reader["LastName"].ToString();
            }
            reader.Close();
        }
        catch (Exception err)
        {
            // lblRecipeID.Text = err.ToString();
        }
        finally
        {
            konekcija.Close();
        }


    }
}
