using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnlinePrint.Models;
using System.Configuration;

namespace OnlinePrint.Service
{
    public class FormDBService
    {
        //建立與資料庫的連線字串
        private readonly static string sqlConnstr = "[DB connection string]";

        //建立與資料庫的連線
        private readonly SqlConnection sqlConnection = new SqlConnection(sqlConnstr);

        //建立新建立的Applyform SID全域static 變數
        public static FormModels form_DB = new FormModels();

        #region 新增申請表資料至ApplyForm資料表中
        public void InsertForm(FormModels formData)
        {
            //將無填入的欄位自動填寫為"無"
            if (formData.Memo == null)
            {
                formData.Memo = "無";
            }
            if (formData.add_AnsPaperNum.ToString() == null)
            {
                formData.add_AnsPaperNum = 0;
            }

            DateTime dateTime = DateTime.UtcNow.AddHours(8);
            string applyTime = dateTime.ToString("yyyy/MM/dd HH:mm:ss");

            string sql = $@"INSERT INTO ApplyForm (Apply_Time, Name, Account, department, 
                            Subject, Print_Kind, PickUp_Date, PickUp_Time, 
                            Phone, Print_Num, Print_Side, 
                            Print_Size, Add_Package, Add_AnswerPaper, Add_AnsPaperNum, Memo, status, Apply_Memo) 
                            OUTPUT INSERTED.SID
                            VALUES('{applyTime}', '{MembersDBService.member.name}', '{MembersDBService.member.account}', '{MembersDBService.member.department}',
                                   '{formData.Subject}', '{formData.Print_Kind}', '{formData.PickUp_Date}', '{formData.PickUp_Time}', 
                                   '{formData.phone}', '{formData.Print_Num}', '{formData.Print_Side}', 
                                   '{formData.Print_Size}', '{formData.add_Package}', '{formData.add_AnswerPaper}', '{formData.add_AnsPaperNum}', '{formData.Memo}', '待處裡', '無')";

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                int temp = (int)cmd.ExecuteScalar();
                form_DB.Inserted_SID = temp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        #endregion

        #region 透過SID尋找FilePath資料表中的檔案路徑資料
        public List<DownloadModel> searchFilePath(int SID)
        {
            List<DownloadModel> downloadModels = new List<DownloadModel>();
            SqlCommand sqlCommand = new SqlCommand($@"SELECT * FROM FilePath WHERE SID = '{SID}'")
            {
                Connection = sqlConnection
            };
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DownloadModel downloadData = new DownloadModel
                    {
                        fileName = reader.GetString(reader.GetOrdinal("fileName"))
                    };
                    downloadModels.Add(downloadData);
                }
            }
            else
            {
                Console.WriteLine("查無資料！");
            }
            sqlConnection.Close();
            return downloadModels;
        }
        #endregion

        #region 透過SID取得申請人個資(Mail Service)
        public List<FormModels> searchFormDataBySID(int sid)
        {
            List<FormModels> formModels = new List<FormModels>();
            SqlCommand sqlCommand = new SqlCommand($@"SELECT * FROM ApplyForm WHERE SID = '{sid}'")
            {
                Connection = sqlConnection
            };
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FormModels formData = new FormModels
                    {
                        result_Name = reader.GetString(reader.GetOrdinal("Name")),
                        result_Phone = reader.GetString(reader.GetOrdinal("Phone")),
                        result_ApplyDate = reader.GetDateTime(reader.GetOrdinal("Apply_Time")).ToString("yyyy/MM/dd HH:mm:ss"),
                        result_pickUp_Date = reader.GetDateTime(reader.GetOrdinal("PickUp_Date")).ToString("yyyy/MM/dd"),
                        result_pickUp_Time = reader.GetString(reader.GetOrdinal("PickUp_Time")),
                        result_printKind = reader.GetString(reader.GetOrdinal("Print_Kind")),
                        result_Subject = reader.GetString(reader.GetOrdinal("Subject")),
                        reject_Detail = reader.GetString(reader.GetOrdinal("Apply_Memo"))
                    };
                    formModels.Add(formData);
                }
            }
            else
            {
                Console.WriteLine("查無資料！");
            }
            sqlConnection.Close();
            return formModels;
        }
        #endregion

        #region 透過使用者名字取得申請單資料(使用者端)
        public List<FormModels> GetFormData(string time)
        {
            List<FormModels> formModels = new List<FormModels>();
            SqlCommand sqlCommand = new SqlCommand();

            if (time == "current")
            {
                sqlCommand = new SqlCommand($@"SELECT * FROM ApplyForm WHERE name = '{MembersDBService.member.name}' AND (status = '待處裡' OR status = '處理中')")
                {
                    Connection = sqlConnection
                };
            }
            else if (time == "past")
            {
                sqlCommand = new SqlCommand($@"SELECT * FROM ApplyForm WHERE name = '{MembersDBService.member.name}' AND (status = '已完成' OR status = '退件')")
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
                            Apply_Time = reader.GetDateTime(reader.GetOrdinal("Apply_Time")).ToString("yyyy/MM/dd HH:mm:ss"),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            phone = reader.GetString(reader.GetOrdinal("Phone")),

                            //申請欄位
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Print_Kind = reader.GetString(reader.GetOrdinal("Print_Kind")),
                            Print_Num = reader.GetByte(reader.GetOrdinal("Print_Num")),
                            Print_Size = reader.GetString(reader.GetOrdinal("Print_Size")),
                            Print_Side = reader.GetString(reader.GetOrdinal("Print_Side")),
                            PickUp_Date = reader.GetDateTime(reader.GetOrdinal("PickUp_Date")).ToString("yyyy/MM/dd"),
                            PickUp_Time = reader.GetString(reader.GetOrdinal("PickUp_Time")),
                            Memo = reader.GetString(reader.GetOrdinal("Memo")),

                            //附加需求
                            add_Package = reader.GetBoolean(reader.GetOrdinal("Add_Package")),
                            add_AnswerPaper = reader.GetBoolean(reader.GetOrdinal("Add_AnswerPaper")),
                            add_AnsPaperNum = reader.GetByte(reader.GetOrdinal("Add_AnsPaperNum")),

                            //申請狀態
                            status = reader.GetString(reader.GetOrdinal("status")),
                            apply_Memo = reader.GetString(reader.GetOrdinal("Apply_Memo"))
                        };

                        //將值更改為使用者易懂之意思
                        //牛皮紙袋
                        if (formData.add_Package == true)
                        {
                            formData.dis_add_Package = "需要";
                        }
                        else if (formData.add_Package == false)
                        {
                            formData.dis_add_Package = "不需要";
                        }

                        //考試答案卷
                        if (formData.add_AnswerPaper == true)
                        {
                            formData.dis_add_AnswerPaper = "需要";
                        }
                        else if (formData.add_AnswerPaper == false)
                        {
                            formData.dis_add_AnswerPaper = "不需要";
                        }

                        if (formData.apply_Memo == "")
                        {
                            formData.apply_Memo = "無";
                        }

                        formModels.Add(formData);
                    }
                }
                else
                {
                    Console.WriteLine("查無資料！");
                }
                sqlConnection.Close();
            }
            return formModels;
        }
        #endregion

        #region 狀態更新
        public string update_Status(int SID, string statusEdit)
        {
            string sql = $@" UPDATE ApplyForm SET status = '{statusEdit}' WHERE SID = {SID} ";
            
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
            return statusEdit;
        }
        #endregion

        #region 拒單理由
        public void rejectMemo(int SID, string statusEdit)
        {
            string sql = $@" UPDATE ApplyForm SET Apply_Memo = '{statusEdit}' WHERE SID = {SID} ";
            
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        #endregion

        #region 新增檔案路徑 & 檔案名稱至FilePath資料表中
        public void InsertFilePath_FilePathDB(string filePath, string fileName, int SID)
        {
            string sql = $@" INSERT FilePath (SID, fileName) VALUES ('{SID}', '{fileName}')";

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        #endregion
    }
}
