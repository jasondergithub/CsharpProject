using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ProjectService
    {
        //建立資料庫連結
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        //新增Project
        public void Createroject(Project newProject)
        {
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"INSERT INTO Project (projectGenre,checkAB,judgeState)
                VALUES (@projectGenre,@checkAB,@judgeState)");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@projectGenre", newProject.projectGenre));
            sqlCommand.Parameters.Add(new SqlParameter("@checkAB", false));
            sqlCommand.Parameters.Add(new SqlParameter("@judgeState", false));
            //確保程式不會因執行錯誤而中斷
            try
            {
                //資料庫連線
                sqlConnection.Open();
                //執行Sql指令
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }

        }

    }
}