using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorApp.Pages
{
    public class SuccessModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Nazwisko { get; set; }

        public void OnGet()
        {
            // Model bêdzie wype³niony danymi z przekierowania
        }
    }
}