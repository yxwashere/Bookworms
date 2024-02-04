using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookworms.Pages.errors
{
    public class _500Model : PageModel
    {
        private readonly ILogger<_500Model> _logger;

        public _500Model(ILogger<_500Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
