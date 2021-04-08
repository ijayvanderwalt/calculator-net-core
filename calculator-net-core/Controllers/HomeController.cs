using calculator_net_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace calculator_net_core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("/evaluate/{expression}")]
        public IActionResult Evaluate(string expression)
        {
            DataTable data = new DataTable();
            try
            {
                //I had to do my own encoding for the operators as they are reserved characters which are not allowed in URL's
                //Furthermore I used the simplest method to evaluate a string expression which is datatable.Compute similar to Js eval()
                var result = data.Compute(
                    expression.Replace("plus","+").Replace("minus","-").Replace("divide","/").Replace("multiply","*"), "");
                return Ok(result);
            }
            catch (Exception)
            {
                return Ok("There seems to be a syntax error!");
            }
            
        }
    }
}
