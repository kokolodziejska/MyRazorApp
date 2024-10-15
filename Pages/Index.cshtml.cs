using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyRazorApp.Pages //Definicja przestrzeni nazw, w kt�rej znajduje si� kod. W tym przypadku to przestrze� nazw dla aplikacji MyRazorApp i strony Pages.
{
    public class IndexModel : PageModel   //Definicja klasy IndexModel, kt�ra dziedziczy z klasy PageModel. Jest to model strony w aplikacji Razor Pages. Klasa PageModel zarz�dza logik� i danymi widoku.
    {
        public class UserInput //Definiowanie klasy UserInput, kt�ra przechowuje dane wej�ciowe od u�ytkownika. Jest to wewn�trzna klasa modelu IndexModel.
        {
            [Required(ErrorMessage = "Imi� jest wymagane.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Nazwisko jest wymagane.")]
            public string Nazwisko { get; set; }

            [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
            [CustomValidation(typeof(IndexModel), nameof(ValidateEmail))]
            public string Email { get; set; }
           

            [Required(ErrorMessage = "Has�o jest wymagane.")]
            [DataType(DataType.Password)]
            [MinLength(8, ErrorMessage = "Has�o musi mie� co najmniej 8 znak�w.")]
            [CustomValidation(typeof(IndexModel), nameof(ValidatePassword))]
            public string Password { get; set; }
        }

        [BindProperty] //Atrybut BindProperty pozwala automatycznie powi�za� dane wej�ciowe z formularza z w�a�ciwo�ciami klasy UserInput podczas przesy�ania formularza.
        public UserInput UserData { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Wr�� do strony, je�li walidacja si� nie powiedzie
            }

            // Po poprawnej walidacji
            return RedirectToPage("Success", new { Name = UserData.Name, Nazwisko = UserData.Nazwisko });
        }


        // Metoda waliduj�ca adres e-mail
        public static ValidationResult ValidateEmail(string email, ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult("Adres e-mail jest wymagany.");

            if (!email.Contains("@"))
            {
                return new ValidationResult("Adres e-mail musi zawiera� znak '@'.");
            }
            return ValidationResult.Success;
        }


        // Metoda waliduj�ca has�o
        public static ValidationResult ValidatePassword(string password, ValidationContext context)
        {
            if (string.IsNullOrEmpty(password)) // inne atrybutu ju� to sprawdzi�y
                return ValidationResult.Success;

            // Sprawdzanie, czy has�o zawiera du�� liter�
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Has�o musi zawiera� co najmniej jedn� du�� liter�.");

            // Sprawdzanie, czy has�o zawiera cyfr�
            if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult("Has�o musi zawiera� co najmniej jedn� cyfr�.");

            return ValidationResult.Success;
        }
    }
}
