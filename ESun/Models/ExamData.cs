using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESun.Models
{
    public class ExamData
    {
        public string 年度 { get; set; }
        public string 月份 { get; set; }
        public string 現金門票收入 { get; set; }
        public string 多卡通門票收入 { get; set; }
        public string 門票收入合計 { get; set; }
        public string 當月購票入園人數 { get; set; }
    }
}