using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Web.Base.FormControls
{
    public static class ImageFileUpload
    {
        public static MvcHtmlString ImageFileUploadFor(
        this HtmlHelper content,
        int Id,
        string ObjectId = null,
        string ObjectName = null,
        string Image = null,
        string DeleteUrl = null,
        string ListObjectId = null,
        bool isBigImage = true
        )
        {
            if (ObjectId is null)
            {
                return new MvcHtmlString("");
            }

            if (string.IsNullOrEmpty(ObjectId))
                return new MvcHtmlString("");

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            StringBuilder shtml = new StringBuilder();
            shtml.Append($@"<div class='imagePanel'>
                                <div class='fileUpload'>
                                    <input type='file' id='{ObjectId}' name='{ObjectName}' DeleteUrl='{DeleteUrl}' class='form-control' />
                                </div>");

            if (!string.IsNullOrEmpty(Image))
            {
                shtml.Append($@"
                           <div class='image'>
                                    <div class='small-image-content'>
                                        <a id='ImageLink' href='{ConfigurationManager.AppSettings["ImageUpload"] + (isBigImage ? "b_" : "k_") + Image}' data-fancybox data-caption=''><img id='Image' src='{ConfigurationManager.AppSettings["ImageUpload"] + "k_" + Image}' width='100' height='100' style='border:solid 1px #000000' /></a>
                                    </div>
                                    <div class='image-delete'>
                                        <button type='button' class='btn btn-standart' onclick=""$ImageFileUpload.Delete({Id},'{ObjectId}','{ListObjectId}')"">Resmi sil</button>
                                    </div>
                                </div>");
            }
            shtml.Append("  </div>");

            return new MvcHtmlString(shtml.ToString());
        }

    }
}
