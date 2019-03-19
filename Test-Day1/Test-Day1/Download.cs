using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Test_Day1
{
    //https://www.cnblogs.com/downmoon/articles/1217269.html
    public class Download
    {

        public  bool DownloadOneFileByURL(string url, string localPath)
        {
            try
            {
                string fileName = url.Split('.').LastOrDefault();
                fileName = "." + fileName;
                System.Net.HttpWebRequest request = null;
                System.Net.HttpWebResponse response = null;
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                request.Timeout = 3000;//8000 Not work ?
                response = (System.Net.HttpWebResponse)request.GetResponse();
                //int length2 = Int32.TryParse(response.ContentLength.ToString(), out 0);
                int length2 = Int32.Parse(response.ContentLength.ToString());
                if (length2 >= 10 * 1024) //图片大小不足时跳过
                {
                    //Stream s = response.GetResponseStream();  //这种方式读不出来？能下下来读不出来？
                    //BinaryReader br = new BinaryReader(s);
                    //byte[] byteArr = new byte[length2];
                    //s.Read(byteArr, 0, length2);
                    //if (!File.Exists(localPath + fileName)) //当存在文件时跳过
                    //{
                    //    if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
                    //    FileStream fs = File.Create(localPath + fileName);
                    //    fs.Write(byteArr, 0, length2);
                    //    fs.Close();
                    //}
                    //br.Close();
                    if (!File.Exists(localPath + length2 + fileName))
                    {
                        try
                        {
                            response = (HttpWebResponse)request.GetResponse();
                            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                            Stream stream = response.GetResponseStream();
                            if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
                            FileStream fileStream = new FileStream(localPath + length2 + fileName, FileMode.Create, FileAccess.Write);
                            byte[] bytes = new byte[length2];
                            int readSize = 0;
                            while ((readSize = stream.Read(bytes, 0, length2)) > 0)
                            {
                                fileStream.Write(bytes, 0, readSize);
                                fileStream.Flush();
                            }
                            response.Close();
                            stream.Close();
                            fileStream.Close();
                            Console.WriteLine(length2 + fileName + "已完成！");
                            return true;
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("下载图片时：" + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine(length2 + fileName + "已存在！");
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return false;
        }
        /// <summary>
        ///Web Client Method ,only For Small picture,else large please use FTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="url"></param>
        /// <param name="localPath"></param>
        public  void DownloadOneFileByURLWithWebClient(string url, string localPath)//可以直接下载，但是没办法判断大小
        {
            string fileName = url.Split('/').LastOrDefault();
            System.Net.WebClient wc = new System.Net.WebClient();
            if (!File.Exists(localPath + fileName))
            {
                if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
                wc.DownloadFile(url, localPath + fileName);
            }
        }
    }

}
