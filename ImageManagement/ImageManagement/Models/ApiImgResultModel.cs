using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageManagement.Models
{
    public class ApiImgResultModel
    {
        public int? direction { get; set; }
        public string log_id { get; set; }
        public List<string> words_result { get; set; }
        public int words_result_num { get; set; }
        public string words { get; set; }
        public string probability { get; set; }
        public string request_id { get; set; }
    }
}
