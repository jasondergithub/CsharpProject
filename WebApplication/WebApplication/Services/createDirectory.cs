using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;


namespace WebApplication.Services
{
    public class createDirectory
    {
        public bool createDir(string dirName)
        {
            try
            {
                //string dirName = "CreateDirTest";
                WebRequest request = WebRequest.Create("ftp://140.124.183.13/" + dirName);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("ftpuser", "wiki1424");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    //Console.WriteLine(resp.StatusCode);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
            //return false;
        }
    }
}