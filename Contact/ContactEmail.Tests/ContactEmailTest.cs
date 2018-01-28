using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactClass;
using ContactClass.Interface;


namespace ContactEmail.Tests
{
    [TestClass]
    public class ContactEmailTest
    {
        [TestMethod]
        public void TestValidateEmailValid()
        {
            // Arrange
            IContactEmail contactEmail = new ContactClass.ContactEmail();
            string validEmail = "shanli.pro@gmail.com";
            // Act
            bool result = contactEmail.ValidateEmail(validEmail);

            // Assert
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestValidateEmailInvalid()
        {
            // Arrange
            IContactEmail contactEmail = new ContactClass.ContactEmail();
            // in spam block list
            string validEmail = "phishing@phishing.com";
            // Act
            bool result = contactEmail.ValidateEmail(validEmail);

            // Assert
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        // and more...
    }
}
