using ContactClass.Interface;
using DomainModel.Contact;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Linq;

namespace ContactClass
{
    public class ContactEmail : IContactEmail 
    {

        // any log framework
        public EventLog ContactLog;

        public ContactEmail()
        {
            try
            {
                // Sample only, use any existing logging framework you prefer
                // Create the log source, if it does not already exist.
                //if (!EventLog.SourceExists("Contact"))
                //{
                //    EventLog.CreateEventSource("Contact", "ContactLog");
                //}

                //ContactLog = new EventLog("Contact");
            }
            catch (Exception ex)
            {

            }

        }

        //private bool mailSent = false;

        public bool ValidateEmail(string email)
        {
            bool validation = true;
            // could do more checks rather than just email format check in front-end
            // for example, check Spam email list.
            string[] blockedEmailList = new string[]{ "phishing@phishing.com", "attacker@attack.com", "xxx@xxxx.com" };
            validation = !blockedEmailList.Contains(email);

            return validation;
        }
        public bool ValidatePhone(string phone)
        {
            // could do furtuher checks rather than just phone number format check in front-end
            // for example, check Spam phone number list.
            bool validation = true;
            // could do more checks rather than just email format check in front-end
            // for example, check Spam email list.
            string[] blockedPhoneList = new string[] { "000", "1234567", "99999999" };
            validation = !blockedPhoneList.Contains(phone);

            return validation;
        }

        public bool KeywordEscalator(string comment)
        {
            //To do...
            return true;
        }

        public string SendEmail(Contact contact)
        {
            string result = "";
            try
            {
                if (ValidateEmail(contact.Emaill))
                {
                    return "Invalid Email.";
                }

                if (ValidatePhone(contact.PhoneNumber))
                {
                    return "Invalid Phone Number.";
                }

                // SMTPSever retrieved from config file
                SmtpClient client = new SmtpClient(Settings1.Default.SMTPServer);
                // Specify the e-mail sender.
                MailAddress from = new MailAddress(contact.Emaill,
                   contact.FirstName + (char)0xD8 + contact.LastName,
                System.Text.Encoding.UTF8);
                // EmailTo retrieved from config file
                MailAddress to = new MailAddress(Settings1.Default.EmailTo);
                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.Body = contact.Comment;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = contact.EnquiryType + "##" + contact.PhoneNumber;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                // For this example, the userToken is phoneNumber + timestamp to identify this send operation.
                string userState = contact.PhoneNumber + DateTime.Now.ToString();
                
                // Send email 
                client.SendAsync(message, userState);

                // Clean up.
                message.Dispose();
            }
            catch (Exception ex)
            {
                // Sample only, Write an error entry to the event log.  
                //ContactLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                result = "Fail to send the enquiry. Please try again later.";
            }
            
            return result;
        }
    }
}
