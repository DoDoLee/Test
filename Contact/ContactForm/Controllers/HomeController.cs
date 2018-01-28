using ContactClass.Interface;
using ContactForm.Models;
using DomainModel.Contact;
using System.Web.Mvc;

namespace ContactForm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ContactViewModel m = new ContactViewModel();
          
            return View(m);
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contactModel, FormCollection form)
        {
            contactModel.EnquiryType = form["ContactType"].ToString();
            IContactEmail contactEmail  = new ContactClass.ContactEmail();

            if (ModelState.IsValid)
            {
                //prepare and pass contact for background processing
                Contact contact = new Contact();
                contact.FirstName = contactModel.FirstName;
                contact.LastName = contactModel.LastName;
                contact.Emaill = contactModel.Emaill;
                contact.EnquiryType = contactModel.EnquiryType;
                contact.PhoneNumber = contactModel.PhoneNumber;
                contact.Comment = contactModel.Comment;

                string receipt = contactEmail.SendEmail(contact);

                // Successful
                if (string.IsNullOrEmpty(receipt))
                {
                    contactModel.EmailSent = true;
                    contactModel.ErrorMessage = "";
                }
                // Fail to send email
                else
                {
                    contactModel.EmailSent = false;
                    // could be customized error message
                    contactModel.ErrorMessage = receipt;
                }
            }

            return View("Index",contactModel);
        }
    }
}