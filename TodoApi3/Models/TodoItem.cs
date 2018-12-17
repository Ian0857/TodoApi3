using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi3.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class SendMbMessage
    {
        public string token {get;set;}
        public string phone_number { get; set; }
        public string content { get; set; }
        public string campaign_id { get; set; }
    }

    public class ResMessage
    {
        public string is_error { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }

    public class Setting
    {
        public string FlyToken { get; set; }
    }

    public class InputItem
    {
        public string MobileNumber { get; set; }
        public string MessageContent { get; set; }
    }

    public class ReInputItems
    {
        string Status { get; set; }
        string MobileNumber { get; set; }
        string MessageContent { get; set; }
    }
}
