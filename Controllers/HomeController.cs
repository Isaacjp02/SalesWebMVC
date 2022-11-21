using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers;

public class HomeController : Controller
{
 
    public IActionResult Index()
    {
        return View();
    }


}
