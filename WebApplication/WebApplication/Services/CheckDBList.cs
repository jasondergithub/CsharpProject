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
        public void CreateList(CheckList list)
        {

            SqlConnection conn = new SqlConnection(connStr);
            string ss = " INSERT INTO checkList (two_1, two_2, two_3, two_4, two_5, three_six, three, three_1, three_2, four, four_1, four_2, five_1, five_2, five_3)VALUES(@two_1, @two_2, @two_3, @two_4, @two_5, @three_six, @three, @three_1, @three_2, @four, @four_1, @four_2, @five_1, @five_2, @five_3)";
            SqlCommand cmd = new SqlCommand(@ss);
            //SqlCommand cmd = new SqlCommand(
            //@" INSERT INTO checkList (three_six, three, three_1, three_2, four, four_1, four_2, five_1, five_2, five_3)
            //                                 VALUES(@three_six, @three, @three_1, @three_2, @four, @four_1, @four_2, @five_1, @five_2, @five_3)");

            //openEssential(list);    // 開資料表權限
            // openEssentialWithFalse(list);   // 開資料表權限，設定true跟false

            foreach (PropertyInfo prop in typeof(CheckList).GetProperties())
            {
                if (prop.GetValue(list) == null)
                {
                    prop.SetValue(list, "");
                }
            }
            //openEssential(list);    // 開資料表權限

            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@two_1", list.two_1));
            cmd.Parameters.Add(new SqlParameter("@two_2", list.two_2));
            cmd.Parameters.Add(new SqlParameter("@two_3", list.two_3));
            cmd.Parameters.Add(new SqlParameter("@two_4", list.two_4));
            cmd.Parameters.Add(new SqlParameter("@two_5", list.two_5));
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

        public void openEssential(CheckList list)
        {
            Dictionary<string, List<string>> folder = new Dictionary<string, List<string>>();
            List<string> li1 = new List<string>();
            li1.Add("essentialF");
            List<string> li2 = new List<string>();
            li2.Add("essentialE"); li2.Add("essentialG"); li2.Add("essentialH");
            List<string> li3 = new List<string>();
            li3.Add("essentialB");
            folder["健保給付"] = li1;
            folder["差額給付"] = li2;
            folder["給付給付"] = li3;
            List<string> all_list = new List<string>();
            foreach (PropertyInfo prop in typeof(CheckList).GetProperties())
            {
                if (prop.GetValue(list) != null)
                {
                    if (folder.ContainsKey(prop.GetValue(list).ToString()))
                    {
                        List<string> tmp = folder[prop.GetValue(list).ToString()];
                        all_list.AddRange(tmp);
                    }
                }
            }
            
            string comstr_head = "INSERT INTO essentialAndReason (";

            string result = String.Join(", ", all_list.ToArray());

            string comstr_mid = comstr_head + result + ")VALUES(";

            foreach (var item in all_list)
            {
                comstr_mid = comstr_mid+"@"+item+", ";
            }
            string comstr_fin = comstr_mid.Remove(comstr_mid.LastIndexOf(", "), 1) + ")";
            SqlConnection conn = new SqlConnection(connStr);
            // SqlCommand cmd = new SqlCommand(@comstr1);
            SqlCommand cmd = new SqlCommand(@comstr_fin);

            cmd.Connection = conn;
            foreach(var item in all_list)
            {
                //cmd.Parameters.Add(new SqlParameter("@"+item, true));
                cmd.Parameters.Add(new SqlParameter("@" + item, 1));
            }
            try
            {
                conn.Open();    //開啟資料庫連線
                cmd.ExecuteNonQuery();   //執行Sql指令
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());  //丟出錯誤
            }
            finally
            {
                conn.Close();   //關閉資料庫連線
            }

        }

        public void openEssentialWithFalse(CheckList list)
        {
            Dictionary<string, List<string>> folder = new Dictionary<string, List<string>>();
            List<string> li1 = new List<string>();
            li1.Add("essentialF");
            List<string> li2 = new List<string>();
            li2.Add("essentialE"); li2.Add("essentialG"); li2.Add("essentialH");
            List<string> li3 = new List<string>();
            li3.Add("essentialB");
            folder["健保給付"] = li1;
            folder["差額給付"] = li2;
            folder["給付給付"] = li3;
            List<string> all_list = new List<string>();
            List<string> all_in_one = new List<string>();
            all_in_one.Add("essentialA"); all_in_one.Add("essentialB"); all_in_one.Add("essentialC"); all_in_one.Add("essentialD"); all_in_one.Add("essentialE"); all_in_one.Add("essentialF"); all_in_one.Add("essentialG"); all_in_one.Add("essentialH"); all_in_one.Add("essentialI"); all_in_one.Add("essentialJ"); all_in_one.Add("essentialK"); all_in_one.Add("essentialL"); all_in_one.Add("essentialM"); all_in_one.Add("essentialN"); all_in_one.Add("essentialO"); all_in_one.Add("essentialP"); all_in_one.Add("essentialQ"); all_in_one.Add("essentialR"); all_in_one.Add("essentialS"); all_in_one.Add("essentialT"); all_in_one.Add("essentialU");
            foreach (PropertyInfo prop in typeof(CheckList).GetProperties())
            {
                if (prop.GetValue(list) != null)
                {
                    if (folder.ContainsKey(prop.GetValue(list).ToString()))
                    {
                        List<string> tmp = folder[prop.GetValue(list).ToString()];
                        all_list.AddRange(tmp);
                    }
                }
            }
            string comstr_head = "INSERT INTO essentialAndReason (essentialA, essentialB, essentialC, essentialD, essentialE, essentialF, essentialG, essentialH, essentialI, essentialJ, essentialK, essentialL, essentialM, essentialN, essentialO, essentialP, essentialQ, essentialR, essentialS, essentialT, essentialU)" +
                "VALUES(@essentialA, @essentialB, @essentialC, @essentialD,@essentialE, @essentialF, @essentialG, @essentialH, @essentialI, @essentialJ, @essentialK, @essentialL, @essentialM, @essentialN, @essentialO, @essentialP, @essentialQ, @essentialR, @essentialS, @essentialT, @essentialU)";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(@comstr_head);
            cmd.Connection = conn;
            foreach (var item in all_in_one)
            {
               if (all_list.Contains(item))
                {
                    cmd.Parameters.Add(new SqlParameter("@" + item, true));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@" + item, false));
                }
            }
            try
            {
                conn.Open();    //開啟資料庫連線
                cmd.ExecuteNonQuery();   //執行Sql指令
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());  //丟出錯誤
            }
            finally
            {
                conn.Close();   //關閉資料庫連線
            }

        }
    }


}