using SmtpTestApp.Models;
using SmtpTestApp.Models.Common;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SmtpTestApp.Controllers
{
    public class HomeController : MainController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmailModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new NullReferenceException();
            }

            //Extension.SendMail(model.ToMails, model.Subject, model.Body);
            model.ToMails.SendMail(model.Subject, model.Body);

            return View();
        }

        [HttpPost]
        public ActionResult Subscribe(string email)
        {
            if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                using (var db = new SmtpDbContext())
                {
                    if (db.Subscribers.Any(s=>s.Email==email))
                    {
                        ViewBag.ErrorMessage = "Artiq qeydiyyatdan kecmisiniz!";
                        return View("Index");
                    }

                    string key = Guid.NewGuid().ToString().Replace("-", "");
                    string replyLink = string.Concat(ConfigurationManager.AppSettings.Get("siteDomainName"),
                        "/Home/CheckSubscribe?key=", key);

                    //ConfigurationManager.AppSettings.Get("siteDomainName")
                    // Home/CheckSubscribe
                    // ?key=

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"<h1>Hello,{email}!{Environment.NewLine}</h1>");
                    builder.AppendLine($"Please check this link!{Environment.NewLine}<a href='{replyLink}'>{replyLink}</a>");

                    if (email.SendMail("New Subscriber", builder.ToString()))
                    {
                        db.Subscribers.Add(new Models.Entity.Subscriber
                        {
                            Email = email,
                            Key = key,
                            CreateDate = DateTime.Now
                        });
                        db.SaveChanges();
                    }

                    builder.Clear();
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Email Duzgun Deyil!";
            }
            return View("Index");
        }

        public ActionResult CheckSubscribe(string key)
        {
            using (var db = new SmtpDbContext())
            {
                var current=db.Subscribers.FirstOrDefault(s => s.Key == key && !s.IsActive);
                if (current==null)
                {
                    ViewBag.ErrorMessage = "Gozleyen ticket yoxdur!";
                    return View("Index");
                }
                current.IsActive = true;
                db.SaveChanges();
            }
            return View();
        }
    }
}