using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyRazorApp.Pages //Definicja przestrzeni nazw, w której znajduje siê kod. W tym przypadku to przestrzeñ nazw dla aplikacji MyRazorApp i strony Pages.
{
    public class IndexModel : PageModel   //Definicja klasy IndexModel, która dziedziczy z klasy PageModel. Jest to model strony w aplikacji Razor Pages. Klasa PageModel zarz¹dza logik¹ i danymi widoku.
    {
        public class UserInput //Definiowanie klasy UserInput, która przechowuje dane wejœciowe od u¿ytkownika. Jest to wewnêtrzna klasa modelu IndexModel.
        {
            [Required(ErrorMessage = "Imiê jest wymagane.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Nazwisko jest wymagane.")]
            public string Nazwisko { get; set; }

            [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
            [CustomValidation(typeof(IndexModel), nameof(ValidateEmail))]
            public string Email { get; set; }
           

            [Required(ErrorMessage = "Has³o jest wymagane.")]
            [DataType(DataType.Password)]
            [MinLength(8, ErrorMessage = "Has³o musi mieæ co najmniej 8 znaków.")]
            [CustomValidation(typeof(IndexModel), nameof(ValidatePassword))]
            public string Password { get; set; }
        }

        [BindProperty] //Atrybut BindProperty pozwala automatycznie powi¹zaæ dane wejœciowe z formularza z w³aœciwoœciami klasy UserInput podczas przesy³ania formularza.
        public UserInput UserData { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Wróæ do strony, jeœli walidacja siê nie powiedzie
            }

            // Po poprawnej walidacji
            return RedirectToPage("Success", new { Name = UserData.Name, Nazwisko = UserData.Nazwisko });
        }


        // Metoda waliduj¹ca adres e-mail
        public static ValidationResult ValidateEmail(string email, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult("Adres e-mail jest wymagany.");

            if (!email.Contains("@"))
            {
                return new ValidationResult("Adres e-mail musi zawieraæ znak '@'.");
            }
            return ValidationResult.Success;
        }


        // Metoda waliduj¹ca has³o
        public static ValidationResult ValidatePassword(string password, ValidationContext context)
        {
            if (string.IsNullOrEmpty(password)) // inne atrybutu ju¿ to sprawdzi³y
                return ValidationResult.Success;

            // Sprawdzanie, czy has³o zawiera du¿¹ literê
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Has³o musi zawieraæ co najmniej jedn¹ du¿¹ literê.");

            // Sprawdzanie, czy has³o zawiera cyfrê
            if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult("Has³o musi zawieraæ co najmniej jedn¹ cyfrê.");

            return ValidationResult.Success;
        }
    }
}
