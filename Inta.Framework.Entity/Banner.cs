using Inta.Framework.Ado.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inta.Framework.Entity
{
    [Table("Banner")]
    public class Banner
    {
        public Banner()
        {
        }


        [DatabaseColumn(Name = "Id")]
        public int Id { get; set; }
        [DatabaseColumn(Name = "SystemUserId")]
        public int SystemUserId { get; set; }
        [DatabaseColumn(Name = "LanguageId")]
        public int LanguageId { get; set; }
        [DatabaseColumn(Name = "BannerTypeId")]
        public int BannerTypeId { get; set; }
        [DatabaseColumn(Name = "Name")]
        public string Name { get; set; }
        [DatabaseColumn(Name = "Link")]
        public string Link { get; set; }
        [DatabaseColumn(Name = "TargetId")]
        public int TargetId { get; set; }
        [DatabaseColumn(Name = "ShortExplanation")]
        public string ShortExplanation { get; set; }
        [DatabaseColumn(Name = "OrderNumber")]
        public int OrderNumber { get; set; }
        [DatabaseColumn(Name = "Image")]
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }
}
