using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Diagnostics;

public partial class Repeater : System.Web.UI.Page
{
    public String MomentalnaKategorijaID;
    public String MomentalnaKategorijaName;
    public String MomentalnaKategorijaDescription;

    //novo
    public string title;
    public string description;
    string flag; //category za listanje kategorija; recent za listanje najnovi recepti i review za listanje spored review

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToInt32(Request.QueryString["C"]) < 24) && (Convert.ToInt32(Request.QueryString["C"]) > 0))
        {


            MomentalnaKategorijaID = Request.QueryString["C"].ToString();
            //DataSourceCategory.SelectCommand = "SELECT * FROM [Recipes]  WHERE [Recipes].CategoryID='" + MomentalnaKategorijaID + "'";
            flag = "category";
            // DataSourceCategory.SelectCommand = "SELECT  [Recipes$].RecipeID, [Recipes$].Title,  [Recipes$].Description AS RecipeDescription, [Recipes$].ImageURL, [Sheet1$].Name AS CategoryName,   [Sheet1$].Description AS CategoryDescription  FROM [Recipes$] INNER JOIN [Sheet1$] ON [Recipes$].CategoryID=[Sheet1$].CategoryID WHERE [Recipes$].CategoryID='" + MomentalnaKategorija + "'";
            fillRecipes();
        }
        else if (Convert.ToString(Request.QueryString["type"]) == "recent")
        {
            //DataSourceCategory.SelectCommand = "SELECT top 20 * FROM [Recipes] ORDER BY RecipeID DESC";
            flag = "recent";
            fillRecipes();
        }
        else if (Convert.ToString(Request.QueryString["type"]) == "review")
        {
            flag = "review";
            fillRecipes();
        }
        else if (Session["Recipes"] != null)
        {
            // togash search by ingredient
            flag = "session";
            fillRecipes();
        }
        else
        {
            //error
            Response.Redirect("~/Homepage.aspx");
        }
        if (!IsPostBack)
        {
            fillRecipes();
        }

        getInfoForCategory(MomentalnaKategorijaID);
        this.DataBind();
    }

    protected void getInfoForCategory(String categorijaID)
    {
        if (flag.Equals("category"))
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlCommand komanda = new SqlCommand();
            komanda.Connection = konekcija;
            komanda.CommandText = "SELECT * FROM [Category] WHERE [Category].CategoryID='" + categorijaID + "'";
            try
            {
                konekcija.Open();
                SqlDataReader reader = komanda.ExecuteReader();
                while (reader.Read())
                {
                    title = MomentalnaKategorijaName = reader["Name"].ToString();
                    description = MomentalnaKategorijaDescription = reader["Description"].ToString();
                }
                reader.Close();
            }
            catch (Exception err)
            {
                Debug.WriteLine("error: " + err.Message);
                //lblPoraka.Text = err.ToString();
            }
            finally
            {
                konekcija.Close();
            }
        }
        else if (flag.Equals("recent"))
        {
            title = "The newest one";
            description = "Here you can find the 20 newest recipes posted by our users";
        }
        else if (flag.Equals("review"))
        {
            title = "The most delicious one";
            description = "Users put this recepts among the most delicious one";
        }
        else if (flag.Equals("session"))
        {
            title = "Recipes";
            description = "Recipes searched by ingredients";
        }


    }

    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "SelectRecipe")
        {
            Response.Redirect("Recipe.aspx?r=" + e.CommandArgument);
        }
    }


    private void fillRecipes()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        if (flag.Equals("category"))
        {
            command.CommandText = "SELECT * FROM [Recipes]  WHERE [Recipes].CategoryID='" + MomentalnaKategorijaID + "' AND Status=@Status";
        }
        else if (flag.Equals("recent"))
        {
            command.CommandText = "SELECT * FROM [Recipes] WHERE Status=@Status ORDER BY RecipeID DESC";
        }
        else if (flag.Equals("review"))
        {
            command.CommandText = "SELECT  * FROM [Recipes] WHERE Status=@Status ORDER BY Mark DESC";
        }
        else if(flag.Equals("session"))
        {
            ArrayList recipes = (ArrayList) Session["Recipes"];
            string cmdText = "SELECT * FROM [Recipes] WHERE ";
            for (int i = 0; i < recipes.Count-1; i++ )
            {
                cmdText += "RecipeID=" + recipes[i] + " OR ";
            }
            cmdText += "RecipeID=" + recipes[recipes.Count - 1];
            command.CommandText = cmdText;
        }

        command.Parameters.AddWithValue("@Status", "True");

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

    protected void fillReviewsForRecipes()
    {

    }
}