using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication.Services
{
    public class MembersDBService
    {
        //建立與資料庫的連線
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public string HashPassWord(string Password)
        {
            //宣告Hash時所添加的無異議亂數值
            string saltKey = "lkmflks516f15dsfhfes59d2";
            //將剛剛宣告的字串與密碼結合
            string saltAndPassword = String.Concat(Password, saltKey);
            //定義SHA256的HASH物件
            SHA256CryptoServiceProvider sha256Hasher = new SHA256CryptoServiceProvider();
            //取得密碼後轉換成bytes資料
            byte[] passwordData = Encoding.Default.GetBytes(saltAndPassword);
            //取得Hash後byte資料
            byte[] hashData = sha256Hasher.ComputeHash(passwordData);
            //將Hash後byte資料轉換成String
            string hashResult = Convert.ToBase64String(hashData);

            return hashResult;
        }
        private Members getDataByAccount(string companyId)
        {
            Members Data = new Members();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Company WHERE companyId = @companyId"); // 你的@呢?
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@companyId", companyId));
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            try
            {

                Data.companyName = reader["companyName"].ToString();
                Data.companyId = reader["companyId"].ToString();
                Data.password = reader["password"].ToString();
                Data.companyLeader = reader["companyLeader"].ToString();
                Data.companyAddress = reader["companyAddress"].ToString();
                Data.fax = reader["fax"].ToString();
                Data.telephone = reader["telephone"].ToString();
                Data.email1 = reader["email1"].ToString();
                Data.email2 = reader["email2"].ToString();
                Data.email3 = reader["email3"].ToString();
                Data.email4 = reader["email4"].ToString();
                Data.email5 = reader["email5"].ToString();
            }
            catch
            {
                //查無資料
                Data = null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return Data;
        }
        public string LoginCheck(string companyId, string Password)
        {
            //取得傳入帳號的會員資料
            Members LogingMember = getDataByAccount(companyId);
            //判斷是否有此會員
            if (LogingMember != null)
            {
                //進行帳號密碼確認
                if (PasswordCheck(LogingMember, Password))
                {
                    return "";
                }
                else
                {
                    return "密碼輸入錯誤";
                }
            }
            else
            {
                return "無此會員帳號，請去註冊";
            }
        }
        public bool PasswordCheck(Members CheckMember, string Password)
        {
            //判斷資料庫裡的密碼資料與傳入密碼資料Hash後是否一樣
            bool result = CheckMember.password.Equals(HashPassWord(Password));
            //回傳結果
            return result;
        }
        public bool Register(Members newMember)
        {
            bool accountExist = false;
            SqlConnection conn = new SqlConnection(connStr);
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = connStr;
            SqlCommand cmd = new SqlCommand(
            @" INSERT INTO Company (password, companyName, companyId, companyLeader,
                                            companyAddress, fax, telephone, email1,  email2, email3, email4, email5,factoryType,isFactory,isMember)VALUES( @password,@companyName, @companyId, @companyLeader,
                                            @companyAddress, @fax, @telephone, @email1,  @email2, @email3, @email4, @email5,@factoryType,@isFactory,@isMember)");
            if (newMember.email2 == null)
                newMember.email2 = "";
            if (newMember.email3 == null)
                newMember.email3 = "";
            if (newMember.email4 == null)
                newMember.email4 = "";
            if (newMember.email5 == null)
                newMember.email5 = "";
            //將密碼Hash過
            newMember.password = HashPassWord(newMember.password);
            //需完成寫入sql database
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@password", newMember.password));
            cmd.Parameters.Add(new SqlParameter("@companyName", newMember.companyName));
            cmd.Parameters.Add(new SqlParameter("@companyId", newMember.companyId));
            cmd.Parameters.Add(new SqlParameter("@companyLeader", newMember.companyLeader));
            cmd.Parameters.Add(new SqlParameter("@companyAddress", newMember.companyAddress));
            cmd.Parameters.Add(new SqlParameter("@fax", newMember.fax));
            cmd.Parameters.Add(new SqlParameter("@telephone", newMember.telephone));
            cmd.Parameters.Add(new SqlParameter("@email1", newMember.email1));
            cmd.Parameters.Add(new SqlParameter("@email2", newMember.email2));
            cmd.Parameters.Add(new SqlParameter("@email3", newMember.email3));
            cmd.Parameters.Add(new SqlParameter("@email4", newMember.email4));
            cmd.Parameters.Add(new SqlParameter("@email5", newMember.email5));
            cmd.Parameters.Add(new SqlParameter("@factoryType", newMember.factoryType));
            cmd.Parameters.Add(new SqlParameter("@isFactory", newMember.isFactory));
            cmd.Parameters.Add(new SqlParameter("@isMember", newMember.isMember));
            //確保程式不會因執行錯誤而整個中斷
            try
            {
                //開啟資料庫連線
                conn.Open();
                //執行Sql指令
                cmd.ExecuteNonQuery();
            }
            catch //(Exception e)
            {
                //丟出錯誤
                //throw new Exception(e.Message.ToString());
                accountExist = true;
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
            return accountExist;
        }


        #region 帳號註冊重複確認
        //public string AccountCheck(string companyId)
        //{
        //    //取得帳號
        //    string result = GetDataById(companyId);
        //    //宣告驗證後訊息字串
        //    //bool result = (Data == null);
        //    return result;
        //}


        //public string GetDataById(string companyId)
        //{
        //    // Members Data = new Members();
        //    string account = "";
        //    SqlConnection conn = new SqlConnection(connStr);
        //    //SqlConnection conn = new SqlConnection();
        //    //conn.ConnectionString = connStr;
        //    string commandStr = "SELECT * FROM company WHERE companyId = @companyId";
        //    SqlCommand sqlCommand = new SqlCommand(commandStr);
        //    sqlCommand.Connection = conn;
        //    sqlCommand.Parameters.Add(new SqlParameter("@companyId", companyId));
        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader reader = sqlCommand.ExecuteReader();
        //        reader.Read();
        //        account = reader["companyId"].ToString();
        //    }
        //    catch
        //    {
        //        //查無資料
        //        account = "";
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    //throw new NotImplementedException();
        //    return account;
        //}
        #endregion
    }

}
