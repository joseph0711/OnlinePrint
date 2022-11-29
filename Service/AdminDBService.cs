using System.Collections.Generic;
using NCUT系統開發案.Models;
using System.Data.SqlClient;
using System;

namespace OnlinePrint.Service
{
    public class AdminDBService
    {
        //建立與資料庫的連線字串
        private readonly static string sqlConnstr = "[DB connection string]";

        //建立與資料庫的連線
        SqlConnection sqlConnection = new SqlConnection(sqlConnstr);

        public List<AdminModel> GetAdminInfo()
        {
            List<AdminModel> adminModels = new List<AdminModel>();
            
            SqlCommand sqlCommand = new SqlCommand($@"SELECT * FROM Member WHERE isAdmin = '1'")
            {
                Connection = sqlConnection
            };
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AdminModel adminData = new AdminModel
                    {
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Mail = reader.GetString(reader.GetOrdinal("mail"))
                    };
                    adminModels.Add(adminData);
                }
            }
            else
            {
                Console.WriteLine("查無資料！");
            }
            sqlConnection.Close();
            return adminModels;
        }
    }
}