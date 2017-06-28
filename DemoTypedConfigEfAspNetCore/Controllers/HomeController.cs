using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DemoTypedConfigEfAspNetCore.AppSetting;
using System.Diagnostics;

namespace DemoTypedConfigEfAspNetCore.Controllers
{
    public class HomeController
        : Controller
    {
        public HomeController(IOptions<LdapSetting> ldapSetting, IOptions<Setting> wholeSettings)
        {
            Debugger.Break();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}