using MyMvcProject.CommonLayer.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcProject.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetUserName()
        {
            if (CurrentSession.User != null)
            {
                return CurrentSession.User.UserName;
            }
            return "system";
        }
    }
}