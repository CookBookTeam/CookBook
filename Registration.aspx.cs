using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Profile;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Register : System.Web.UI.Page
{

    CreateUserWizardStep step;

    protected void Page_Load(object sender, EventArgs e)
    {

        CreateUserWizardStep step = (CreateUserWizardStep)RegisterUser.FindControl("step1");
        if (step != null)
        {
            // bind-uva so javascript delot za da se pojavi slikata pri upload :)
            FileUpload uploadedPicture = (FileUpload)step.ContentTemplateContainer.FindControl("profilePicture");
            uploadedPicture.Attributes["onchange"] = "UploadFile(this)";
        }
    }

    protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
    {


        
    }
    protected void RegisterUser_DataBinding(object sender, EventArgs e)
    {

    }

    // make it a service
    protected List<float> scaleSize(int maxW, int maxH, float currW, float currH)
    {
        List<float> result = new List<float>();
        float ratio = currH / currW;
        if (currW >= maxW && ratio <= 1) {
            currW = maxW;
            currH = currW * ratio;
        } else if (currH >= maxH) {
            currH = maxH;
            currW = currH / ratio;
        }
        result.Add(currW);
        result.Add(currH);

        return result;
    }

    /*
        se koristi za da se apdejtira stotuku zacuvanata torka vo bazata
     *  taka shto imeto na slikata se cuva vo baza (za podocna da se pristapi)
     *  + first name i last name 
     */
    protected void storeExtra(string profilePic)
    {
        // get user id of just added user
        MembershipUser newUser = Membership.GetUser(RegisterUser.UserName);
        Guid newUserId = (Guid)newUser.ProviderUserKey;

        string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        string updateSQL = updateSQL = "UPDATE aspnet_Users SET ProfilePicture=@ProfilePic, Name=@Name, LastName=@LastName WHERE UserId=@userId";

      

        SqlConnection myConnection = new SqlConnection(connectionString);
        SqlCommand myCommand = new SqlCommand(updateSQL, myConnection);

        myCommand.Parameters.AddWithValue("@UserId", newUserId);
        myCommand.Parameters.AddWithValue("@ProfilePic", profilePic);
        myCommand.Parameters.AddWithValue("@Name", ((TextBox)step.ContentTemplateContainer.FindControl("Name")).Text);
        myCommand.Parameters.AddWithValue("@LastName", ((TextBox)step.ContentTemplateContainer.FindControl("LastName")).Text);
      
        try
        {

            myConnection.Open();
            myCommand.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Debug.WriteLine("EXCEPTION is: " + ex.Message);
        }
        finally
        {
            myConnection.Close();
        }

        
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        step = (CreateUserWizardStep)RegisterUser.FindControl("step1");
        if (step != null)
        {
            FileUpload uploadedPicture = (FileUpload)step.ContentTemplateContainer.FindControl("profilePicture");
            if (uploadedPicture.HasFile)
            {
                Debug.WriteLine("***** FILE FIELD HAS FILE *****");
                string profilePic = uploadedPicture.FileName;
                string[] chopped = profilePic.Split('.');
                string extension = chopped.ElementAt(chopped.Count() - 1);

                // ja zacuvuvam original
                string path = Server.MapPath("~/User/ProfilePictures/" + RegisterUser.UserName + "." + extension);
                uploadedPicture.PostedFile.SaveAs(path);

                // resizing thumbs
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                ImageFormat imgFormat = img.RawFormat;

                List<float> scaledSize = scaleSize(200, 200, img.Width, img.Height);
                Bitmap newImg = new Bitmap(img, (int) scaledSize[0], (int) scaledSize[1]);
                string destPath = Server.MapPath("~/User/ProfilePictures/thumbs/" + RegisterUser.UserName + "." + extension);
                newImg.Save(destPath, imgFormat);

                // resizing standard
                img = System.Drawing.Image.FromFile(path);
                imgFormat = img.RawFormat;

                scaledSize = scaleSize(300, 300, img.Width, img.Height);
                newImg = new Bitmap(img, (int)scaledSize[0], (int)scaledSize[1]);
                destPath = Server.MapPath("~/User/ProfilePictures/standard/" + RegisterUser.UserName + "." + extension);
                newImg.Save(destPath, imgFormat);



                // username-ovite se unique :) po difolt
                storeExtra(RegisterUser.UserName + "." + extension);
            }
            else
            { 
                // se stava default profilePicture.png
                storeExtra("profilePicture.png");
            }
        }
        else
        {
            Debug.WriteLine("step is null");
        }
    }

    protected void RegisterUser_ActiveStepChanged(object sender, EventArgs e)
    {
        // 
    }

    protected void profilePicture_DataBinding(object sender, EventArgs e)
    {
        Debug.WriteLine("UPLOAD FIELD DATA BINDING");
    }
}