using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Firebase.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoRunTests
{
    [TestClass]
    public class ContactTests
    {

        [TestMethod]
        public async Task SubmitContactFormAsync_WithInvalidEmail_Failure()
        {
            // Arrange
            var firebaseClientWrapperMock = new Mock<IFirebaseClientWrapper>();
            var contactService = new ContactService(firebaseClientWrapperMock.Object);

            // Act
            var result = await contactService.SubmitContactFormAsync("John Doe", "invalid_email", "1234567890", "Test message", "Individual");

            // Assert
            Assert.IsFalse(result, "Submission should fail with invalid email format");
        }

        [TestMethod]
        public async Task SubmitContactFormAsync_WithEmptyFields_Failure()
        {
            // Arrange
            var firebaseClientWrapperMock = new Mock<IFirebaseClientWrapper>();
            var contactService = new ContactService(firebaseClientWrapperMock.Object);

            // Act
            var result = await contactService.SubmitContactFormAsync("", "", "", "", "");

            // Assert
            Assert.IsFalse(result, "Submission should fail with empty fields");
        }

        [TestMethod]
        public async Task SubmitContactFormAsync_WithInvalidPhoneNumber_Failure()
        {
            // Arrange
            var firebaseClientWrapperMock = new Mock<IFirebaseClientWrapper>();
            var contactService = new ContactService(firebaseClientWrapperMock.Object);

            // Act
            var result = await contactService.SubmitContactFormAsync("John Doe", "john@example.com", "123", "Test message", "Individual");

            // Assert
            Assert.IsFalse(result, "Submission should fail with invalid phone number");
        }
        [TestMethod]
        public async Task SubmitContactFormAsync_WithInvalidName_Failure()
        {
            // Arrange
            var firebaseClientWrapperMock = new Mock<IFirebaseClientWrapper>();
            var contactService = new ContactService(firebaseClientWrapperMock.Object);

            // Act
            var result = await contactService.SubmitContactFormAsync("John", "test@example.com", "1234567890", "This is a valid message.", "Business");

            // Assert
            Assert.IsFalse(result, "Submission should fail with an invalid name");
        }

        [TestMethod]
        public async Task SubmitContactFormAsync_WithInvalidMessage_Failure()
        {
            // Arrange
            var firebaseClientWrapperMock = new Mock<IFirebaseClientWrapper>();
            var contactService = new ContactService(firebaseClientWrapperMock.Object);

            // Act
            var result = await contactService.SubmitContactFormAsync("John Doe", "test@example.com", "1234567890", "Short", "Business");

            // Assert
            Assert.IsFalse(result, "Submission should fail with an invalid message");
        }

        [TestMethod]
        public async Task SubmitContactFormAsync_WithInvalidUserType_Failure()
        {
            // Arrange
            var firebaseClientWrapperMock = new Mock<IFirebaseClientWrapper>();
            var contactService = new ContactService(firebaseClientWrapperMock.Object);

            // Act
            var result = await contactService.SubmitContactFormAsync("John Doe", "test@example.com", "1234567890", "This is a valid message.", "InvalidType");

            // Assert
            Assert.IsFalse(result, "Submission should fail with an invalid user type");
        }
    }
}