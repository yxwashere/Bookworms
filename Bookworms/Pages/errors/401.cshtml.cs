using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookworms.Pages.errors
{
    public class _401Model : PageModel
    {
        private readonly ILogger<_401Model> _logger;

        public _401Model(ILogger<_401Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
