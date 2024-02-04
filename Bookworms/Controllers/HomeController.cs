using Microsoft.AspNetCore.Mvc;

namespace Bookworms.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpContextAccessor contxt;
		public HomeController(IHttpContextAccessor httpContextAccessor)
		{
			contxt = httpContextAccessor;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
