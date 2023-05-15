using ESun.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ESun.Controllers
{
    public class ExamController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Exam
        public ActionResult Index()
        {
            string url = "https://odws.hccg.gov.tw/001/Upload/25/opendataback/9059/879/1ca8d112-4b43-401e-9a0e-fd75bc3a6ec7.json";
            WebRequest request = WebRequest.Create(url);  //创建连接
            WebResponse response = request.GetResponse();
            Stream webstream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(webstream);

            string json = streamReader.ReadToEnd();  //获取json数据
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<ExamData> rootObject = js.Deserialize<List<ExamData>>(json);

            ViewData.Model = rootObject;

            foreach(ExamData data in rootObject)
            {
                //資料沒有重覆才寫入
                if (!db.zoo.Any(x => x.year == data.年度 && x.month == data.月份))
                {
                    zoo zoodata = new zoo();
                    zoodata.year = data.年度;
                    zoodata.month = data.月份;
                    zoodata.cash = data.現金門票收入;
                    zoodata.multicard = data.多卡通門票收入;
                    zoodata.ticket = data.現金門票收入;
                    zoodata.people = data.當月購票入園人數;
                    db.zoo.Add(zoodata);
                }
            }
            db.SaveChanges();

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}