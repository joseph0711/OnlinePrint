using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using OnlinePrint.Service;
using OnlinePrint.ViewModel;
using OnlinePrint.Security;
using System.IO;
using System.Drawing;
using System.Text;

namespace NCUT系統開發案.Controllers
{
    public class LoginController : Controller
    {
        //宣告Members資料表的Service物件
        private readonly MembersDBService membersDBService = new MembersDBService();

        #region 登入畫面
        //登入一開始載入畫面
        public ActionResult login()
        {
            //判斷使用者是否已經過登入驗證
            //預留使用者已登入redirct時頁面依然要有username

            //已登入則重新導向
            if (User.Identity.IsAuthenticated)
            {
               return RedirectToAction("homepage", "home");
            }
                
            return View();//否則進入登入畫面
        }
        #endregion

        #region 登入功能
        //傳入登入資料的Action
        [HttpPost] 
        public ActionResult login(MembersLoginViewModel membersLoginViewModel)
        {
            //驗證碼
            string code = membersLoginViewModel.validationStr;

            if (!ModelState.IsValid)
            {
                return View(membersLoginViewModel);
            }
            else
            {
                if (code == TempData["code"].ToString())
                {
                    //使用Service裡的方法來驗證登入的帳號密碼
                    string errMsg = membersDBService.LoginCheck(membersLoginViewModel.account, membersLoginViewModel.password);

                    //判斷驗證後結果是否有錯誤訊息
                    if (String.IsNullOrEmpty(errMsg))
                    {
                        //無錯誤訊息，則登入

                        //先藉由Service取得登入者角色資料
                        string RoleData = membersDBService.GetRole(membersLoginViewModel.account);

                        //設定JWT
                        JwtService jwtService = new JwtService();

                        //從Web.Config撈出資料
                        //Cookie名稱
                        string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();
                        string Token = jwtService.GenerateToken(membersLoginViewModel.account, RoleData);

                        ////產生一個Cookie
                        HttpCookie cookie = new HttpCookie(cookieName);

                        //設定單值
                        cookie.Value = Server.UrlEncode(Token);

                        //寫到用戶端
                        Response.Cookies.Add(cookie);

                        //設定Cookie期限
                        //Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfigurationManager.AppSettings["ExpireMinutes"]));

                        //設定session值儲存上次使用者登入的資訊
                        Session["account"] = MembersDBService.member.account;
                        Session["username"] = MembersDBService.member.name;
                        Session["role"] = RoleData;

                        //重新導向頁面
                        if (RoleData == "Admin")
                        {
                            return RedirectToAction("homepage", "home");
                        }
                        else
                        {
                            return RedirectToAction("homepage", "home");
                        }
                    }
                    else
                    {
                        //有驗證錯誤訊息，加入頁面模型中
                        ModelState.AddModelError("errMsg", errMsg);
                        //將資料回填至View中
                        return View(membersLoginViewModel);
                    }
                }
                else
                {
                    TempData["result"] = "驗證錯誤！";
                    return RedirectToAction("Login", "Login");
                }
            }
        }
        #endregion

        #region 驗證碼生成方法
        //隨機碼生成
        private string RandomCode(int length)
        {
            string s = "0123456789";
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            int index;
            for (int i = 0; i < length; i++)
            {
                index = rand.Next(0, s.Length);
                sb.Append(s[index]);
            }
            return sb.ToString();
        }

        //將隨機碼畫在畫板上，並輸出
        public ActionResult GetValidateCode()
        {
            byte[] data = null;
            string code = RandomCode(4);
            TempData["code"] = code;
            //定義一個畫板
            MemoryStream ms = new MemoryStream();
            using (Bitmap map = new Bitmap(100, 40))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.FromArgb(93, 93, 93));
                    g.DrawString(code, new Font("黑體", 20.0F), Brushes.White, new Point(14, 3));
                }
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            data = ms.GetBuffer();
            return File(data, "image/jpeg");
        }
        #endregion

        #region 登出功能
        //登出Action
        [Authorize] //設定此Action須登入
        [HttpPost]
        public ActionResult logout()
        {
            //使用者登出
            //Cookie名稱
            string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();

            //清除Cookie
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Values.Clear();
            Response.Cookies.Set(cookie);

            //清除session
            Session.Clear();
            Session.Abandon();

            //重新導向至登入Action
            return RedirectToAction("login", "login");
        }
        #endregion

        #region 自動登出
        //登出Action
        [Authorize] //設定此Action須登入
        public ActionResult Auto_Logout()
        {
            //使用者登出
            //Cookie名稱
            string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();

            //清除Cookie
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie = Request.Cookies[cookieName];
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Values.Clear();
            Response.Cookies.Set(cookie);

            //重新導向至登入Action
            return RedirectToAction("login", "login");
        }
        #endregion
    }
}