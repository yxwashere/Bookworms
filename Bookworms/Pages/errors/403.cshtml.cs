using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookworms.Pages.errors
{
    public class _403Model : PageModel
    {
        private readonly ILogger<_403Model> _logger;

        public _403Model(ILogger<_403Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
