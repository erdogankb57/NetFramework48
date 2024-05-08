using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inta.Framework.Web.Base
{
    public static class PagingDataList
    {

        public static MvcHtmlString PagingDataListFor(
        this HtmlHelper content,
        string ObjectId = null,
        List<PagingDataListColumn> Header = null,
        string Url = null,
        string AddUrl = "",
        bool isAddPopup = true,
        string DeleteUrl = "",
        string ActiveUrl = "",
        string PassiveUrl = "",
        string SaveUrl = "",
        string CallBackFunction = "",
        int PagingRowCount = 0,
        string OrderColumn = null,
        string OrderType = "asc"
        )
        {
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<form id='saveForm' method='post' novalidate>");
            shtml.Append("    <div class='modal fade' id='addRecordModal' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'>");
            shtml.Append("    </div>");
            shtml.Append("</form>");

            shtml.Append("<div class='PagingDataTable'>");
            shtml.Append("<div class='col-lg-12'>");
            shtml.Append("    <div class='form-group'>");
            shtml.Append($"        <button type = 'button' class='btn btn-standart' onclick=\"$PagingDataList.AddRecordModal('{AddUrl}','{isAddPopup}',0)\">");
            shtml.Append("            Kayıt Ekle");
            shtml.Append("        </button>");
            shtml.Append($"        <button type='button' id='deleteAllRecord' onclick=\"$PagingDataList.DeleteRecordModal('{ObjectId}','{DeleteUrl}',{CallBackFunction},0)\" class='deleteAllRecord btn btn-standart' data-toggle='modal'>");
            shtml.Append("            Seçilen kayıtları sil");
            shtml.Append("        </button>");
            shtml.Append($"        <button type='button' id='activeAllRecord' onclick=\"$PagingDataList.ActiveRecordModal('{ObjectId}','{ActiveUrl}',{CallBackFunction},0)\" class='deleteAllRecord btn btn-standart' data-toggle='modal'>");
            shtml.Append("            Seçilen kayıtları aktif yap");
            shtml.Append("        </button>");
            shtml.Append($"        <button type='button' id='passiveAllRecord' onclick=\"$PagingDataList.PassiveRecordModal('{ObjectId}','{PassiveUrl}',{CallBackFunction},0)\" class='deleteAllRecord btn btn-standart' data-toggle='modal'>");
            shtml.Append("            Seçilen kayıtları pasif yap");
            shtml.Append("        </button>");
            shtml.Append("    </div>");
            shtml.Append("</div>");

            shtml.Append("<div class='PagingDataList col-lg-12'>");
            shtml.Append("<table id='" + ObjectId + "' Url='" + Url + "' AddUrl='" + AddUrl + "' DeleteUrl='" + DeleteUrl + "' PagingRowCount='" + PagingRowCount + "' OrderColumnn='" + OrderColumn + "' SaveUrl='" + SaveUrl + "' OrderType='" + OrderType + "'>");
            shtml.Append("<thead>");
            shtml.Append("<tr>");




            if (Header != null)
            {
                foreach (var item in Header)
                {
                    shtml.Append(String.Format("<th Name=\"{0}\" Text='{1}' Format=\"{2}\" Width=\"{3}\" Short=\"{4}\">{5}</th>", item.Name, item.Text, item.Format, item.Width, item.Short, item.Text));
                }
            }
            shtml.Append("</tr>");
            shtml.Append("</thead>");
            shtml.Append("<tbody>");
            shtml.Append("</tbody>");
            shtml.Append("</table>");
            shtml.Append("<div class='PagingRow'></div>");
            shtml.Append("</div>");
            shtml.Append("</div>");

            return new MvcHtmlString(shtml.ToString());
        }
    }
    public class PagingDataListRequest<T>
    {
        public T Search { get; set; }
        public int PageRowCount { get; set; }
        public string OrderColumn { get; set; }
        public PagingDataListOrderType OrderType { get; set; }
        public int ActivePageNumber { get; set; } = 1;
    }

    public enum PagingDataListOrderType
    {
        Ascending = 1,
        Descending = 2
    }
    public class PagingDataListColumn
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Format { get; set; }
        public int Width { get; set; }
        public bool Short { get; set; }
    }

}
