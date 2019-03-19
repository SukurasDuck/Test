using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Threading;

namespace Test_Day1
{
    /// <summary>
    /// 代理ip模型
    /// </summary>
    class ProxyViewModel
    {
        /// <summary>
        /// IP:PORT
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string ProxyIP { get; set; }
        /// <summary>
        /// IP端口
        /// </summary>
        public int ProxyPort { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
    class Program
    {
        //登录访问的cookie
        private static string cookie = @"bid=NLEtWV9624Y; __utmc=30149280; __utmz=30149280.1552870629.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); douban-fav-remind=1; __yadk_uid=cpckABelaoBqkzDeoGfrJsmTtcQGHdVD; ct=y; ll=" + 118282 + "; _pk_ses.100001.8cb4=*; ap_v=0,6.0; __gads=ID=260ea25819e4e065:T=1552898329:S=ALNI_MYL-aQmZifouLuEhNftDsmEu9NCCw; __utma=30149280.1538213622.1552870629.1552894265.1552898330.5; __utmt=1; push_noty_num=0; push_doumail_num=0; __utmv=30149280.19357; dbcl2=\"193577791:MrSJyYH6Ol4\"; ck=-r7b; _pk_id.100001.8cb4=8e53c1407a6afe2c.1552870604.5.1552898588.1552894263.; __utmb=30149280.39.5.1552898589974";
        //帖子地址集合
        private static List<string> postingsHref = new List<string>();
        //代理ip集合
        private static List<ProxyViewModel> _ProxyQueue = new List<ProxyViewModel>();
        static void Main(string[] args)
        {
            _ProxyQueue.Add(new ProxyViewModel { Id = "1", ProxyIP = "192.168.0.49", ProxyPort = 1080, CreateTime = DateTime.Now, State = 1 });
            // _ProxyQueue.Add(new ProxyViewModel { Id = "2", ProxyIP = "221.7.211.246", ProxyPort = 60233, CreateTime = DateTime.Now, State = 1 });
            const int count = 500;
            for (int i = 0; i <= count; i += 25)
            {
                GetPostingsList(i);
                Console.WriteLine("等待1s---");
                Thread.Sleep(1000);
            }

            foreach (var item in postingsHref)
            {
                GetPosting(item);
                Console.WriteLine("等待1s---");
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 获取帖子列表
        /// </summary>
        /// <param name="page">所需要的行数</param>
        static void GetPostingsList(int page)
        {
            string url = @"https://www.douban.com/group/lvxing/discussion?start=" + page;
            var htmlStr = "";
            getHtmlstr(url, ref htmlStr);//代理
            //getHtmlByCookie(url,ref htmlStr); //登录
            if (!htmlStr.Equals(""))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlStr);
                var nodes = htmlDoc.DocumentNode.SelectNodes("//td[@class='title']");
                if (nodes == null)
                {
                    Console.WriteLine(htmlDoc);
                }
                else
                {
                    //得到帖子列表的table
                    //Console.WriteLine(nodes.Count);
                    foreach (var item in nodes)
                    {
                        if (item.HasChildNodes)
                        {
                            //Console.WriteLine(item.SelectSingleNode("a").Attributes["href"].Value);
                            postingsHref.Add(item.SelectSingleNode("a").Attributes["href"].Value);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 获取帖子
        /// </summary>
        /// <param name="url">帖子地址</param>
        static void GetPosting(string url)
        {
            //url = "https://www.douban.com/group/topic/133971891/";
            var htmlStr = "";
            getHtmlstr(url, ref htmlStr); //代理
            //getHtmlByCookie(url, ref htmlStr); //登录
            if (!htmlStr.Equals(""))
            {
                try
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlStr);
                    //先得到内容div
                    var nodes = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='content']");
                    //得到帖子名称
                    var postName = nodes.SelectSingleNode("h1[1]").InnerText;
                    //得到发帖时间
                    var postCreateDate = nodes.SelectSingleNode("//div[@class='topic-doc'][1]/h3[1]/span[2]").InnerHtml;

                    //得到图片的div
                    var imgs = nodes.SelectNodes("//div[@class='image-wrapper']");
                    if (imgs != null)
                    {
                        List<string> imgUrls = new List<string>();
                        foreach (var item in imgs)
                        {
                            if (item.HasChildNodes)
                            {
                                imgUrls.Add(item.ChildNodes.FirstOrDefault().Attributes["src"].Value);
                            }
                        }
                        getImgs(imgUrls, postName.Replace("\n", "").Replace(" ", ""), getStrDate(postCreateDate));
                    }
                    else
                    {
                        Console.WriteLine(postName + "没有图片,跳过--");
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("获取帖子中发生报错" + e.Message);
                }
            }
        }
        /// <summary>
        /// 将时间截取只需要日期
        /// </summary>
        /// <param name="longStr">把帖子的长日期转为短日期</param>
        /// <returns></returns>
        static string getStrDate(string longStr)
        {
            return longStr.Substring(0, 10);
        }
        /// <summary>
        /// 获取西刺高匿代理  https://github.com/Yahuiya/Proxy/blob/master/CoreProxyIP/HtmlRule.cs
        /// </summary>
        /// <returns></returns>
        static void XiciGaoni()
        {
            Console.WriteLine("开始获取代理ip--");
            while (true)
            {
                try
                {
                    string url = "http://www.xicidaili.com/nn/";
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(IWebClient(url));//这里因为 Html Agility Pack请求不到
                    var tdList = doc.DocumentNode.SelectNodes("//tr[@class='odd']");
                    foreach (var item in tdList)
                    {
                        try
                        {
                            var ip = item.SelectSingleNode("td[2]").InnerHtml;
                            int port = Convert.ToInt32(item.SelectSingleNode("td[3]").InnerHtml);
                            ProxyViewModel proxy = new ProxyViewModel() { Id = string.Format("{0}:{1}", ip, port), ProxyIP = ip, ProxyPort = port, CreateTime = DateTime.Now, State = 1 };
                            if (_ProxyQueue.Where(model => model.ProxyIP.Equals(ip)).Count() > 0) continue;//判断队列是否存在代理
                            _ProxyQueue.Add(proxy);
                            //ProxyVerification(proxy, "西刺nn");（这里验证ip）直接使用，返回错误则删掉
                        }
                        catch (Exception e)
                        { }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("获取代理IP时" + e.Message);
                    break;
                }
                if (_ProxyQueue.Where(model => model.State == 1).Count() > 10) { break; }
                else
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    Console.WriteLine("再次请求代理ip");
                }

            }

        }
        /// <summary>
        /// 获取页面的HTML
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string IWebClient(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Cookie", "__cfduid=dbc0747d4f20e880b1f3fbeddd7ee7f9b1518096123; Hm_lvt_24b7d5cc1b26f24f256b6869b069278e=1518096226; yjs_id=4b3a274b8bb0be2202978cee06964563; yd_cookie=5bc973fb-f37b-425614221fbda95de6a441f2298ff2543cf1; UM_distinctid=1654c7c2cf02a0-09d94a3256c3ad-514d2f1f-144000-1654c7c2cf35bb; CNZZDATA1254651946=1879619778-1534585157-%7C1534585157; Hm_lvt_8ccd0ef22095c2eebfe4cd6187dea829=1534586531; Hm_lpvt_8ccd0ef22095c2eebfe4cd6187dea829=1534586545");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.75 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 通过代理ip访问地址
        /// </summary>
        /// <param name="url">要访问的地址</param>
        /// <param name="html">返回的html</param>
        static void getHtmlstr(string url, ref string html)
        {
            Console.WriteLine("当前代理ip数量" + _ProxyQueue.Where(model => model.State == 1).Count());
            var proxy = _ProxyQueue.FirstOrDefault(model => model.State == 1);//从代理iplist中取一个
            if (proxy == null)
            {
                XiciGaoni();
                getHtmlstr(url, ref html);
            }
            else
            {
                try
                {
                    Console.WriteLine("开始使用" + proxy.ProxyIP + ":" + proxy.ProxyPort + "访问 : " + url);
                    HttpWebRequest Req;
                    HttpWebResponse Resp;
                    WebProxy proxyObject = new WebProxy(proxy.ProxyIP, proxy.ProxyPort);// port为端口号 整数型
                    Req = WebRequest.Create(url) as HttpWebRequest;
                    Req.Proxy = proxyObject; //设置代理
                    Req.Timeout = 2000;   //超时
                    Req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                    Resp = (HttpWebResponse)Req.GetResponse();
                    Encoding bin = Encoding.GetEncoding("UTF-8");
                    using (StreamReader sr = new StreamReader(Resp.GetResponseStream(), bin))
                    {
                        string str = sr.ReadToEnd();
                        if (str.Contains("异常"))
                        {
                            Resp.Close();
                            // 更新验证状态
                            proxy.State = 0;
                            getHtmlstr(url, ref html);
                        }
                        else
                        {
                            Console.WriteLine("访问成功！");
                            html = str;
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("使用" + proxy.ProxyIP + ":" + proxy.ProxyPort + " 发生 " + e.Message);
                    proxy.State = 0;
                    getHtmlstr(url, ref html);
                }
            }
        }
        /// <summary>
        /// 获取图片并保存 https://blog.csdn.net/liehuo123/article/details/81415879
        /// </summary>
        /// <param name="url">图片路径集合</param>
        /// <param name="postName">帖子名字</param>
        /// <param name="postCreateDate">帖子创建时间</param>
        static void getImgs(List<string> url, string postName, string postCreateDate)
        {
            if (postName.Equals(""))
            {
                postName = "默认文件夹";
            }
            string path = @"D:\douban\" + postCreateDate + "\\" + postName + "\\";//本地地址;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            foreach (var item in url)
            {

                Download download = new Download();
                //download.DownloadOneFileByURLWithWebClient(item, path);
                Console.WriteLine("正在保存" + postName + "的图片---");
                if (download.DownloadOneFileByURL(item, path))
                {
                    Console.WriteLine("等待0.5s----");
                    Thread.Sleep(500);
                }

            }

        }


        /// <summary>
        /// 通过登录访问    登录访问失败,账号请求过多被锁，访问403
        /// </summary>
        /// <param name="url">访问地址</param>
        /// <param name="html">得到的html字符串</param> 
        static void getHtmlByCookie(string url, ref string html)
        {
            try
            {
                HttpWebRequest Req;
                HttpWebResponse Resp;
                Req = WebRequest.Create(url) as HttpWebRequest;
                Req.Timeout = 8000;   //超时
                Req.Headers.Add("Cookie", cookie);
                Req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                Resp = (HttpWebResponse)Req.GetResponse();
                Encoding bin = Encoding.GetEncoding("UTF-8");
                using (StreamReader sr = new StreamReader(Resp.GetResponseStream(), bin))
                {
                    string str = sr.ReadToEnd();
                    if (str.Contains("异常"))
                    {
                        Console.WriteLine("");
                    }
                    else
                    {
                        html = str;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



        }

        //写一个登录去更新cookie （夭折）

        //写一个文件存json去保存ip
        /// <summary>
        ///     读取json 原文：https://blog.csdn.net/qq_36051316/article/details/79894578 
        /// </summary>
        /// <param name="desktopPath"></param>
        //private static void ReadingJson(string desktopPath)
        //{
        //    string myStr = null;
        //    //IO读取
        //    myStr = GetMyJson(desktopPath);
        //    //转换
        //    var jArray = JsonConvert.DeserializeObject<List<ProxyViewModel>>(myStr);    

        //}



    }
}
