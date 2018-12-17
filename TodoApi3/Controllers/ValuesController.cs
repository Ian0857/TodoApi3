using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoApi3.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TodoApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string flyFoveToken = string.Empty;
        public ValuesController(IConfigurationRoot config)
        {
            flyFoveToken = config["AppConfiguration:FlyfoveToken"];
            var bbb = "aaa";
        }

        // https://localhost:44322/api/values?sms_id=aa&sms_code=bb&update_time=cc
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromQuery]string sms_id, [FromQuery]string sms_code, [FromQuery]string update_time)
        {
            string cmmmm = DateTime.Now.ToString("yyMMddHHmmss");
            return new string[] { sms_code, update_time };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id,[FromQuery]string value, [FromQuery]string fff="Ok")
        {
            return fff;
        }


        [HttpPost]
        public ActionResult<bool> Post0(ReInputItems item)
        {
            var aa = "abc";
            return false;
        }
        // POST api/values
        [HttpPost]
        public ActionResult<bool> Post1(InputItem item)
        {
            ReInputItems reInput = new ReInputItems();

            try
            {
                bool status = false;
                string url = "https://api.smartdove.net/index.php?r=smsApi/SendOneSms";
                string responseString = string.Empty;
                string phone_number = item.MobileNumber;
                string content = item.MessageContent;
                string campaign_id = "test123";

                SendMbMessage inputFlyFove = new SendMbMessage()
                {
                    token = flyFoveToken,
                    phone_number = phone_number,
                    content = content,
                    campaign_id = campaign_id
                };

                string postData = string.Format("token={0}&phone_number={1}&content={2}&campaign_id={3}", flyFoveToken, phone_number, content, campaign_id);
                byte[] bs = Encoding.UTF8.GetBytes(postData);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bs.Length;

                //using (Stream reqStream = req.GetRequestStream())
                //{
                //    reqStream.Write(bs, 0, bs.Length);
                //    reqStream.Close();
                //}
                //responseString = GetResponse(req);

                //var aaaa = "{\"is_error\":true,\"error_code\":0,\"error_message\":\"\",\"sms_id\":\"5\",\"sms_code\":\"1\"}";
                var bbb = "aasss";
                //var bbb = JsonConvert.DeserializeObject(aaaa);
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseString);
                string is_error = jo["is_error"].ToString();

                if (!Convert.ToBoolean(is_error))
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private string GetResponse(HttpWebRequest req)
        {
            req.Timeout = 30000; //millisecond timeout

            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                if(resp.StatusCode != HttpStatusCode.OK)
                    return string.Empty;

                var ccc = resp.StatusCode;

                Stream responseStream = resp.GetResponseStream();

                using (StreamReader readerStream = new StreamReader(responseStream, Encoding.UTF8))
                {
                    return readerStream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
