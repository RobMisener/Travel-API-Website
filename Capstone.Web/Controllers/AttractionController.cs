using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Capstone.Web.Authentication;
using Capstone.Web.Models;


namespace Capstone.Web.Controllers
{
    public class AttractionController : Controller
    {
        // GET: AttractionDetail
        public ActionResult Attraction()
        {
            return View();
        }
    }
}