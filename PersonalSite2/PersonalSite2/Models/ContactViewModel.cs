using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PersonalSite2.Models
{
    [Keyless]
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Name is required")] //Makes the field required
        public string Name { get; set; }

        [Required(ErrorMessage = "*Email is required")]
        //[DataType(DataType.EmailAddress)] //Certain formatiing is expected (@ symbol, .com, etc.)
        [EmailAddress(ErrorMessage = "*Must be a valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "*Message is required")]
        [DataType(DataType.MultilineText)] //Makes the textbox for this field bigger
        public string Message { get; set; }
    }
}
