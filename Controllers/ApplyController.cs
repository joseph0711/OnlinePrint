using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using OnlinePrint.Models;
using OnlinePrint.Service;
using System.Web.Configuration;
using System.IO;
using iTextSharp.text.pdf;
using System;
using Microsoft.Office.Interop.Word;
using System.Threading.Tasks;

namespace OnlinePrint.Controllers
{
    public class ApplyController : Controller
    {
        //宣告寄信用的Service物件
        //private readonly MailService mailService = new MailService();
        #region 申請表頁面
        public ActionResult form()
        {
            BlobService blobService = new BlobService();

            List<SelectListItem> print_Kind;
            List<SelectListItem> print_Side;
            List<SelectListItem> origin_Size;
            List<SelectListItem> print_Size;

            #region 印製種類
            switch ((string)TempData["print_Kind"])
            {
                case "平時考試":
                    print_Kind = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="平時考試", Value="平時考試", Selected = true },
                        new SelectListItem {Text="期中考", Value="期中考" },
                        new SelectListItem {Text="期末考", Value="期末考" },
                    };
                    ViewBag.print_Kind = print_Kind;
                    break; 
                
                case "期中考":
                    print_Kind = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="平時考試", Value="平時考試" },
                        new SelectListItem {Text="期中考", Value="期中考", Selected = true },
                        new SelectListItem {Text="期末考", Value="期末考" },
                    };
                    ViewBag.print_Kind = print_Kind;
                    break;
                
                case "期末考":
                    print_Kind = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="平時考試", Value="平時考試" },
                        new SelectListItem {Text="期中考", Value="期中考" },
                        new SelectListItem {Text="期末考", Value="期末考", Selected = true },
                    };
                    ViewBag.print_Kind = print_Kind;
                    break;

                default:
                    print_Kind = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="請選擇", Value="" },
                        new SelectListItem {Text="平時考試", Value="平時考試" },
                        new SelectListItem {Text="期中考", Value="期中考" },
                        new SelectListItem {Text="期末考", Value="期末考" },
                    };
                    ViewBag.print_Kind = print_Kind;
                    break;
            }
            #endregion

            #region 單雙面
            switch ((string)TempData["print_Side"])
            {
                case "單面":
                    print_Side = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="單面", Value="單面", Selected = true},
                        new SelectListItem {Text="雙面", Value="雙面" },
                    };
                    ViewBag.print_Side = print_Side;
                    break; 
                
                case "雙面":
                    print_Side = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="單面", Value="單面" },
                        new SelectListItem {Text="雙面", Value="雙面", Selected = true},
                    };
                    ViewBag.print_Side = print_Side;
                    break;

                default:
                    print_Side = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="請選擇", Value="" },
                        new SelectListItem {Text="單面", Value="單面" },
                        new SelectListItem {Text="雙面", Value="雙面" },
                    };
                    ViewBag.print_Side = print_Side;
                    break;
            }
            #endregion

            #region 原搞紙張規格
            switch ((string)TempData["origin_Size"])
            {
                case "A4":
                    origin_Size = new List<SelectListItem>()
                    {
                         new SelectListItem {Text="A4", Value="A4", Selected = true },
                        new SelectListItem {Text="B4", Value="B4" },
                        new SelectListItem {Text="A3", Value="A3" },
                    };
                    ViewBag.origin_Size = origin_Size;
                    break;

                case "B4":
                    origin_Size = new List<SelectListItem>()
                    {
                         new SelectListItem {Text="A4", Value="A4" },
                        new SelectListItem {Text="B4", Value="B4", Selected = true },
                        new SelectListItem {Text="A3", Value="A3" },
                    };
                    ViewBag.origin_Size = origin_Size;
                    break;
                
                case "A3":
                    origin_Size = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="A4", Value="A4" },
                        new SelectListItem {Text="B4", Value="B4" },
                        new SelectListItem {Text="A3", Value="A3", Selected = true },
                    };
                    ViewBag.origin_Size = origin_Size;
                    break;
              
                default:
                    origin_Size = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="請選擇", Value=""},
                        new SelectListItem {Text="A4", Value="A4" },
                        new SelectListItem {Text="B4", Value="B4" },
                        new SelectListItem {Text="A3", Value="A3" },
                    };
                    ViewBag.origin_Size = origin_Size;
                    break;
            }
            #endregion

            #region 列印紙張規格
            switch ((string)TempData["print_Size"])
            {
                case "A4":
                    print_Size = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="A4", Value="A4", Selected = true },
                        new SelectListItem {Text="B4", Value="B4" },
                        new SelectListItem {Text="A3", Value="A3" },
                    };
                    ViewBag.print_Size = print_Size;
                    break;

                case "B4":
                    print_Size = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="A4", Value="A4" },
                        new SelectListItem {Text="B4", Value="B4", Selected = true },
                        new SelectListItem {Text="A3", Value="A3" },
                    };
                    ViewBag.print_Size = print_Size;
                    break;

                case "A3":
                    print_Size = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="A4", Value="A4" },
                        new SelectListItem {Text="B4", Value="B4" },
                        new SelectListItem {Text="A3", Value="A3", Selected = true },
                    };
                    ViewBag.print_Size = print_Size;
                    break;

                default:
                    print_Size = new List<SelectListItem>()
                    {
                        new SelectListItem {Text="請選擇", Value="" },
                        new SelectListItem {Text="A4", Value="A4" },
                        new SelectListItem {Text="B4", Value="B4" },
                        new SelectListItem {Text="A3", Value="A3" },
                    };
                    ViewBag.print_Size = print_Size;
                    break;
            }
            #endregion

            ViewBag.username = MembersDBService.member.name;
            ViewBag.loginTime = MembersDBService.member.login_Time;
            ViewBag.department = MembersDBService.member.department;

            if (TempData["fileLength"] == null)
            {
                TempData["fileLength"] = 1;

                for (int i = (int)TempData["fileLength"]; i < (int)TempData["fileLength"]; i++)
                {
                    TempData["alert_MaxPage" + i] = "nothing";
                    TempData["alert_ErrorFormat" + i] = "nothing";
                }
            }

            if (User.Identity.IsAuthenticated && (string)Session["role"] == "User")
            {
                var token = Guid.NewGuid().ToString();
                Session[token] = DateTime.Now.ToString("HHmmss");
                ViewBag.Token = token;
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }
        #endregion

        #region 訂單完成頁面
        public ActionResult ordercomplete(int SID)
        {
            ViewBag.SID = SID;
            if (User.Identity.IsAuthenticated && (string)Session["role"] == "User")
            {
                var token = Guid.NewGuid().ToString();
                Session[token] = DateTime.Now.ToString("HHmmss");
                ViewBag.Token = token;
                return View();
            }
            else
            {
                return RedirectToAction("login", "login");
            }
        }
        #endregion

        #region 申請資料上傳
        [HttpPost]
        public ActionResult Upload(FormModels formModels, HttpPostedFileBase[] file)
        {
            bool uploadSuccess_Flag = false;
            FormDBService formDbService = new FormDBService();
            TempData["fileLength"] = file.Length;

            //判斷是否檔案皆為pdf
            bool isValidFile = true; //若其中一個檔案未為pdf，故無法送出申請表
            for (int k = 0; k < file.Length; k++)
            {
                //fileExtension 取得檔案類型
                string fileExtension = Path.GetExtension(file[k].FileName);
                switch (fileExtension)
                {
                    case ".pdf":
                    {
                        isValidFile = true;
                        break;
                    }
                    default:
                    {
                        isValidFile = false;
                        //alert message
                        TempData["alert_ErrorFormat" + k] = "檔案" + (k + 1) + "：未符合檔案規範！\n";
                        break;
                    }
                }
            }

            //若檔案類型皆為pdf，則上傳申請表資料和檔案。
            if (isValidFile != false)
            {
                formDbService.InsertForm(formModels);

                //establish connection with Storage Account.
                BlobService blobService = new BlobService();

                for (int i = 0; i < file.Length; i++)
                {
                    Stream fileConverted = file[i].InputStream;
                    string fileName = file[i].FileName;
                    string uploadFilePath = blobService.UploadBlob(MembersDBService.member.account, fileName, fileConverted);

                    //新增檔案路徑、檔案名稱以及SID碼到Filepath table中
                    formDbService.InsertFilePath_FilePathDB(uploadFilePath, file[i].FileName, FormDBService.form_DB.Inserted_SID);

                    uploadSuccess_Flag = true;
                }

                //判斷是否皆有上傳flag為true
                //若為true，寄送郵件至使用者信箱
                //信件功能暫時保留
                if (uploadSuccess_Flag == true)
                {
                    //this.sendMail();
                    return RedirectToAction("ordercomplete", "apply", new {SID = FormDBService.form_DB.Inserted_SID });
                }
            }
            else
            {
                //一般欄位
                TempData["subject"] = formModels.Subject;
                TempData["pickUp_Date"] = formModels.PickUp_Date;
                TempData["pickUp_Time"] = formModels.PickUp_Time;
                TempData["phone"] = formModels.phone;
                TempData["print_Num1"] = formModels.Print_Num;
                TempData["ansPaperNum"] = formModels.add_AnsPaperNum;

                //勾選欄位
                switch (formModels.add_Package)
                {
                    case true:
                        TempData["add_Package"] = "yes";
                        break;

                    case false:
                        TempData["add_Package"] = "no";
                        break;
                }
                switch (formModels.add_AnswerPaper)
                {
                    case true:
                        TempData["add_AnswerPaper"] = "yes";
                        break;

                    case false:
                        TempData["add_AnswerPaper"] = "no";
                        break;
                }

                //下拉選單欄位
                //印製種類
                switch (formModels.Print_Kind)
                {
                    case "平時考試":
                        TempData["print_Kind"] = formModels.Print_Kind;
                        break;

                    case "期中考":
                        TempData["print_Kind"] = formModels.Print_Kind;
                        break;

                    case "期末考":
                        TempData["print_Kind"] = formModels.Print_Kind;
                        break;
                }

                //單雙面
                switch (formModels.Print_Side)
                {
                    case "單面":
                        TempData["print_Side"] = formModels.Print_Side;
                        break;
                    case "雙面":
                        TempData["print_Side"] = formModels.Print_Side;
                        break;
                }

                //列印紙張規格
                switch (formModels.Print_Size)
                {
                    case "A4":
                        TempData["print_Size"] = formModels.Print_Size;
                        break;
                    case "B4":
                        TempData["print_Size"] = formModels.Print_Size;
                        break;
                    case "A3":
                        TempData["print_Size"] = formModels.Print_Size;
                        break;
                }

                //text area 
                TempData["memo"] = formModels.Memo;
            }
            return RedirectToAction("form", "apply");
        }
        #endregion

        #region 寄送申請信件給使用者
        public ActionResult sendMail()
        {
            //寄送使用者通知信
            //取得寫好的驗證信範本內容
            string UserMail = System.IO.File.ReadAllText(
                Server.MapPath("~/Views/Shared/EmailNotify_Template.html"));

            //藉由Service將使用者資料填入驗證信範本中
            string User_MailBody = mailService.GetUserMailBody(FormDBService.form_DB.Inserted_SID, UserMail
                , MembersDBService.Data.name);
            //呼叫Service寄出信件
            mailService.User_SendMail(User_MailBody, MembersDBService.Data.mail);

            //寄送行政人員端通知信
            //取得寫好的驗證信範本內容
            string AdminMail = System.IO.File.ReadAllText(
                Server.MapPath("~/Views/Shared/EmailAdmin_Template.html"));

            //藉由Service將使用者資料填入驗證信範本中
            mailService.GetAdminMailBody(AdminMail);
            return RedirectToAction("history", "home");
        }
        #endregion
    }
}