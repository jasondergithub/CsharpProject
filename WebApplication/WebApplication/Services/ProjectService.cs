using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Services
{
    public class ProjectService
    {
        //建立資料庫連結
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        //新增Project
        public void Createproject(Project newProject) //, string name)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Project (projectId, projectGenre,time,season,projectNo,hospitalUser,department,checkAB,judgeState,CompanyId,buyReason,usage,predictOfUsePerMonth,recommandId)
                VALUES (@projectId, @projectGenre,@time,@season,@projectNo,@hospitalUser,@department,@checkAB,@judgeState,@CompanyId,@buyReason,@usage,@predictOfUsePerMonth,@recommandId)");
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@projectId", newProject.projectId));
            cmd.Parameters.Add(new SqlParameter("@projectGenre", newProject.projectGenre));
            cmd.Parameters.Add(new SqlParameter("@time", newProject.time));
            cmd.Parameters.Add(new SqlParameter("@season", newProject.season));
            cmd.Parameters.Add(new SqlParameter("@projectNo", newProject.projectNo));
            cmd.Parameters.Add(new SqlParameter("@department", newProject.department));
            cmd.Parameters.Add(new SqlParameter("@hospitalUser", newProject.hospitalUser));
            cmd.Parameters.Add(new SqlParameter("@checkAB", false));
            cmd.Parameters.Add(new SqlParameter("@judgeState", false));
            cmd.Parameters.Add(new SqlParameter("@CompanyId", newProject.companyId));
            cmd.Parameters.Add(new SqlParameter("@buyReason", newProject.buyReason));
            cmd.Parameters.Add(new SqlParameter("@usage", newProject.usage));
            cmd.Parameters.Add(new SqlParameter("@predictOfUsePerMonth", newProject.predictOfUsePerMonth));
            cmd.Parameters.Add(new SqlParameter("@recommandId", newProject.recommandId));
            //確保程式不會因執行錯誤而中斷
            try
            {
                //資料庫連線
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
                conn.Close();
            }

        }
        public List<string> getCompanyName()
        {
            SqlConnection conn = new SqlConnection(connStr);
            // conn.ConnectionString = connStr;
            DataSet ds = new DataSet();
            SqlDataAdapter daCompany = new SqlDataAdapter("SELECT DISTINCT companyName FROM Company", conn);
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
            SqlConnection conn = new SqlConnection(connStr);
            // sqlConnection.ConnectionString = connStr;
            SqlCommand cmd = new SqlCommand("SELECT companyId FROM Company WHERE companyName=@companyName",
                conn);
            cmd.Parameters.Add(new SqlParameter("@companyName", SqlDbType.NVarChar)).Value = name;            
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
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
            SqlConnection conn = new SqlConnection(connStr);
            /* 找到當前流水號最大值 */
            SqlCommand cmd = new SqlCommand(@"SELECT MAX(projectNo) FROM Project WHERE department=@value1");
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@value1", departmentCheck));
            try
            {
                conn.Open();
                //SqlDataReader reader = sqlCommand.ExecuteReader();
                //reader.Read();
                int temp = (int)cmd.ExecuteScalar();
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
                conn.Close();
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
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Project Where hospitalUser=@hospitalUser");
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@hospitalUser", user));
            conn.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
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
            conn.Close();
            return projects;
        }
        public List<Project> getProjetctByCompany(string user)
        {
            List<Project> projects = new List<Project>();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Project Where CompanyId=@CompanyId");
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@CompanyId", user));
            conn.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
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
            conn.Close();
            return projects;
        }
        public List<bool> getEssentialValue(string projectId)
        {
            List<bool> essentialValueList  = new List<bool>();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM essentialAndReason Where projectId=@projectId");
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@projectId", projectId));
            conn.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
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

        public void writeReason2DB(List<string> reasonNames, Reason reasons, string projectId) 
        {
            System.Diagnostics.Debug.WriteLine(projectId);
            SqlConnection conn = new SqlConnection(connStr);
            string cmdStr = "UPDATE essentialAndReason SET ";
            int i = 0;
            foreach(PropertyInfo prop in typeof(Reason).GetProperties())
            {
                //System.Diagnostics.Debug.WriteLine(prop.GetValue(reasons));
                //System.Diagnostics.Debug.WriteLine(prop.Name);
                if (prop.GetValue(reasons)!=null)
                    //cmdStr = cmdStr + prop.Name + '='+ prop.GetValue(reasons) + ',';
                    cmdStr = cmdStr + prop.Name + '='+'@'+ prop.Name + ',';
                i++;
            }

            cmdStr = cmdStr.TrimEnd(',')+' ';
            cmdStr = cmdStr + "WHERE projectId=@projectId";
            System.Diagnostics.Debug.WriteLine(cmdStr);


            SqlCommand cmd = new SqlCommand((@cmdStr));
            cmd.Connection = conn;
            
            foreach (PropertyInfo prop in typeof(Reason).GetProperties())
            {
                if (prop.GetValue(reasons) != null)
                    //cmd.Parameters.Add(new SqlParameter("@password", newMember.password));
                    cmd.Parameters.Add(new SqlParameter("@"+ prop.Name, prop.GetValue(reasons)));
            }
            cmd.Parameters.Add(new SqlParameter("@projectId", projectId));

            try
            {
                conn.Open();    /* 開啟資料庫連線 */
                cmd.ExecuteNonQuery();   /* 執行Sql指令 */
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());  /* 丟出錯誤 */
            }
            finally
            {
                conn.Close();   /* 關閉資料庫連線 */
            }
        }

    }
}