using Company.Day02.PL.Models;
using Company.Day02.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.Day02.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;
        private readonly ITransentService transentService01;
        private readonly ITransentService transentService02;
        private readonly ISingletonService singletonService01;
        private readonly ISingletonService singletonService02;

        public HomeController(ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransentService transentService01,
            ITransentService transentService02,
            ISingletonService singletonService01,
            ISingletonService singletonService02
            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.transentService01 = transentService01;
            this.transentService02 = transentService02;
            this.singletonService01 = singletonService01;
            this.singletonService02 = singletonService02;
        }


        public string TestLifeTime()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"scopedService01 ::  {scopedService01.GetGuid()}\n");
            stringBuilder.Append($"scopedService02 ::  {scopedService02.GetGuid()}\n\n");
            stringBuilder.Append($"transentService01 ::  {transentService01.GetGuid()}\n");
            stringBuilder.Append($"transentService02 ::  {transentService02.GetGuid()} \n\n");
            stringBuilder.Append($"singletonService01 ::  {singletonService01.GetGuid()}\n");
            stringBuilder.Append($"singletonService02 ::  {singletonService02.GetGuid()} \n\n");

            return stringBuilder.ToString();
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
    }
}
