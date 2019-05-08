using SmtpTestApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmtpTestApp.Controllers
{
    public class MainController : Controller
    {
        public User CurrentUser {
            get {
                var user = Session[SessionKey.User] as User;

                //if (user==null)
                //{
                //    //Response.Clear();
                //    //Response.RedirectToRoute("Default",new RouteValueDictionary(new {
                //    //    controller="Account",
                //    //    action="Login"
                //    //}));
                //    //Response.End();
                //}

                return user;
            } }
    }
}