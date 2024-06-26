using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Inta.Framework.Extension
{
    public class FileManager
    {
        public static string FileUpload(HttpPostedFileBase File, string filePath)
        {
            string FileName = "";
            string extension = System.IO.Path.GetExtension(File.FileName.ToLower());
            string imageFilePath = filePath;

            string random = File.FileName.Replace(extension, "") + "_" + Guid.NewGuid().ToString();
            random = StringManager.TextUrlCharReplace(random);

            if (File.ContentLength > 0 && (extension == ".xls" | extension == ".xlsx" | extension == ".pdf" | extension == ".doc" | extension == ".docx") && (File.FileName.ToLower().IndexOf(";") == -1))
            {
                File.SaveAs(imageFilePath + random + extension);

                FileName = random.ToString() + extension;
            }

            return FileName;
        }



    }
}
