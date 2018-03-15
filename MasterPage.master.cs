using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Diagnostics;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            HyperLink profile = (HyperLink)homeView.FindControl("hyperProfile");
            if (profile != null)
            {
                // zemi id na logiran korisnik
                string userId = Membership.GetUser().ProviderUserKey.ToString();
                // zemi id na korisnik so username
                //MembershipUser mu = Membership.GetUser("username");
                //string userId = mu.ProviderUserKey.ToString();
                profile.NavigateUrl = string.Format("~/ProfilePage.aspx?u={0}", userId);

            }
            else
            {
                // javi greshka ili vrati go na prethodnata strana - najdobro :)
            }
        }
    
    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Homepage.aspx");
    }

    protected void btnNewProjectCancel_Click(object sender, EventArgs e)
    {
        Debug.WriteLine("cancel");
    }

    protected void btnNewProjectSubmit_Click(object sender, EventArgs e)
    {
        Debug.WriteLine("submit");
    }


    protected string autoComplete()
    {
        Debug.WriteLine("AUTO COMPLETE");
        string vrati = null;
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT Title FROM Recipes WHERE Status=@Status";
        cmd.Parameters.AddWithValue("@Status", "True");
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            adapter.Fill(ds, "Recepti");
            int i = 0;
            foreach (DataRow dr in ds.Tables["Recepti"].Rows)
            {
                vrati = vrati + dr[0] + ";";
                // Debug.WriteLine(vrati[i]);
            }
        }
        catch (Exception err)
        {
            // Debug.WriteLine("**************" + err.Message);
        }
        finally
        {
            con.Close();
        }

        Debug.WriteLine(vrati);

        return vrati;
    }
    protected void searchIcon_OnClick(object sender, EventArgs e)
    {
        if (autocomplete.Text.Trim() != "")
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string cmdSelect = "SELECT RecipeID FROM Recipes WHERE Title=@title";
            SqlCommand command = new SqlCommand(cmdSelect, conn);

            command.Parameters.AddWithValue("@title", autocomplete.Text.Trim());

            string RecipeId = ""; 

            try
            {
                conn.Open();
                SqlDataReader data = command.ExecuteReader();
                data.Read();
              
                // treba samo eden vakov recept da ima
                RecipeId = data["RecipeID"].ToString();
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

            Response.Redirect("~/Recipe.aspx?r=" + RecipeId);
        }
        else
        {
            // error ama nishto nema da se dava :) da ne se overload dizajnot
        }
    }
}
