using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi3.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;
using DingOkMovieApi.Tools;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace TodoApi3.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context,IConfigurationRoot config)
       {
            var flyFoveToken = config["AppConfiguration:FlyfoveToken"];
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //[HttpPost]
        //public async Task<string> Verification([FromForm]string mobilNo)
        //{
        //    string baseUri = "https://api.smartdove.net/index.php?r=smsApi/SendOneSms";
        //    string token = "18667b283e75c2bbf4dd9ebbd4c440d1";
        //    string phone_number = mobilNo;
        //    string content = "test smsl";
        //    string campaign_id = "test123"; //+ DateTime.Now.ToString("yyMMddHHmmss");
        //    string response_url = "https//:api.test.com/update.php";

        //    SendMbMessage inputs2 = new SendMbMessage()
        //    {
        //        token = token,
        //        phone_number = phone_number,
        //        content = content,
        //        campaign_id = campaign_id
        //        //response_url = response_url
        //    };

        //    string strJson2 = JsonConvert.SerializeObject(inputs2, Formatting.Indented);
        //    NameValueCollection postParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
        //    postParams.Add("token", token);
        //    postParams.Add("phone_number", phone_number);
        //    postParams.Add("content", "test smsl");
        //    postParams.Add("campaign_id", "test123");

        //    var sss = postParams.ToString();

        //    var requestUri = $"{baseUri}";
        //    var response = await HttpRequestFactory.Post(requestUri, inputs2);

        //    Console.WriteLine($"Status: {response.StatusCode}");
        //    var outputModel = response.ContentAsType<List<ResMessage>>();
        //    outputModel.ForEach(item =>
        //                    Console.WriteLine("{0} - {1}", item.is_error, item.error_message));
        //    return "aaa";
        //}

        [HttpPost]
        public string Verification([FromForm]string mobilNo)
        {
            string url = "https://api.smartdove.net/index.php?r=smsApi/SendOneSms";
            string responseString = string.Empty;

            string token = "18667b283e75c2bbf4dd9ebbd4c440d1";
            string phone_number = mobilNo;
            string content = "test smsl";
            string campaign_id = "test123"; //+ DateTime.Now.ToString("yyMMddHHmmss");
            string response_url = "https//:api.test.com/update.php";

            //SendMbMessage inputs = new SendMbMessage();
            //inputs.Token = token;
            //inputs.Phone_number = phone_number;
            //inputs.Content = content;
            //inputs.Campaign_id = campaign_id;
            //inputs.Response_url = response_url;

            SendMbMessage inputs2 = new SendMbMessage()
            {
                token = token,
                phone_number = phone_number,
                content = content,
                campaign_id = campaign_id
                //response_url = response_url
            };

            string postData = "token=18667b283e75c2bbf4dd9ebbd4c440d1&phone_number=0912877857&content=test+0821&campaign_id=test1234";
            string strJson2 = JsonConvert.SerializeObject(inputs2, Formatting.Indented);

            byte[] bs = Encoding.UTF8.GetBytes(postData);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            req.Timeout = 30000;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }

            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            //Stream stream = resp.GetResponseStream();

            //using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //{
            //    responseString = reader.ReadToEnd();
            //}

            //using (StreamWriter requestWriter = new StreamWriter(wr.GetRequestStream()))
            //{
            //    requestWriter.Write(strJson2);
            //}

            //responseString = GetResponse(wr);
            return "aab";
        }

        //[HttpPost]
        //public IActionResult Create(TodoItem item)
        //{
        //    _context.TodoItems.Add(item);
        //    _context.SaveChanges();

        //    return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        //}

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }   

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

        private string GetResponse(HttpWebRequest wr)
        {
            wr.Timeout = 1000; //millisecond timeout

            try
            {
                HttpWebResponse response = (HttpWebResponse)wr.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                    return String.Empty;

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readerStream = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                    {
                        return readerStream.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}