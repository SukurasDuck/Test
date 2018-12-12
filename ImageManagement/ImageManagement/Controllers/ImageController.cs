using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> img)
        {
            Models.ApiImgResultModel imgResult = new Models.ApiImgResultModel();
            long size = img.Sum(f => f.Length);
            
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach(var formFile in img)
            {
                if(formFile.Length > 0)
                {
                    using(var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        string imgBase64String = Convert.ToBase64String(stream.ToArray());
                       imgResult=ImageManagement.Helper.BaiduApiHelper.general_basic(imgBase64String);
                        
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            
            return View("Index",imgResult);
        }
    }
}