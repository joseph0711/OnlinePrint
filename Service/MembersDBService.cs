using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using OnlinePrint.Models;

namespace OnlinePrint.Service
{
    public class MembersDBService
    {
        public static Member member = new Member();

        //建立與資料庫的連線字串
        private readonly static string sqlConnstr = "[DB connection string]";

        //建立與資料庫的連線
        private readonly SqlConnection conn = new SqlConnection(sqlConnstr);

        #region Hash密碼
        //Hash密碼用的方法
        public string HashPassword(string Password)
        {
            //宣告Hash時所添加的無意義亂數值
            string saltkey = "1q2w3e4r5t6y7u8ui9o0po7tyy";
            //將剛剛宣告的字串與密碼結合
            string saltAndPassword = String.Concat(Password, saltkey);
            //定義SHA256的HASH物件
            SHA256CryptoServiceProvider sha256Hasher = new SHA256CryptoServiceProvider();
            //取得密碼轉換成byte資料
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            //取得Hash後byte資料
            byte[] HashDate = sha256Hasher.ComputeHash(PasswordData);
            //將Hash後byte資料轉換成string
            string Hashresult = Convert.ToBase64String(HashDate);
            //回傳Hash後結果
            return Hashresult;
        }
        #endregion

        #region 查詢登入使用者資料
        //藉由帳號取得單筆資料的方法
        public Member GetDataByAccount(string Account)
        {
            //Sql語法
            string sql = $@" select * from Member where account = '{Account}' ";
            //確保程式不會因執行錯誤而整個中斷
            try
            {
                //開啟資料庫連線
                conn.Open();

                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);

                //取得Sql資料
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                member.account = dr["account"].ToString();
                member.password = dr["password"].ToString();
                member.isAdmin = Convert.ToBoolean(dr["isAdmin"]);
                member.name = dr["name"].ToString();
                member.department = dr["department"].ToString();
                member.login_Time = dr["login_Time"].ToString();
                member.mail = dr["mail"].ToString();
            }   
            catch (Exception e)
            {
                //查無資料
                member = null;
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
            //回傳根據編號所取得的資料
            return member;
        }
        #endregion

        #region 登入確認
        //登入帳密確認方法，並回傳驗證後訊息
        public string LoginCheck(string Account, string Password)
        {
            //取得傳入帳號的會員資料
            Member loginMember = GetDataByAccount(Account);
            //判斷是否有此會員
            if (loginMember != null)
            {
                //進行帳號密碼確認
                if (PasswordCheck(loginMember, Password))
                {
                    //insert login time into sql db.
                    DateTime dateTime = DateTime.UtcNow.AddHours(8);
                    string temp = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
                    LoginTimeInsert(temp, Account);
                    return "";
                }
                else
                {
                    return "密碼輸入錯誤！";
                }
            }
            else
            {
                return "無此帳號！";
            }
        }
        #endregion

        #region 密碼確認
        //進行密碼確認方法
        public bool PasswordCheck(Member CheckMember, string Password)
        {
            //判斷資料庫裡的密碼資料與傳入密碼資料Hash後是否一樣
            bool result = CheckMember.password.Equals(HashPassword(Password));
            //回傳結果
            return result;
        }
        #endregion

        #region 更新登入時間
        public void LoginTimeInsert(string datetimeData, string account)
        {
            //Sql語法
            string sql = $@" UPDATE Member SET login_Time = '{datetimeData}' WHERE account = '{account}' ";
            //確保程式不會因執行錯誤而整個中斷
            try
            {
                //開啟資料庫連線
                conn.Open();

                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //throw the exception info.
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                //close the connection with sql db
                conn.Close();
            }
        }
        #endregion

        #region 取得角色
        //取得會員的權限角色資料
        public string GetRole(string Account)
        {
            //宣告初始角色字串
            string Role = "User";
            //取得傳入帳號的會員資料
            Member LoginMember = GetDataByAccount(Account);
            //判斷資料庫欄位，用以確認是否為Admon
            if (LoginMember.isAdmin)
            {
                Role = "Admin"; //添加Admin
            }
            //回傳最後結果
            return Role;
        }
        #endregion
    }
}