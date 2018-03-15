using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;

public partial class Homepage : System.Web.UI.Page
{
    // homepage-ot e dostapen za site, no samo korisnicite kje imaat dopolnitelni
    // privilegii: dodavanje nov recept, like na daden recept, zachuvuvanje na omileni recepti
    int counter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            counter = 0;
            plusIngr.Visible = false;
            minusIngr.Visible = false;
            plusIngr.Items.Clear();
            minusIngr.Items.Clear();           
        }

        if (Page.IsPostBack)
        {
            // re-add buttons [ingredients] in PlaceHolder 
            if (ViewState["ctrls"] != null)
            {
                string[] ingrs = ViewState["ctrls"].ToString().Split(';');
                foreach (string ingr in ingrs)
                {
                    addItemsPlus(ingr);
                }
            }

            // re-add buttons [ingredients] in PlaceHolder 
            if (ViewState["ctrlsMinus"] != null)
            {
                string[] ingrs = ViewState["ctrlsMinus"].ToString().Split(';');
                foreach (string ingr in ingrs)
                {
                    addItemsMinus(ingr);
                }
            }
        }
    }

    // add buttons controls in PlaceHolder for plus
    protected void addItemsPlus(String text)
    {
        Button newItem = new Button();
        newItem.Text = text;
        Guid guid = Guid.NewGuid();
        newItem.ID = guid.ToString();
        newItem.Attributes.Add("class", "button button-action button-rounded button-small");
        newItem.Attributes.Add("style", "margin-right:5px");
        ingrPH.Controls.Add(newItem);
    }

    // add buttons controls in PlaceHolder for minus
    protected void addItemsMinus(string text)
    {
        Button newItem = new Button();
        newItem.Text = text;
        Guid guid = Guid.NewGuid();
        newItem.ID = guid.ToString();
        newItem.Attributes.Add("class", "button button-caution  button-rounded button-small");
        newItem.Attributes.Add("style", "margin-right:5px");
        ingrPHMinus.Controls.Add(newItem);
    }

    protected void btnPlus_Click(object sender, EventArgs e)
    {
     
       
        if (searchIngrPlus.Text.Trim() != "")
        {
            // addni button
            addItemsPlus(searchIngrPlus.Text);

            if (ViewState["ctrls"] != null)
            {
                ViewState["ctrls"] += ";" + searchIngrPlus.Text;
            }
            else
            {
                ViewState["ctrls"] = searchIngrPlus.Text;
            }
           

            plusIngr.Items.Add(searchIngrPlus.Text);
            searchIngrPlus.Text = "";
            plusIngr.Visible = true;
            plusIngr.Rows = plusIngr.Items.Count;
        }

    }
    protected void btnMinus_Click(object sender, EventArgs e)
    {
        if (searchIngrMinus.Text.Trim() != "")
        {
            // addni button
            addItemsMinus(searchIngrMinus.Text);

            if (ViewState["ctrlsMinus"] != null)
            {
                ViewState["ctrlsMinus"] += ";" + searchIngrMinus.Text;
            }
            else
            {
                ViewState["ctrlsMinus"] = searchIngrMinus.Text;
            }

            minusIngr.Items.Add(searchIngrMinus.Text);
            searchIngrMinus.Text = "";
            minusIngr.Visible = true;
            minusIngr.Rows = minusIngr.Items.Count;
        }
    }

    protected void btnSearchIngr_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        command.Connection = conn;
        ArrayList includeIngr = new ArrayList();
        ArrayList exludeIngr = new ArrayList();
        for (int i = 0; i < plusIngr.Items.Count; i++ )
        {
            includeIngr.Add(plusIngr.Items[i].Text);
        }
        for (int i = 0; i < minusIngr.Items.Count; i++)
        {
            exludeIngr.Add(minusIngr.Items[i].Text);
        }
        string stringIngrPlus = "";
        string name;
        for (int i = 0; i < includeIngr.Count-1; i++)
        {
            name = "@IngredientName" + i;
            stringIngrPlus += "IngredientName=" + name + " OR ";
            
            command.Parameters.AddWithValue(name, includeIngr[i]);
        }
        stringIngrPlus += "IngredientName=@IngredientName";
        command.Parameters.AddWithValue("@IngredientName", includeIngr[includeIngr.Count - 1]);
        command.CommandText = "SELECT Recipes.RecipeID FROM Recipes INNER JOIN RecipeIngredients ON  Recipes.RecipeID=RecipeIngredients.RecipeID WHERE "+stringIngrPlus;
  
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            adapter.Fill(ds, "RecipesAndIngredients");

            //za exclude na ingredients 
            ArrayList finalRecipes = new ArrayList(); 
            ArrayList workingIngrRecipe = new ArrayList(); // sosotojki na receptot koj go izminuvame
            
            for (int i = 0; i < ds.Tables["RecipesAndIngredients"].Rows.Count; i++ )
            {
                bool flag = false;
                Debug.WriteLine("RECEPT: " + ds.Tables["RecipesAndIngredients"].Rows[i]["RecipeID"]);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM RecipeIngredients WHERE RecipeID=" + ds.Tables["RecipesAndIngredients"].Rows[i]["RecipeID"];
                cmd.Connection = conn;
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                DataSet ds1 = new DataSet();
                adapter1.SelectCommand = cmd;
                adapter1.Fill(ds1, "Recipes");
                for (int j = 0; j < ds1.Tables["Recipes"].Rows.Count; j++)
                {
                    //Debug.WriteLine("   SOSTOJKA: " + ds1.Tables["Recipes"].Rows[j]["IngredientName"]);

                    if (exludeIngr.Contains(ds1.Tables["Recipes"].Rows[j]["IngredientName"]))
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    // super e receptov, nema excluded ingredients
                    finalRecipes.Add(ds.Tables["RecipesAndIngredients"].Rows[i]["RecipeID"]);
                }
            }

           
            Session["Recipes"] = finalRecipes;
            Response.Redirect("~/Repeater.aspx");

        }
        catch (Exception err)
        {
            Debug.WriteLine(err.Message);
        }
        finally
        {
            conn.Close();
        }
     }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string email = "abc@abc.com";
        ClientScript.RegisterStartupScript(this.GetType(), "mailto", "parent.location='mailto:" + email + "'", true);

        TextArea1.Text = "";
        name.Text = "";

    }
}