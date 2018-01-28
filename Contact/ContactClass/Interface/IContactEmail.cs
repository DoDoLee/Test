using DomainModel.Contact;

namespace ContactClass.Interface
{
    public interface IContactEmail
    {
        //further email and phone number validation
        bool ValidateEmail(string email);
        bool ValidatePhone(string phone);
        // when keyword occurs in comments, prioritize the email to dedicated team
        bool KeywordEscalator(string comment);

        string SendEmail(Contact contact);
    }
}
