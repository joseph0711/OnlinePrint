using System;
using System.Data.SqlClient;
using OnlinePrint.Models;

namespace OnlinePrint.Service
{
    public class HomepageDBService
    {
        //建立與資料庫的連線字串
        private readonly static string sqlConnstr = "[DB connection string]";

        //建立與資料庫的連線
        private readonly SqlConnection conn = new SqlConnection(sqlConnstr);

        #region 從DB獲取Homepage公布欄html tag資料
        public string GetHomepageContent()
        {
            BulletinBoardModel bulletinBoardModel = new BulletinBoardModel();
            string sql = $@" select * from HomepageContent";

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                bulletinBoardModel.HtmlContent = dr["HtmlContent"].ToString();
            }
            catch (Exception e)
            {
                bulletinBoardModel.HtmlContent = "錯誤!";
            }
            finally
            {
                conn.Close();
            }
            return bulletinBoardModel.HtmlContent;
        }
        #endregion

        #region 將更新公佈欄的html tag資料更新至DB
        public void updateHomepageContent(string updateString)
        {
            string sql = $@" UPDATE HomepageContent SET HtmlContent = '{updateString}' ";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}