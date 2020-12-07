using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GUM.WebApi.Helpers
{
    public class ImageHelper
    {
        #region SaveImage
        public static string SaveImage(string image)
        {
            //getting blob from bse64 string
            int count = 2;
            string[] separator = { "," };
            string[] base64string = image.Split(separator, count, StringSplitOptions.None); //splitting the base64 string into array
            byte[] blob = Convert.FromBase64String(base64string[1]);

            //extracting file extension
            int extentionStartIndex = base64string[0].IndexOf('/');
            int extensionEndIndex = base64string[0].IndexOf(';');
            string fileExtension = base64string[0].Substring(extentionStartIndex + 1,extensionEndIndex-(extentionStartIndex+1));

            //creating file name 
            var fileName = Guid.NewGuid() +"."+ fileExtension;
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/images"), fileName);
            //saving image 
            File.WriteAllBytes(path, blob);
            return path;
        }
        #endregion

        #region DeleteImage
        public static bool DeleteImage(string imgPath)
        {           
            if (File.Exists(imgPath))
            {
                File.Delete(imgPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}