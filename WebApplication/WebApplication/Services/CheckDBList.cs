using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

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
            @" INSERT INTO checkList (three_six, three, three_1, three_2, four, four_1, four_2, five_1, five_2, five_3)
                                             VALUES(@three_six, @three, @three_1, @three_2, @four, @four_1, @four_2, @five_1, @five_2, @five_3)");

            foreach( PropertyInfo prop in typeof(CheckList).GetProperties() )
            {
                if (prop.GetValue(list)==null)
                {
                    prop.SetValue(list, "");
                }
            }

            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@three_six", list.three_six));
            cmd.Parameters.Add(new SqlParameter("@three", list.three));
            cmd.Parameters.Add(new SqlParameter("@three_1", list.three_1));
            cmd.Parameters.Add(new SqlParameter("@three_2", list.three_2));
            cmd.Parameters.Add(new SqlParameter("@four", list.four));
            cmd.Parameters.Add(new SqlParameter("@four_1", list.four_1));
            cmd.Parameters.Add(new SqlParameter("@four_2", list.four_2));
            cmd.Parameters.Add(new SqlParameter("@five_1", list.five_1));
            cmd.Parameters.Add(new SqlParameter("@five_2", list.five_2));
            cmd.Parameters.Add(new SqlParameter("@five_3", list.five_3));

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