using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlinePrint.Models
{
    public class BulletinBoardModel
    {
        [AllowHtml]
        public string updateHtmlContent { get; set; }

        public string HtmlContent { get; set; }
    }
}