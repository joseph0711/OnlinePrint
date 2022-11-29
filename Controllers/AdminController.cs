using System.Collections.Generic;
using System.Web.Mvc;
using OnlinePrint.Models;
using OnlinePrint.Service;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Linq;

namespace OnlinePrint.Controllers
{
    public class AdminController : Controller
    {
        public static FormModels temp = new FormModels();
        // GET: Admin
        public ActionResult console()
        {
            ConsoleDBService consoleDBService = new ConsoleDBService();
            List<FormModels> col_FormData = consoleDBService.GetFormData(1);
            ViewBag.formData = col_FormData;

            if (User.Identity.IsAuthenticated && (string)Session["role"] == "Admin")
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

        public ActionResult history()
        {
            ConsoleDBService consoleDBService = new ConsoleDBService();
            List<FormModels> history_FormData = consoleDBService.GetFormData(2);
            ViewBag.formData = history_FormData;

            if (User.Identity.IsAuthenticated && (string)Session["role"] == "Admin")
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

        
        public ActionResult settings()
        {
            if (User.Identity.IsAuthenticated && (string)Session["role"] == "Admin")
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

        
        public ActionResult preview()
        {
            if (User.Identity.IsAuthenticated && (string)Session["role"] == "Admin")
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

        #region 方法
        #region 更新公佈欄資訊
        static string temp_str;
        [HttpPost]
        public ActionResult Settings(BulletinBoardModel bulletinBoardModel)
        {
            temp_str = String.Copy(bulletinBoardModel.updateHtmlContent);
            TempData["temp_Cont"] = bulletinBoardModel.updateHtmlContent;
            return RedirectToAction("preview", "admin");
        }
        #endregion

        #region 預覽公佈欄資訊
        public ActionResult previewSettings()
        {
            HomepageDBService homepageDBService = new HomepageDBService();
            homepageDBService.updateHomepageContent(temp_str);
            return RedirectToAction("homepage", "home");
        }
        #endregion

        #region 狀態更新(處理中 & 已完成)
        public ActionResult update_Status(int SID, string statusEdit, string account)
        {
            FormDBService form_dbService = new FormDBService();
            //MailService mailService = new MailService();

            //透過account資料查詢該使用者的個資
            MembersDBService membersDBService = new MembersDBService();
            Member members = membersDBService.GetDataByAccount(account);

            string status_flag = form_dbService.update_Status(SID, statusEdit);

            
            if (status_flag == "已完成")
            {
                //寄送狀態更新通知信
                //取得寫好的驗證信範本內容
                string TempMail = System.IO.File.ReadAllText(
                    Server.MapPath("~/Views/Shared/EmailComplete_Template.html"));

                //藉由Service將使用者資料填入驗證信範本中
                string MailBody = mailService.GetUserMailBody(SID, TempMail
                    , members.name);
                //呼叫Service寄出驗證信
                mailService.User_SendMail(MailBody, MembersDBService.Data.mail);

                RedirectToAction("console");
            }
           
            return RedirectToAction("console");
        }
        #endregion

        #region 退件理由狀態更新
        [HttpPost]
        public ActionResult rejectMemo_Update(int SID, string statusEdit, string account, FormModels formModels)
        {
            FormDBService form_dbService = new FormDBService();
            //MailService mailService = new MailService();

            //透過account資料查詢該使用者的個資
            MembersDBService membersDBService = new MembersDBService();
            Member members = membersDBService.GetDataByAccount(account);

            string status_flag = form_dbService.update_Status(SID, statusEdit);
            if (status_flag == "退件")
            {
                //寫入拒單理由
                form_dbService.rejectMemo(SID, formModels.apply_Memo);

                //寄送狀態更新通知信
                //取得寫好的驗證信範本內容
                string TempMail = System.IO.File.ReadAllText(
                    Server.MapPath("~/Views/Shared/EmailReject_Template.html"));

                //藉由Service將使用者資料填入驗證信範本中
                string MailBody = mailService.GetUserMailBody(SID, TempMail
                    , members.name);

                //呼叫Service寄出驗證信
                mailService.User_SendMail(MailBody, MembersDBService.Data.mail);
                
                RedirectToAction("console");
            }

            return RedirectToAction("console");
        }
        #endregion

        #region 下載檔案
        public ActionResult download_Files(int SID, string account, string name, string method)
        {
            BlobService blobService = new BlobService();    
            FormDBService formDBService = new FormDBService();

            //傳入SID至SQL查詢符合的filepath & 在SQL讀取符合SID的Filepath值
            List<DownloadModel> downloadData = formDBService.searchFilePath(SID);

            string temp = "";
            for (int i = 0; i < downloadData.Count; i++)
            {
                temp = blobService.DownloadBlob(account, downloadData[i].fileName);
            }

            if (method == "console")
            {
                update_Status(SID, "處理中", account);
            }
            return RedirectPermanent(temp);
        }
        #endregion
        #endregion
    }
}