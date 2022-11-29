using System;
using System.Collections.Generic;
using OnlinePrint.Models;
using System.Data.SqlClient;

namespace OnlinePrint.Service
{
    public class ConsoleDBService
    {
        //建立與資料庫的連線字串
        private readonly static string sqlConnstr = "[DB connection string]";

        //建立與資料庫的連線
        private readonly SqlConnection sqlConnection = new SqlConnection(sqlConnstr);

        #region 透過SID取得申請單資料(管理者端)
        public List<FormModels> GetFormData(int id)
        {
            List<FormModels> formModels = new List<FormModels>();
            SqlCommand sqlCommand = new SqlCommand();

            //1為待處裡 & 處理中狀態資料；2為已完成狀態資料
            if (id == 1)
            {
                sqlCommand = new SqlCommand($@" SELECT * FROM ApplyForm WHERE status = '待處裡' OR　status = '處理中'")
                {
                    Connection = sqlConnection
                };
            }
            else
            {
                sqlCommand = new SqlCommand($@" SELECT * FROM ApplyForm WHERE status = '已完成' OR　status = '退件'")
                {
                    Connection = sqlConnection
                };
            }

            try
            {
                sqlConnection.Open();
            }
            catch (Exception e)
            {

            }
            finally //確保與SQL請求資料為ok
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FormModels formData = new FormModels
                        {
                            //基本資料
                            SID = reader.GetInt32(reader.GetOrdinal("SID")),
                            account = reader.GetString(reader.GetOrdinal("Account")),
                            department = reader.GetString(reader.GetOrdinal("department")),
                            Apply_Time = reader.GetDateTime(reader.GetOrdinal("Apply_Time")).ToString("yyyy/MM/dd HH:mm:ss"),
                            Name = reader.GetString(reader.GetOrdinal("Name")),

                            //申請欄位
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Print_Kind = reader.GetString(reader.GetOrdinal("Print_Kind")),
                            Print_Num = reader.GetByte(reader.GetOrdinal("Print_Num")),
                            Print_Size = reader.GetString(reader.GetOrdinal("Print_Size")),
                            Print_Side = reader.GetString(reader.GetOrdinal("Print_Side")),
                            Memo = reader.GetString(reader.GetOrdinal("Memo")),

                            //附加需求
                            add_Package = reader.GetBoolean(reader.GetOrdinal("Add_Package")),
                            add_AnswerPaper = reader.GetBoolean(reader.GetOrdinal("Add_AnswerPaper")),
                            add_AnsPaperNum = reader.GetByte(reader.GetOrdinal("Add_AnsPaperNum")),

                            //領取需求
                            PickUp_Date = reader.GetDateTime(reader.GetOrdinal("PickUp_Date")).ToString("yyyy/MM/dd"),
                            PickUp_Time = reader.GetString(reader.GetOrdinal("PickUp_Time")),
                            phone = reader.GetString(reader.GetOrdinal("Phone")),

                            //申請狀態
                            status = reader.GetString(reader.GetOrdinal("status"))
                        };

                        //將值更改為使用者易懂之意思
                        if (formData.add_Package == true)
                        {
                            formData.dis_add_Package = "需要";
                        }
                        else if (formData.add_Package == false)
                        {
                            formData.dis_add_Package = "不需要";
                        }

                        if (formData.add_AnswerPaper == true)
                        {
                            formData.dis_add_AnswerPaper = "需要";
                        }
                        else if (formData.add_AnswerPaper == false)
                        {
                            formData.dis_add_AnswerPaper = "不需要";
                        }

                        formModels.Add(formData);
                    }
                }
                else
                {
                    Console.WriteLine("資料庫為空！");
                }
                sqlConnection.Close();
            }
            return formModels;
        }
        #endregion
    }
}