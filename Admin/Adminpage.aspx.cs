using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Adminpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            ispolniGridView();
            gvSteps.Visible = false;
        }
    }

    protected void ispolniGridView()
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM [Recipes] ORDER BY RecipeID DESC";

        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            adapter.Fill(ds, "Recipes");
            gvRecipes.DataSource = ds;
            gvRecipes.DataBind();
            ViewState["dataset"] = ds;
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
        }
        finally
        {
            connection.Close();
        }
    }

    protected void gvRecipes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dataset"];
        gvRecipes.EditIndex = e.NewEditIndex;
        gvRecipes.DataSource = ds;
        gvRecipes.DataBind();
    }

    protected void gvRecipes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dataset"];
        gvRecipes.EditIndex = -1;
        gvRecipes.DataSource = ds;
        gvRecipes.DataBind();
    }

    protected void gvRecipes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        string sqlString = "UPDATE [Recipes] SET Status=@Status WHERE RecipeID=@RecipeID";
        SqlCommand command = new SqlCommand(sqlString, connection);
        CheckBox cb = (CheckBox)gvRecipes.Rows[e.RowIndex].Cells[6].Controls[0];
        if (cb.Checked)
        {
            command.Parameters.AddWithValue("@Status", "True");
        }
        else
        {
            command.Parameters.AddWithValue("@Status", "False");
        }
        
        command.Parameters.AddWithValue("@RecipeID", gvRecipes.DataKeys[gvRecipes.EditIndex].Value);
        int efekt = 0;
        try
        {
            connection.Open();
            efekt = command.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            lblMessage.Text += err.Message;
        }
        finally
        {
            connection.Close();
            gvRecipes.EditIndex = -1;
        }
        if (efekt != 0)
        {
            ispolniGridView();
        }
    }

    protected void gvRecipes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ispolniSoIngredients();
        ispolniSoSteps();
        gvIngredients.Visible = true;
        gvSteps.Visible = true;
    }

    protected void gvRecipes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecipes.PageIndex = e.NewPageIndex;
        gvRecipes.SelectedIndex = -1;
        DataSet ds = (DataSet)ViewState["dataset"];
        gvRecipes.DataSource = ds;
        gvRecipes.DataBind();
        gvSteps.Visible = false;
        gvIngredients.Visible = false;
    }

    protected void ispolniSoIngredients()
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM [RecipeIngredients] WHERE RecipeID=@RecipeID";
        command.Parameters.AddWithValue("@RecipeID", gvRecipes.DataKeys[gvRecipes.SelectedIndex].Value);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            adapter.Fill(ds, "Ingredients");
            gvIngredients.DataSource = ds;
            gvIngredients.DataBind();
            ViewState["ingredients"] = ds;
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
        }
        finally
        {
            connection.Close();
        }
    }

    protected void gvIngredients_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIngredients.PageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)ViewState["ingredients"];
        gvIngredients.DataSource = ds;
        gvIngredients.DataBind();
    }

    protected void ispolniSoSteps()
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM [StepsPreparation] WHERE RecipeID=@RecipeID";
        command.Parameters.AddWithValue("@RecipeID", gvRecipes.DataKeys[gvRecipes.SelectedIndex].Value);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = command;
        DataSet ds = new DataSet();
        try
        {
            connection.Open();
            adapter.Fill(ds, "Steps");
            gvSteps.DataSource = ds.Tables["Steps"];
            gvSteps.DataBind();
            ViewState["steps"] = ds;
        }
        catch (Exception err)
        {
            lblMessage.Text = err.Message;
        }
        finally
        {
            connection.Close();
        }
    }
}