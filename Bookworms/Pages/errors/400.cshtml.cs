using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookworms.Pages.errors
{
    public class _400Model : PageModel
    {
        private readonly ILogger<_400Model> _logger;

        public _400Model(ILogger<_400Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
