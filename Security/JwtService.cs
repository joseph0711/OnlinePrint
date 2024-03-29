﻿using Jose;
using System;
using System.Web.Configuration;
using System.Text;

namespace NCUT系統開發案.Security
{
    public class JwtService
    {
        #region 製作Token
        public string GenerateToken(string Account, string Role)
        {
            JwtObject jwtObject = new JwtObject
            {
                Account = Account,
                Role = Role,
                Expire = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfigurationManager.AppSettings["ExpireMinutes"])).ToString()
            };
            //從Web.Config取得密鑰
            string SecretKey = WebConfigurationManager.AppSettings["SecretKey"].ToString();
            //JWT的內容
            var payload = jwtObject;
            //將資料加密為Token
            var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(SecretKey), JwsAlgorithm.HS512);
            return token;
        }
        #endregion

    }
}