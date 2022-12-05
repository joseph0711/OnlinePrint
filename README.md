# 線上列印系統
作品Demo連結：https://onlineprint.azurewebsites.net/

## 作品簡述

線上列印系統針對校內教師在期中考、期末考及小考，透過本系統進行無紙化線上申請，並可透過表單來符合教師在考題上列印上的各式需求。行政人員亦可透過本系統來進行管理需求單，
不僅比紙本申請更加效率，也更加容易控管。

## 技術
1. .NET framework 4.8 (MVC架構)
2. Azure MS SQL：用以儲存學生資訊和申請單資訊
3. Azure Blob：儲存各申請單之考卷檔案
4. Azure App Services

除了上述的技術框架外，也使用到Bootstrap模板、FontAwesome圖示以及JQuery函式庫。

## 畫面展示說明
<div>
    <h3>登入身份</h3>
    <p>分成一般身份及管理身份，配合Demo模式因此將登入匡列更改為點選按鈕，透過JS自動帶入使用者帳密</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/LoginPage.png?sp=r&st=2022-12-05T07:55:24Z&se=2023-12-05T15:55:24Z&spr=https&sv=2021-06-08&sr=b&sig=s%2BIxSUnhVcCYfc15Rs7inZm5jKPXPGZw1a1PdQGBGnI%3D">
</div>

<div>
    <h3>一般身份-申請單</h3>
    <p>使用者可進入申請單頁面進行試卷列印申請。</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E7%94%B3%E8%AB%8B%E5%96%AE-1.png?sp=r&st=2022-09-18T08:17:53Z&se=2023-09-17T16:17:53Z&spr=https&sv=2021-06-08&sr=b&sig=CirZmZmndtDMQysVXYuiSOQMouz9L7Rk31m2qcGBekc%3D">
</div>

<div>
    <h3>一般身份-申請紀錄</h3>
    <p>使用者可進入申請紀錄頁面觀看<b>處理中</b>表單之狀態及詳細內容。</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E4%BD%BF%E7%94%A8%E8%80%85%E7%AB%AF-%E7%94%B3%E8%AB%8B%E7%B4%80%E9%8C%84.png?sp=r&st=2022-09-18T08:20:27Z&se=2023-09-17T16:20:27Z&spr=https&sv=2021-06-08&sr=b&sig=bfRMMKxl1IFQoK1hseHXr9uW4VarH9GT%2FZLHMVbXQis%3D">
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E4%BD%BF%E7%94%A8%E8%80%85%E7%AB%AF-%E7%94%B3%E8%AB%8B%E8%A9%B3%E7%B4%B0%E7%B4%80%E9%8C%84.png?sp=r&st=2022-09-18T08:21:22Z&se=2023-09-17T16:21:22Z&spr=https&sv=2021-06-08&sr=b&sig=egeIwmPXuG1V1oswrCN8a1hEsBFnaY0Xg18iBjujbQU%3D">
</div>

<div>
    <h3>一般身份-歷史紀錄</h3>
    <p>使用者可進入申請紀錄頁面觀看<b>已完成或退件</b>表單之狀態及詳細內容。</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E4%BD%BF%E7%94%A8%E8%80%85%E7%AB%AF-%E6%AD%B7%E5%8F%B2%E7%B4%80%E9%8C%84.png?sp=r&st=2022-09-18T08:22:08Z&se=2023-09-17T16:22:08Z&spr=https&sv=2021-06-08&sr=b&sig=Ocdrc%2Bad4z2zOfCdJcDZSERDcSfp62irVB%2F1%2F3vwttU%3D">
</div>

<div>
    <h3>管理身分-申請單管理</h3>
    <p>管理者可透過頁面來管理表單，並可對表單進行檔案下載、結單以及退回</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E7%AE%A1%E7%90%86%E7%AB%AF-%E7%94%B3%E8%AB%8B%E5%96%AE%E7%AE%A1%E7%90%862.png?sp=r&st=2022-12-05T08:44:52Z&se=2023-12-05T16:44:52Z&spr=https&sv=2021-06-08&sr=b&sig=4QzQ0U9DbNY4b0TfGkBdZUEubz8KYKAmr87quMAqIf8%3D">
</div>

<div>
    <h3>管理身分-歷史紀錄</h3>
    <p>管理者可透過歷史紀錄頁面，來觀看所有<b>已結單或退回</b>之申請單</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E7%AE%A1%E7%90%86%E7%AB%AF-%E6%AD%B7%E5%8F%B2%E7%B4%80%E9%8C%84.png?sp=r&st=2022-09-18T08:23:09Z&se=2023-09-17T16:23:09Z&spr=https&sv=2021-06-08&sr=b&sig=vpDaQPc51v%2BGLBi%2BsSpA7aQRo%2Bq3KuPyBreBAC4Ghq4%3D">
</div>

<div>
    <h3>管理身分-公佈欄設定</h3>
    <p>公布欄可依任何公告需求來調整內容，僅需要點選「公佈欄設定」即可進行更改，並可添加任何文字、媒體內容、修改文字格式……等。故此系統為配合Demo作品，因此暫不開放修改權限。</p>
    <img
        src="https://onlineprint.blob.core.windows.net/joseph/%E7%AE%A1%E7%90%86%E5%96%AE-%E5%85%AC%E4%BD%88%E6%AC%84%E8%A8%AD%E5%AE%9A.png?sp=r&st=2022-12-05T08:43:22Z&se=2023-12-05T16:43:22Z&spr=https&sv=2021-06-08&sr=b&sig=GgrKmTsW6hgHx0MsrXe9fCbvoe1VIpdLiOxBDrA82Z4%3D">
</div>


## License

This project is licensed under the MIT License - see the LICENSE.md file for details
