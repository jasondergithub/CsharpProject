using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication.Services
{
    public class CheckDBList
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public void  CreateList(CheckList list)
        {
            SqlConnection conn = new SqlConnection(connStr);
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = connStr;

            SqlCommand cmd = new SqlCommand(
            @" INSERT INTO checkList (three_1, four_1) VALUES (@three_1,@four_1)");

            if (list.three_1 == null)
                list.three_1 = "";
            if (list.four_1 == null)
                list.four_1 = "";
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@three_1", list.three_1));
            cmd.Parameters.Add(new SqlParameter("@four_1", list.four_1));




            try
            {
                //開啟資料庫連線
                conn.Open();
                //執行Sql指令
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
                
            }
            finally
            {
                //關閉資料庫連線
                conn.Close();
            }
        }
    }
}