using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;
using System.Collections;


public partial class ProfilePage : System.Web.UI.Page
{

    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
    SqlDataAdapter adapter;
    DataSet ds;
    DataRow data;
    public object UserID = null;
    string userId;

    /* novi sostojki */
    List<String> amounts;
    List<String> measures;
    List<String> ingrNames;
    List<String> ingrNotes;
    /* novi sostojki */

    protected void Page_Load(object sender, EventArgs e)
    {
        // ako e logiran:
        if (Membership.GetUser(User.Identity.Name) != null)
        {

            UserID = Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey;
       
        }

        if(ViewState["amounts"]!=null)
            amounts = (List<String>)ViewState["amounts"];
        else
            amounts = new List<string>();

        if(ViewState["measures"]!=null)
            measures = (List<String>)ViewState["measures"];
        else
            measures = new List<string>();

         if(ViewState["ingrNames"]!=null)
             ingrNames = (List<String>)ViewState["ingrNames"];
         else
             ingrNames = new List<string>();
          
         if(ViewState["ingrNotes"]!=null)
            ingrNotes = (List<String>)ViewState["ingrNotes"];
         else
             ingrNotes = new List<string>();

         readdIngr();


        fileUploadPic.Attributes["onchange"] = "UploadFile(this)";
        recipePicFile.Attributes["onchange"] = "UploadRecipeFile(this)";

        // load profile picture of the user who's profile page we are looking at
        userId = Request.QueryString["u"];
       
           
            if (userId != null)
            {
                if (!Page.IsPostBack) // pri prvo loadiranje
                {
                    bool result = loadProfileInfo(userId);
                    if (!result)
                    {
                        Response.Redirect("~/Homepage.aspx");
                    }
                    // se e ok so url-to, postoi korisnikot chij profil barame
                    profilePicture.Attributes["onload"] = "displayImg(this)";
                    if (!checkIfMyProfile())
                    {
                        Debug.WriteLine("**not my profile!!**");
                        editPanel.Visible = false;

                    }
                }
                else
                {
                    // samo od viewstate go zimam vekje prethodno loadiranoto (za da ne loadiram pak od baza)
                    data = ((DataSet) ViewState["dataset"]).Tables["aspnet_Users"].Rows[0];
                    if (!checkIfMyProfile())
                    {
                        Debug.WriteLine("**not my profile!!**");
                        editPanel.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Homepage.aspx");
            }
        
        
        // fill drop down list for new recipe
            if (UserID != null)
            {
                fillCategories();
            }
            fillRecipes();
            fillRecipes2();
    }

    protected void readdIngr()
    {
        // amount
        for (int i = 0; i < amounts.Count; i++)
        {
            addIngridient(amounts[i], measures[i], ingrNames[i], ingrNotes[i]);
        }
    }

    // moze i so adapter i so viewstate, ama aj xD
    protected void fillCategories()
    {
        SqlCommand select = new SqlCommand("SELECT * FROM Category", conn);
        SqlDataReader reader;
        try
        {
            conn.Open();
            reader = select.ExecuteReader();

            while (reader.Read())
            {
                categoryChooser.Items.Add(new ListItem(reader["Name"].ToString(), reader["CategoryID"].ToString()));
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            conn.Close();
        }
        
        
    }

    protected bool checkIfMyProfile()
    {
        if (UserID != null && UserID.ToString().Trim().Equals(userId))
        {
            return true;
        }

        return false;
    }

    protected void btnClose_Click(Object sender, EventArgs e)
    {
        // restartiraj - NE RABOTI 
        Debug.WriteLine("I AM IN CLOSE");
        string pic = data["profilePicture"].ToString();
        Debug.WriteLine("i'm in close and pic is: " + pic);
        showPic.Attributes.Remove("src");
        showPic.Attributes.Add("src", "User/ProfilePictures/standard/" + pic);
    }

    // make it a service
   /* protected List<float> scaleSize(int maxW, int maxH, float currW, float currH)
    {
        List<float> result = new List<float>();
        float ratio = currH / currW;
        if (currW >= maxW && ratio <= 1)
        {
            currW = maxW;
            currH = currW * ratio;
        }
        else if (currH >= maxH)
        {
            currH = maxH;
            currW = currH / ratio;
        }
        result.Add(currW);
        result.Add(currH);

        return result;
    }
    */
    protected void loadModalWithInfo()
    {

        string pic = data["profilePicture"].ToString();
        showPic.Attributes.Add("src", "User/ProfilePictures/standard/" + pic);

        if (data["Expertise"] != DBNull.Value)
        {
            switch (data["Expertise"].ToString().Trim())
            {
                case "Low":
                    {
                        culExpertise.Items[0].Selected = true;
                        break;
                    }
                case "Medium":
                    {
                        culExpertise.Items[1].Selected = true;
                        break;
                    }
                case "High":
                    {
                        culExpertise.Items[2].Selected = true;
                        break;
                    }
            }
        }

        if (data["AboutMe"] != DBNull.Value)
            aboutMe.Text = data["AboutMe"].ToString();

        if(data["Facebook"] != DBNull.Value)
            fb.Text = data["Facebook"].ToString();

        if (data["Twitter"] != DBNull.Value)
            twitter.Text = data["Twitter"].ToString();

        if (data["Linkedin"] != DBNull.Value)
            linkedin.Text = data["Linkedin"].ToString();

        if (data["Youtube"] != DBNull.Value)
            youtube.Text = data["Youtube"].ToString();

        if (data["Skype"] != DBNull.Value)
            skype.Text = data["Skype"].ToString();


    }

    protected bool loadProfileInfo(string id)
    {
        // load picture
        SqlCommand cmd = new SqlCommand("SELECT * FROM aspnet_Users " +
                                        "WHERE UserId=@UserId", conn);
        cmd.Parameters.AddWithValue("@UserId", id);
        
        adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        ds = new DataSet();

        try
        {
            conn.Open();
            adapter.Fill(ds, "aspnet_Users");
            
            data = ds.Tables["aspnet_Users"].Rows[0];
            
            if(data == null){
                Debug.WriteLine("User not found: data==null vo load info");
                // user not found, greshen query string (so brojki)
                return false;
            }

            ViewState["dataset"] = ds;

            
            // profile picture
            string profilePic;
            if (data["profilePicture"] == DBNull.Value)
            {
                Debug.WriteLine("DATA (PROFILE PIC) IS NULL");
                profilePic = "profilePicture.png";
            }
            else
            {
                Debug.WriteLine("DATA (PROFILE PIC) IS NOT NULL " + data);
                profilePic = data["profilePicture"].ToString();
            }
            profilePicture.ImageUrl = "~/User/ProfilePictures/thumbs/" + profilePic;

            // username to display
            name.InnerText = data["name"].ToString() + " " + data["lastname"].ToString();


            if (User != null && UserID != null && UserID.ToString().Equals(userId))
                // load modal info za update samo ako avtenticiraniot user se naogja na svojot profil
                loadModalWithInfo(); 
                  
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Excepion in ProfilePage: " + ex.Message);
            // ako si pishe vo url greshen username id neshto ko na facebook:
            // sorry this page isn't availale mozhe :)
            return false;
        }
        finally
        {
            conn.Close();
        }

        return true;
    }

    protected void updateProfile(object sender, EventArgs e)
    {

        bool newPic = false;
        string extension = "";

        // update napravi na slikata t.s. starata ja stavame vo tmp (ako se sluci greska pri apdejtuvanje da ne se izgubi)
        // novata ja zacuvuvame kaj sto treba i potoa starata ja brisheme od tmp
        if (fileUploadPic.HasFile)
        {
            newPic = true;
            string profilePic = fileUploadPic.FileName;
            string[] chopped = profilePic.Split('.');
            extension = chopped.ElementAt(chopped.Count() - 1);

            string path = Server.MapPath("~/User/ProfilePictures/" + User.Identity.Name + "." + extension);
            string pathStandard = Server.MapPath("~/User/ProfilePictures/standard/" + User.Identity.Name + "." + extension);
            string pathThumb = Server.MapPath("~/User/ProfilePictures/thumbs/" + User.Identity.Name + "." + extension);
            
            // starata originalna ja brisham
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else if (System.IO.File.Exists(pathStandard))
            {
                System.IO.File.Delete(pathStandard);
            }
            else if (System.IO.File.Exists(pathThumb))
            {
                System.IO.File.Delete(pathThumb);
            }

            // ja zacuvuvam originalnata nova
            fileUploadPic.PostedFile.SaveAs(path);

            // resizing thumbs
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            ImageFormat imgFormat = img.RawFormat;
            //___________________________________________________________________________________________________________________________________SERVICE____________________           

            SizeScaleService ssService = new SizeScaleService();
            

            List<float> scaledSize = ssService.scaleSize(200, 200, img.Width, img.Height);
            Bitmap newImg = new Bitmap(img, (int)scaledSize[0], (int)scaledSize[1]);
            string destPath = Server.MapPath("~/User/ProfilePictures/thumbs/" + User.Identity.Name + "." + extension);
            newImg.Save(destPath, imgFormat);

            // resizing standard
            img = System.Drawing.Image.FromFile(path);
            imgFormat = img.RawFormat;

            ssService.scaleSize(300, 300, img.Width, img.Height);
 //___________________________________________________________________________________________________________________________________SERVICE____________________           
            newImg = new Bitmap(img, (int)scaledSize[0], (int)scaledSize[1]);
            destPath = Server.MapPath("~/User/ProfilePictures/standard/" + User.Identity.Name + "." + extension);
            newImg.Save(destPath, imgFormat);

            // sega na serverska strana zacuvana mi e novata slika
        }
        
        
        string updateCmd = "UPDATE aspnet_Users SET ProfilePicture=@ProfilePicture, " +
                           "Expertise=@Expertise, AboutMe=@AboutMe, Facebook=@Facebook, " +
                           "Twitter=@Twitter, Linkedin=@Linkedin, Youtube=@Youtube, " +
                           "Skype=@Skype WHERE UserId=@UserId";

        SqlCommand cmd = new SqlCommand(updateCmd, conn);
        if (newPic)
        {
            cmd.Parameters.AddWithValue("ProfilePicture", User.Identity.Name + "." + extension);
        }
        else
        {
            // istata ostanuva
            cmd.Parameters.AddWithValue("ProfilePicture", data["ProfilePicture"].ToString());
        }

        if (culExpertise.SelectedItem != null)
            cmd.Parameters.AddWithValue("Expertise", culExpertise.SelectedItem.Text);
        else cmd.Parameters.AddWithValue("Expertise", DBNull.Value);

        Debug.WriteLine("ABOUT ME: " + aboutMe.Text);
        if(aboutMe.Text.Trim() != "")
            cmd.Parameters.AddWithValue("AboutMe",  aboutMe.Text.Trim());
        else cmd.Parameters.AddWithValue("AboutMe", DBNull.Value);

        if (fb.Text.Trim() != "")
            cmd.Parameters.AddWithValue("Facebook", fb.Text.Trim());
        else cmd.Parameters.AddWithValue("Facebook", DBNull.Value);

        if (twitter.Text.Trim() != "")
            cmd.Parameters.AddWithValue("Twitter", twitter.Text.Trim());
        else cmd.Parameters.AddWithValue("Twitter", DBNull.Value);

        if (linkedin.Text.Trim() != "")
            cmd.Parameters.AddWithValue("Linkedin", linkedin.Text.Trim());
        else cmd.Parameters.AddWithValue("Linkedin", DBNull.Value);

        if (youtube.Text.Trim() != "")
            cmd.Parameters.AddWithValue("Youtube", youtube.Text.Trim());
        else cmd.Parameters.AddWithValue("Youtube", DBNull.Value);

        if (skype.Text.Trim() != "")
            cmd.Parameters.AddWithValue("Skype", skype.Text.Trim());
        else cmd.Parameters.AddWithValue("Skype", DBNull.Value);

        cmd.Parameters.AddWithValue("UserId", UserID);

        try
        {
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            Debug.WriteLine("AFTER UPDATE: " + result);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            conn.Close();
        }

        loadProfileInfo(userId);

        
    }

    protected void profilePicture_DataBinding(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('DATA BINDING')", true);
        profilePicture.Visible = true;

    }
    protected void profilePicture_Load(object sender, EventArgs e)
    {
        ////ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EVERYTHING IS LOADED')", true);
        //profilePicture.Visible = true;
    }



    /* NEW RECIPE */

    protected void addIngridient(string amountText, string measureText, string nameText, string notesText)
    {
        // adding buttons 

        // amount
        Button btnAmount = new Button();
        btnAmount.Text = amountText;
        btnAmount.Attributes.Add("class", "button button-action button-rounded button-small");
        btnAmount.Attributes.Add("style", "margin-right:5px");
        pnlAmount.Controls.Add(btnAmount);
        pnlAmount.Controls.Add(new LiteralControl("<br />"));
        pnlAmount.Controls.Add(new LiteralControl("<br />"));


        // measures
        Button btnMeasures = new Button();
        btnMeasures.Text = measureText;
        btnMeasures.Attributes.Add("class", "button button-action button-rounded button-small");
        btnMeasures.Attributes.Add("style", "margin-right:5px");
        pnlMeasure.Controls.Add(btnMeasures);
        pnlMeasure.Controls.Add(new LiteralControl("<br />"));
        pnlMeasure.Controls.Add(new LiteralControl("<br />"));

        // names
        Button btnName = new Button();
        btnName.Text = nameText;
        btnName.Attributes.Add("class", "button button-action button-rounded button-small");
        btnName.Attributes.Add("style", "margin-right:5px");
        pnlIngrName.Controls.Add(btnName);
        pnlIngrName.Controls.Add(new LiteralControl("<br />"));
        pnlIngrName.Controls.Add(new LiteralControl("<br />"));

        
        // notes
        Button btnNotes = new Button();
        if (notesText.Trim() != "")
            btnNotes.Text = notesText;
        else
            btnNotes.Text = "x";
        btnNotes.Attributes.Add("class", "button button-action button-rounded button-small");
        btnNotes.Attributes.Add("style", "margin-right:5px");
        pnlNotes.Controls.Add(btnNotes);

        
        pnlNotes.Controls.Add(new LiteralControl("<br />"));
        pnlNotes.Controls.Add(new LiteralControl("<br />"));

    }

    protected void btnAddIngr_OnClick(object sendeer, EventArgs e)
    {
        if (amount.Text.Trim() != "" && measure.Text.Trim() != "" && ingredient_name.Text.Trim() != "")
        {
            // mozhe nova sostojka
            amounts.Add(amount.Text.Trim());
            measures.Add(measure.Text.Trim());
            ingrNames.Add(ingredient_name.Text.Trim());
            ingrNotes.Add(notes.Text.Trim());

            ViewState["amounts"] = amounts;
            ViewState["measures"] = measures;
            ViewState["ingrNames"] = ingrNames;
            ViewState["ingrNotes"] = ingrNotes;

            //// adding buttons 

            addIngridient(amount.Text, measure.Text, ingredient_name.Text, notes.Text);

            //// amount
            //Button btnAmount = new Button();
            //btnAmount.Text = amount.Text;
            //btnAmount.Attributes.Add("class", "button button-action button-rounded button-small");
            //btnAmount.Attributes.Add("style", "margin-right:5px");
            //pnlAmount.Controls.Add(btnAmount);
            //pnlAmount.Controls.Add(new LiteralControl("<br />"));
 
            //// measures
            //Button btnMeasures = new Button();
            //btnMeasures.Text = measure.Text;
            //btnMeasures.Attributes.Add("class", "button button-action button-rounded button-small");
            //btnMeasures.Attributes.Add("style", "margin-right:5px");
            //pnlMeasure.Controls.Add(btnMeasures);
            //pnlMeasure.Controls.Add(new LiteralControl("<br />"));

            //// names
            //Button btnName = new Button();
            //btnName.Text = ingredient_name.Text;
            //btnName.Attributes.Add("class", "button button-action button-rounded button-small");
            //btnName.Attributes.Add("style", "margin-right:5px");
            //pnlIngrName.Controls.Add(btnName);
            //pnlIngrName.Controls.Add(new LiteralControl("<br />"));

            //// notes
            //Button btnNotes = new Button();
            //btnNotes.Text = notes.Text;
            //btnNotes.Attributes.Add("class", "button button-action button-rounded button-small");
            //btnNotes.Attributes.Add("style", "margin-right:5px");
            //pnlNotes.Controls.Add(btnNotes);
            //pnlNotes.Controls.Add(new LiteralControl("<br />"));



            amount.Text = "";
            measure.Text = "";
            ingredient_name.Text = "";
            notes.Text = "";

        }
        else
        {
            // error 
            alertNoEnteredIngr.Visible = true;
            string jsFunc = "hideAgain()";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFunc", jsFunc, true);
        }
    }

   

    /* NEW RECIPE */

    protected void addRecipeInRecipes()
    {
        string insertRecipe = "INSERT INTO Recipes (UserID, Title, Description, Servings, TimePreparation, ImageURL, Status, CategoryID, Mark, NComments) " +
                              "VALUES (@Uid,@title, @desc, @servs, @time, @imgName, @st, @Cid, @mark, @ncomm)";
        SqlCommand cmdRecipe = new SqlCommand(insertRecipe, conn);

        cmdRecipe.Parameters.AddWithValue("@Uid", UserID.ToString());
        cmdRecipe.Parameters.AddWithValue("@title", recipetitle.Text.Trim());
        cmdRecipe.Parameters.AddWithValue("@desc", TextBox2.Text.Trim());
        cmdRecipe.Parameters.AddWithValue("@servs", Convert.ToInt16(numberservings.Text.Trim()));
        cmdRecipe.Parameters.AddWithValue("@time", preptime.Text.Trim());
        cmdRecipe.Parameters.AddWithValue("@st", "False"); // treba admin da go approve
        cmdRecipe.Parameters.AddWithValue("@Cid", categoryChooser.SelectedValue);
        cmdRecipe.Parameters.AddWithValue("@mark", 0);
        cmdRecipe.Parameters.AddWithValue("@ncomm", 0);


        // recipe image
        if (recipePicFile.HasFile)
        {
            string profilePic = recipePicFile.FileName;
            string[] chopped = profilePic.Split('.');
            string extension = chopped.ElementAt(chopped.Count() - 1);

            // ja zacuvuvam original vo tmp
            // tie sto si gi stavivme manuelno kje ni bidat so recipe id,
            // drugive so guid

            Guid guid = Guid.NewGuid();
            string path = Server.MapPath("~/Images/recipesImages/tmp/" + guid + "." + extension);
            recipePicFile.PostedFile.SaveAs(path);

            // resizing 350-350
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            ImageFormat imgFormat = img.RawFormat;
//________________________________________________________________________________________________________________________________SERVICE____________________
            SizeScaleService ssService = new SizeScaleService();
            
            List<float> scaledSize = ssService.scaleSize(450, 450, img.Width, img.Height);
 //________________________________________________________________________________________________________________________________SERVICE____________________

            Bitmap newImg = new Bitmap(img, (int)scaledSize[0], (int)scaledSize[1]);
            string destPath = Server.MapPath("~/Images/recipesImages/" + guid + "." + extension);
            newImg.Save(destPath, imgFormat);


            //// izbrishi od tmp originalna
            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}

            cmdRecipe.Parameters.AddWithValue("@imgName", guid + "." + extension);

            Debug.WriteLine("*************** IMAGE *******************");

        }
        else
        {
            Debug.WriteLine("*************** NO IMAGE *******************");
        }

        try
        {
            conn.Open();
            cmdRecipe.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    protected int addIngredientsInRecipIngr()
    {
        // zemi go receptot shto posleden sum go dodala
        // zemi gi site moi recepti i zemi go onoj so najgolem ID
        string getMyRecipes = "SELECT * FROM Recipes WHERE UserID=@UserId";
        SqlCommand cmd = new SqlCommand(getMyRecipes, conn);
        List<int> myRecipesIds = new List<int>();
        int lastID = -1;
        if (UserID != null)
        {
            cmd.Parameters.AddWithValue("@UserId", UserID);
            
            try
            {
                conn.Open();
                SqlDataReader data = cmd.ExecuteReader();
                //if(UserID.ToString().Equals(userId))
                Debug.WriteLine("IN INGR USER = " + UserID.ToString());
                Debug.WriteLine("IN INGR USER = " + UserID);

                while (data.Read())
                {
                    Debug.WriteLine("IN INGR " + data["RecipeID"].ToString());
                    myRecipesIds.Add(Convert.ToInt16(data["RecipeID"].ToString()));
                }

                lastID = myRecipesIds.Max();
               
                data.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            // add in RecipeIngredients za sekoja sostojka posebno
            for (int i = 0; i < amounts.Count; i++)
            {
                string insertRecipe = "INSERT INTO RecipeIngredients (RecipeID, Amount, Units, IngredientName, Notes) " +
                                "VALUES (@Rid,@amount, @units, @ingrName, @notes)";
                SqlCommand cmdIns = new SqlCommand(insertRecipe, conn);

                cmdIns.Parameters.AddWithValue("@Rid", lastID.ToString()); // proverka da ne e -1 :/
                if(amounts[i] != "")
                cmdIns.Parameters.AddWithValue("@amount", amounts[i]);
                else
                cmdIns.Parameters.AddWithValue("@amount", DBNull.Value);

                if(measures[i] != "")
                cmdIns.Parameters.AddWithValue("@units", measures[i]);
                else
                cmdIns.Parameters.AddWithValue("@units", DBNull.Value);

                if (ingrNames[i] != "") 
                cmdIns.Parameters.AddWithValue("@ingrName", ingrNames[i]);
                else
                    cmdIns.Parameters.AddWithValue("@ingrName", DBNull.Value);

                if(ingrNotes[i] != "")
                cmdIns.Parameters.AddWithValue("@notes", ingrNotes[i]);
                else
                    cmdIns.Parameters.AddWithValue("@notes", DBNull.Value);

                try
                {
                    conn.Open();
                    cmdIns.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return lastID;
        }
        else
        {
            // error
            Debug.WriteLine("USER ID == NULL IN ADDING INGREDIENTS IN RECIPEINGREDIENTS");
            return -1;
        }
        
    }

    protected void addSteps(int recipeID)
    {
        string allSteps = directions.Text;
        string[] chopped = allSteps.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        
     
        foreach (string step in chopped)
        {
            string insertStep = "INSERT INTO StepsPreparation (RecipeID, Description) " +
                                "VALUES (@Rid,@desc)";
            SqlCommand cmdIns = new SqlCommand(insertStep, conn);
            cmdIns.Parameters.AddWithValue("@Rid", recipeID);
            cmdIns.Parameters.AddWithValue("@desc", step);

            try
            {
                conn.Open();
                cmdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }

    protected void btnSaveRecipe_OnClick(object sender, EventArgs e)
    {
       // RECIPES TABELA
        addRecipeInRecipes();
       // vo RecipeIngredients tabela
        int recipeID = addIngredientsInRecipIngr();
       // vo StepsPreparation tabela
        addSteps(recipeID);

        preptime.Text = "";
        numberservings.Text = "";
       // categoryChooser.Text = "";
        recipetitle.Text = "";
        directions.Text = "";
        amount.Text = "";
        measure.Text = "";
        ingredient_name.Text = "";
        notes.Text = "";
    }

    private void fillRecipes()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT * FROM [Recipes] WHERE Status=@Status AND Recipes.UserID=@UserID";
     
        command.Parameters.AddWithValue("@Status", "True");
        command.Parameters.AddWithValue("@UserID", userId);
        DataTable dt = new DataTable();
        SqlDataAdapter adapt = new SqlDataAdapter();
        adapt.SelectCommand = command;
        adapt.Fill(dt);
        con.Close();
        PagedDataSource pds = new PagedDataSource();
        DataView dv = new DataView(dt);
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.PageSize = 12;
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
        Repeater1.DataSource = pds;
        Repeater1.DataBind();
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
        fillRecipes();
    }

    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SelectRecipe")
        {
            Response.Redirect("Recipe.aspx?r=" + e.CommandArgument);
        }
    }

    private void fillRecipes2()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        command.CommandText = "SELECT Recipes.* FROM Recipes INNER JOIN Likes ON Recipes.RecipeID=Likes.RecipeID WHERE Likes.UserID=@UserID";

        command.Parameters.AddWithValue("@Status", "True");
        command.Parameters.AddWithValue("@UserID", userId);
        DataTable dt = new DataTable();
        SqlDataAdapter adapt = new SqlDataAdapter();
        adapt.SelectCommand = command;
        adapt.Fill(dt);
        con.Close();
        PagedDataSource pds = new PagedDataSource();
        DataView dv = new DataView(dt);
        pds.DataSource = dv;
        pds.AllowPaging = true;
        pds.PageSize = 12;
        pds.CurrentPageIndex = PageNumber;
        if (pds.PageCount > 1)
        {
            rptPaging2.Visible = true;
            ArrayList arraylist = new ArrayList();
            for (int i = 0; i < pds.PageCount; i++)
                arraylist.Add((i + 1).ToString());
            rptPaging2.DataSource = arraylist;
            rptPaging2.DataBind();
        }
        else
        {
            rptPaging2.Visible = false;
        }
        Repeater2.DataSource = pds;
        Repeater2.DataBind();
    }

    protected void rptPaging2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
        fillRecipes2();
    }

    protected void LinkButton2_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SelectRecipe")
        {
            Response.Redirect("Recipe.aspx?r=" + e.CommandArgument);
        }
    }
}