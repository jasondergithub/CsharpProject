using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class AdminDBService
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public void writeModifyStatus(string projectId, Judge judge)
        {
            string cmdStr;
            SqlConnection conn = new SqlConnection(connStr);
            //System.Diagnostics.Debug.WriteLine(projectId);
            //System.Diagnostics.Debug.WriteLine(judge.judgeA);
            //System.Diagnostics.Debug.WriteLine(judge.judgeB);
            List<string> judgelabels = new List<string>() {"judgeA", "judgeB", "judgeC", "judgeD", "judgeE",
                              "judgeF", "judgeG", "judgeH", "judgeI", "judgeJ", "judgeK", "judgeL", "judgeM",
                              "judgeN", "judgeO", "judgeP", "judgeQ", "judgeR", "judgeS", "judgeT", "judgeU"};

            cmdStr = "UPDATE essentialAndReason SET ";
            bool writeValue = false;
            foreach (PropertyInfo prop in typeof(Judge).GetProperties())
            {
                //System.Diagnostics.Debug.WriteLine(prop.GetValue(reasons));
                //System.Diagnostics.Debug.WriteLine(prop.Name);
                if (prop.GetValue(judge) != null)
                {       //cmdStr = cmdStr + prop.Name + '='+ prop.GetValue(reasons) + ',';
                    cmdStr = cmdStr + prop.Name + '=' + '@' + prop.Name + ',';
                    writeValue = true;
                }
            }
            if (writeValue)
            {
                cmdStr = cmdStr.TrimEnd(',') + ' ';
                cmdStr = cmdStr + "WHERE projectId=@projectId";
                System.Diagnostics.Debug.WriteLine(cmdStr);

                SqlCommand cmd = new SqlCommand((@cmdStr));
                cmd.Connection = conn;
                foreach (PropertyInfo prop in typeof(Judge).GetProperties())
                {
                    System.Diagnostics.Debug.WriteLine(prop.GetValue(judge));
                    if (prop.GetValue(judge) != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + prop.Name, prop.GetValue(judge)));
                    }

                }
                cmd.Parameters.Add(new SqlParameter("@projectId", projectId));
            }

        }
    }
}