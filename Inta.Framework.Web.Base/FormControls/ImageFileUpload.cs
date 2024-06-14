﻿using Inta.Framework.Ado.Net;
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
        bool isBigImage = true,
        int smallImageCropWidth = 0,
        int smallImageCropHeight = 0,
        int bigImageCropWidth = 0,
        int bigImageCropHeight = 0,
        string SaveUrl = null
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


            shtml.Append($@"
                           <div class='image' style='{(string.IsNullOrEmpty(Image) ? "display:none" : "display:block")}'>
                                    <div class='small-image-content'>
                                        <a id='ImageLink' href='{ConfigurationManager.AppSettings["ImageUpload"] + (isBigImage ? "b_" : "k_") + Image}' data-fancybox data-caption=''><img class='ImagePreview' src='{ConfigurationManager.AppSettings["ImageUpload"] + "k_" + Image}' width='100' height='100' style='border:solid 1px #000000' /></a>
                                    </div>
                                    <div class='image-delete'>
                                        <button type='button' class='btn btn-standart' onclick=""$ImageFileUpload.Delete({Id},'{ObjectId}','{ListObjectId}')"">Resmi sil</button>
                                    </div>
                                    ");

            if (!string.IsNullOrEmpty(Image))
            {
                shtml.Append($@"
                                    <div class='image-delete'>
                                        <a class='btn btn-standart' href='/ImageCrop/Index?ImageName={Image}&Dimension=k_&width={smallImageCropWidth}&height={smallImageCropHeight}&SaveUrl={SaveUrl}'>Küçük resmi cropla</a>
                                    </div>");

                shtml.Append($@"
                                    <div class='image-delete'>
                                        <a class='btn btn-standart' href='/ImageCrop/Index?ImageName={Image}&Dimension=b_&width={bigImageCropWidth}&height={bigImageCropHeight}&SaveUrl={SaveUrl}'>Büyük resmi cropla</a>
                                    </div>");
            }

            shtml.Append($@"        </div>");

            shtml.Append("  </div>");

            return new MvcHtmlString(shtml.ToString());
        }

    }
}
