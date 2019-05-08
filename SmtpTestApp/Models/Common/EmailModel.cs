using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmtpTestApp.Models.Common
{
    public class EmailModel
    {
        public string ToMails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}