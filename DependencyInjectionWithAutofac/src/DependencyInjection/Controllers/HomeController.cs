using DependencyInjection.IApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGuidTransientAppService _guidTransientAppService;

        private readonly IGuidScopedAppService _guidScopedAppService;

        private readonly IGuidSingletonAppService _guidSingletonAppService;
        
        public HomeController(IGuidTransientAppService guidTransientAppService,
            IGuidScopedAppService guidScopedAppService, IGuidSingletonAppService guidSingletonAppService)
        {
            _guidTransientAppService = guidTransientAppService;
            _guidScopedAppService = guidScopedAppService;
            _guidSingletonAppService = guidSingletonAppService;
        }


        public IActionResult Index()
        {
            ViewBag.TransientItem = _guidTransientAppService.GuidItem();
            ViewBag.ScopedItem = _guidScopedAppService.GuidItem();
            ViewBag.SingletonItem = _guidSingletonAppService.GuidItem();

            return View();
        }
        
    }
}