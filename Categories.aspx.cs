using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Categories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblKategorija.Text = lvCategorii.DataKeys[lvCategorii.SelectedIndex].Value.ToString();
        // Session["momentalnaKategorija"]= lvCategorii.DataKeys[lvCategorii.SelectedIndex].Value;
        Response.Redirect("Repeater.aspx?C=" + lvCategorii.DataKeys[lvCategorii.SelectedIndex].Value.ToString());
    }

    protected void lvCategorii_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        //lblKategorija.Text = lvCategorii.SelectedDataKey.ToString();

        //lblKategorija.Text = lvCategorii.DataKeys[lvCategorii.SelectedIndex].Value.ToString();
    }
}