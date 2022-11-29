using System.Net.Mail;
using System.Collections.Generic;
using OnlinePrint.Models;

namespace OnlinePrint.Service
{
    public class MailService
    {
        private string gmail_account = "joseph900711@gmail.com"; //Gmail帳號
        private string gmail_password = "JosephChiang@0711"; //Gmail密碼
        private string gmail_mail = "joseph900711@gmail.com"; //Gmail信箱

        //將使用者資料填入驗證信範本中
        public string GetUserMailBody(int SID, string tempString, string UserName)
        {
            FormDBService formDBService = new FormDBService();
            List<FormModels> formData = formDBService.searchFormDataBySID(SID);

            //將使用者資料填入
            tempString = tempString.Replace("{{username}}", UserName);
            tempString = tempString.Replace("{{subject}}", formData[0].result_Subject);
            tempString = tempString.Replace("{{pickUp_Date}}", formData[0].result_pickUp_Date);
            tempString = tempString.Replace("{{pickUp_Time}}", formData[0].result_pickUp_Time);
            tempString = tempString.Replace("{{reject_Detail}}", formData[0].reject_Detail);

            //回傳最後結果
            return tempString;
        }

        public void GetAdminMailBody(string TempString)
        {
            AdminDBService adminDBService = new AdminDBService();
            FormDBService formDBService = new FormDBService();
            List<AdminModel> adminData = adminDBService.GetAdminInfo();
            List<FormModels> formData = formDBService.searchFormDataBySID(FormDBService.form_DB.Inserted_SID);

            //宣告寄信用的Service物件
            //MailService mailService = new MailService();

            for (int i = 0; i < adminData.Count; i++)
            {
                TempString = TempString.Replace("{{adminName}}", adminData[i].Name);
                TempString = TempString.Replace("{{name}}", formData[0].result_Name);
                TempString = TempString.Replace("{{phone}}", formData[0].result_Phone);
                TempString = TempString.Replace("{{SID}}", FormDBService.form_DB.Inserted_SID.ToString());
                TempString = TempString.Replace("{{applyDate}}", formData[0].result_ApplyDate);
                TempString = TempString.Replace("{{print_Kind}}", formData[0].result_printKind);
                TempString = TempString.Replace("{{pickUp_Date}}", formData[0].result_pickUp_Date);
                TempString = TempString.Replace("{{pickUp_Time}}", formData[0].result_pickUp_Time);

                //呼叫Service寄出信件
                //mailService.Admin_SendMail(TempString, adminData[i].Mail);
            }
        }

        //寄驗證信的方法
        public void User_SendMail(string mailBody, string toEmail)
        {
            //建立寄信用Smtp物件，這裡使用Gmail為例
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //設定使用的Port，這裡設定Gmail所使用的
            SmtpServer.Port = 587;
            //建立使用者憑據，這裡要設定您的Gmail帳戶
            SmtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            //開啟SSL
            SmtpServer.EnableSsl = true;
            //宣告信件內容物件
            MailMessage mail = new MailMessage();
            //設定來源信箱
            mail.From = new MailAddress(gmail_mail);
            //設定收信者信箱
            mail.To.Add(toEmail);
            //設定信件主旨
            mail.Subject = "申請狀態通知信";
            //設定信件內容
            mail.Body = mailBody;
            //設定信件內容為HTML格式
            mail.IsBodyHtml = true;
            //送出信件
            SmtpServer.Send(mail);
        }

        public void Admin_SendMail(string mailBody, string toEmail)
        {
            //建立寄信用Smtp物件，這裡使用Gmail為例
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //設定使用的Port，這裡設定Gmail所使用的
            SmtpServer.Port = 587;
            //建立使用者憑據，這裡要設定您的Gmail帳戶
            SmtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            //開啟SSL
            SmtpServer.EnableSsl = true;
            //宣告信件內容物件
            MailMessage mail = new MailMessage();
            //設定來源信箱
            mail.From = new MailAddress(gmail_mail);
            //設定收信者信箱
            mail.To.Add(toEmail);
            //設定信件主旨
            mail.Subject = "新增申請需求單";
            //設定信件內容
            mail.Body = mailBody;
            //設定信件內容為HTML格式
            mail.IsBodyHtml = true;
            //送出信件
            SmtpServer.Send(mail);
        }
    }
}