using System.Collections.Generic;

namespace ImageManagement.Models
{
    public class ApiExcelResultModel
    {
        public string log_id { get; set; }
        public List<Resultmodel> result { get; set; }
    
    }

    public class Resultmodel
    {
        public string request_id { get; set; }
        public string result_data { get; set; }
        public int percent { get; set; }
        public int ret_code { get; set; }
        public string ret_msg { get; set; }
    }
}