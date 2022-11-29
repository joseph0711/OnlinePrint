namespace OnlinePrint.Models
{
    public class FormModels
    {
        //***申請表單***
        //基本資料 //history 將會從此get
        public string Apply_Time { get; set; }
        public int SID { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string department { get; set; }
        public string account { get; set; }

        //申請欄位
        public string Subject { get; set; }
        public string Print_Kind { get; set; }
        public string PickUp_Date { get; set; }
        public string PickUp_Time { get; set; }

        //列印客製化
        public int Print_Num { get; set; }
        public string Print_Side { get; set; }
        //public string origin_Size { get; set; }
        public string Print_Size { get; set; }
        public bool add_Package { get; set; }
        public bool add_AnswerPaper { get; set; }
        public int add_AnsPaperNum { get; set; }
        public string Memo { get; set; }

        //申請狀態
        public string status { get; set; } //申請狀態
        public string apply_Memo { get; set; } //管理者申請駁回意見欄

        //不同資料型別顯示之暫時變數(bool -> string or ...)
        public string dis_add_Package { get; set; }
        public string dis_add_AnswerPaper { get; set; }
        
        //剛新增進DB表單的SID
        public int Inserted_SID { get; set; }

        //置放透過SID尋找的欄位變數
        public string result_Status { get; set; }
        public string result_ApplyDate { get; set; }
        public string result_Name { get; set; }
        public string result_Phone { get; set; }
        public string result_pickUp_Date { get; set; }
        public string result_pickUp_Time { get; set; }
        public string result_printKind { get; set; }
        public string result_Subject { get; set; }
        public string reject_Detail { get; set; }

    }
}