using System.ComponentModel.DataAnnotations;
namespace ContactForm.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Emaill { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        public string EnquiryType { get; set; }
        public string Comment { get; set; }
        // to do
        public string VerificationText { get; set; }

        public string ErrorMessage { get; set; }

        public bool EmailSent { get; set; }
    }
}