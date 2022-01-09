using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ProjectService
    {
        //建立資料庫連結
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        //新增Project
        public void Createproject(Project newProject) //, string name)
        {
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"INSERT INTO Project (projectId, projectGenre,time,season,projectNo,hospitalUser,department,checkAB,judgeState,CompanyId,buyReason,usage,predictOfUsePerMonth,recommandId)
                VALUES (@projectId, @projectGenre,@time,@season,@projectNo,@hospitalUser,@department,@checkAB,@judgeState,@CompanyId,@buyReason,@usage,@predictOfUsePerMonth,@recommandId)");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@projectId", newProject.projectId));
            sqlCommand.Parameters.Add(new SqlParameter("@projectGenre", newProject.projectGenre));
            sqlCommand.Parameters.Add(new SqlParameter("@time", newProject.time));
            sqlCommand.Parameters.Add(new SqlParameter("@season", newProject.season));
            sqlCommand.Parameters.Add(new SqlParameter("@projectNo", newProject.projectNo));
            sqlCommand.Parameters.Add(new SqlParameter("@department", newProject.department));
            sqlCommand.Parameters.Add(new SqlParameter("@hospitalUser", newProject.hospitalUser));
            sqlCommand.Parameters.Add(new SqlParameter("@checkAB", false));
            sqlCommand.Parameters.Add(new SqlParameter("@judgeState", false));
            sqlCommand.Parameters.Add(new SqlParameter("@CompanyId", newProject.companyId));
            sqlCommand.Parameters.Add(new SqlParameter("@buyReason", newProject.buyReason));
            sqlCommand.Parameters.Add(new SqlParameter("@usage", newProject.usage));
            sqlCommand.Parameters.Add(new SqlParameter("@predictOfUsePerMonth", newProject.predictOfUsePerMonth));
            sqlCommand.Parameters.Add(new SqlParameter("@recommandId", newProject.recommandId));
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
        public List<string> getCompanyName()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connStr;
            DataSet ds = new DataSet();
            SqlDataAdapter daCompany = new SqlDataAdapter("SELECT DISTINCT companyName FROM Company", sqlConnection);
            daCompany.Fill(ds);
            DataTable dt = ds.Tables [0];
            List<string> result = new List<string>();
            for(int i=0; i<dt.Rows.Count; i++)
            {
                result.Add(dt.Rows[i][0].ToString());
            }
            return result;
        }

        public string getIdByName(string name)
        {
            string id;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connStr;
            SqlCommand sqlCommand = new SqlCommand("SELECT companyId FROM Company WHERE companyName=@companyName", 
                sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter("@companyName", SqlDbType.NVarChar)).Value = name;            
            SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            id = dt.Rows[0][0].ToString();
            return id;
        }
        /* 取得日期 */
        public string getDate()
        {
            var nowDateTime = DateTime.UtcNow.AddHours(8);
            string result = nowDateTime.ToString("yyyy/MM/dd");
            //Console.WriteLine(result);
            return result;
        }

        /* 取得季節 */
        public string getSeason()
        {
            int month = Int16.Parse(DateTime.UtcNow.AddHours(8).ToString("MM"));
            if (month <= 3)
            {
                return "Q1";
            }
            else if (month <= 6)
            {
                return "Q2";
            }
            else if (month <= 6)
            {
                return "Q3";
            }
            else
            {
                return "Q4";
            }
        }

        /* 取得流水號 */
        public int getProjectNo(string departmentCheck)
        {
            SqlConnection sqlConnection = new SqlConnection(connStr);
            /* 找到當前流水號最大值 */
            SqlCommand sqlCommand = new SqlCommand(@"SELECT MAX(projectNo) FROM Project WHERE department=@value1");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@value1", departmentCheck));
            try
            {
                sqlConnection.Open();
                //SqlDataReader reader = sqlCommand.ExecuteReader();
                //reader.Read();
                int temp = (int)sqlCommand.ExecuteScalar();
                temp ++;
                return temp;
            }
            catch
            {
                int temp = 1;
                return temp;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string getProjectId(string Genre, string Time, string Season, string Department, int Num)
        {
            string strNum = String.Format("{0:000}", Num);
            string Date = Time.Substring(5, 2) + Time.Substring(8, 2);
            string Id = Genre + Date + Season + Department + strNum;
            return Id;
        }
        public List<Project> getProjetctByUser(string user)
        {
            List<Project> projects = new List<Project>();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Project Where hospitalUser=@hospitalUser");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@hospitalUser", user));
            sqlConnection.Open();
            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Project project = new Project
                        {
                            projectId = reader.GetString(reader.GetOrdinal("projectId")),
                            usage = reader.GetString(reader.GetOrdinal("usage"))
                        };
                        projects.Add(project);
                    }
                }
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            sqlConnection.Close();
            return projects;
        }
        public List<Project> getProjetctByCompany(string user)
        {
            List<Project> projects = new List<Project>();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Project Where CompanyId=@CompanyId");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@CompanyId", user));
            sqlConnection.Open();
            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Project project = new Project
                        {
                            projectId = reader.GetString(reader.GetOrdinal("projectId")),
                            usage = reader.GetString(reader.GetOrdinal("usage")),
                            checkAB = reader.GetBoolean(reader.GetOrdinal("checkAB"))
                        };
                        projects.Add(project);
                    }
                }
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
            sqlConnection.Close();
            return projects;
        }
        public List<bool> getEssentialValue(string projectId)
        {
            List<bool> essentialValueList  = new List<bool>();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM essentialAndReason Where projectId=@projectId");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@projectId", projectId));
            sqlConnection.Open();
            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    for(int i = 1; i < 22; i++)
                    {
                        bool value;
                        value = reader.GetBoolean(i);
                        essentialValueList.Add(value);
                    }
                    //System.Diagnostics.Debug.WriteLine(essentialValueList[1]);
                }
            }
            catch (Exception e)
            {
                //丟出錯誤
                throw new Exception(e.Message.ToString());
            }
           
            return essentialValueList;

        }
    }
}