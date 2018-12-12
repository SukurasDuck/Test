using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageManagement.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Controllers
{
    public class ExcelController : Controller
    {
        public IActionResult Index()
        {
            return View(new Models.ApiExcelResultModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> excel)
        {
            Models.ApiExcelResultModel imgResult = new Models.ApiExcelResultModel();
            long size = excel.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach(var formFile in excel)
            {
                if(formFile.Length > 0)
                {
                    using(var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        string imgBase64String = Convert.ToBase64String(stream.ToArray());
                        imgResult = ImageManagement.Helper.BaiduApiHelper.form_ocr_request(imgBase64String);

                    }
                }
            }
            Console.WriteLine(imgResult.result.FirstOrDefault().request_id);
            ViewBag.requestId = imgResult.result.FirstOrDefault().request_id;
            return View("Index",imgResult);
        }

        public IActionResult Get_request_result(string request_id,int? type)
        {
            Models.ApiExcelResultModel result = BaiduApiHelper.form_ocr_get_request_result(request_id, type ?? 0);
            return View("Index", result);
        }
        
    }
}