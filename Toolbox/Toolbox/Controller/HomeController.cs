using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Toolbox.Controller
{
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }
        // Will come back to this! Doing a course and reading documentation on .ASP Net Core Framework
        public string Index()
        {
            return "This is my default action...";
        }

    }
}
