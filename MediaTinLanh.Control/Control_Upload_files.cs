using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MediaTinLanh.Control
{
    public class Control_Upload_files
    {
        #region Upload file

        public static void upload_files(string localFile, string remotepath)
        {
            using (var client = new WebClient())
            {
                Control_Xml xml  = new Control_Xml();;
                string ftpusername = "";
                string ftpPassword = "";
                string ftpserver = "";
                xml.GetFtpAccount("config.xml",ref ftpserver,ref ftpusername,ref ftpPassword);
                client.Credentials = new NetworkCredential(ftpusername, ftpPassword);
                client.UploadFile(ftpserver + remotepath, WebRequestMethods.Ftp.UploadFile, localFile);
            }
        }

        #endregion

        #region Download files

        public static void Download_files(string inputfilepath, string FilePathOnRemote)
        {
            Control_Xml xml = new Control_Xml(); ;
            string ftpusername = "";
            string ftpPassword = "";
            string ftpserver = "";
            xml.GetFtpAccount("config.xml", ref ftpserver, ref ftpusername, ref ftpPassword);

            string ftpfullpath = "ftp://" + ftpserver +"//"+ FilePathOnRemote;

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential(ftpusername, ftpPassword);
                byte[] fileData = request.DownloadData(ftpfullpath);

                FileInfo fileInfo = new FileInfo(inputfilepath);
                fileInfo.Directory.Create();

                using (FileStream file = File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
            }
        }

        #endregion
    }
}
