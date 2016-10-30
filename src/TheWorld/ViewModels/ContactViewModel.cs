using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "No es una dirección de correo válida")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]               
        [StringLength(4096, MinimumLength = 5)] 
        public string Message { get; set; }
    }
}
