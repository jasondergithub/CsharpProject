using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication.Services
{
    public class uploadFileService
    {
        public void UploadToFtp(HttpPostedFileBase uploadfile, string folderName, int count)
        {
            var uploadurl = "ftp://140.124.183.13/" + folderName + "/";
            var uploadfilename = uploadfile.FileName;
            var username = "ftpuser";
            var password = "wiki1424";

            switch (count)
            {
                case 1:
                    uploadurl += "sp";
                    break;
                case 2:
                    uploadurl += "3m";
                    break;
                case 3:
                    uploadurl += "cp";
                    break;
                case 4:
                    uploadurl += "pd";
                    break;
                case 5:
                    uploadurl += "rc";
                    break;
                case 6:
                    uploadurl += "amcn";
                    break;
            }

            Stream streamObj = uploadfile.InputStream;
            byte[] buffer = new byte[uploadfile.ContentLength];
            streamObj.Read(buffer, 0, buffer.Length);
            streamObj.Close();
            streamObj = null;
            string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
            var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
            requestObj.Method = WebRequestMethods.Ftp.UploadFile;
            requestObj.Credentials = new NetworkCredential(username, password);
            Stream requestStream = requestObj.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Flush();
            requestStream.Close();
            requestObj = null;
        }

        public void UploadToFtp(HttpPostedFileBase uploadfile, string folderName)
        {
            var uploadurl = "ftp://140.124.183.13/" + folderName + "/";
            var uploadfilename = uploadfile.FileName;
            var username = "ftpuser";
            var password = "wiki1424";

            Stream streamObj = uploadfile.InputStream;
            byte[] buffer = new byte[uploadfile.ContentLength];
            streamObj.Read(buffer, 0, buffer.Length);
            streamObj.Close();
            //streamObj = null;
            string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
            var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
            requestObj.Method = WebRequestMethods.Ftp.UploadFile;
            requestObj.Credentials = new NetworkCredential(username, password);
            Stream requestStream = requestObj.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Flush();
            requestStream.Close();
            //requestObj = null;
        }
    }
}