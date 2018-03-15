using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SizeScaleService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SizeScaleService : System.Web.Services.WebService
{

    public SizeScaleService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description ="This service is used to scale images that are uploaded for recipes")]
    public List<float> scaleSize(int maxW, int maxH, float currW, float currH)
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

}
