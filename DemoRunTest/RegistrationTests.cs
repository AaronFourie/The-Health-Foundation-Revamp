using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace DemoRunTests
{
    [TestClass]
    public class RegistrationTests
    {
        [TestMethod]
        public async Task Registration_WithValidInput_Success()
        {
            // Arrange
            var firebaseAuthMock = new Mock<IFirebaseAuthService>();
            var registrationService = new RegistrationService(firebaseAuthMock.Object);

            // Mock successful user registration
            firebaseAuthMock.Setup(auth => auth.CreateUserWithEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(new Firebase.Auth.User());

            // Act
            var result = await registrationService.RegisterUserAsync("test123@gmail.com", "Password12!", "Password12!");

            // Assert
            Assert.IsTrue(result, "User should be registered successfully: Unique email, password length >= 8 characters, passwords match");
        }

        [TestMethod]
        public async Task Registration_WithPasswordMismatch_Failure()
        {
            // Arrange
            var firebaseAuthMock = new Mock<IFirebaseAuthService>();
            var registrationService = new RegistrationService(firebaseAuthMock.Object);

            // Act
            var result = await registrationService.RegisterUserAsync("test@example.com", "password1", "password2");

            // Assert
            Assert.IsFalse(result, "User registration should fail due to password mismatch");
        }

        [TestMethod]
        public async Task Registration_WithExistingEmail_Failure()
        {
            // Arrange
            var firebaseAuthMock = new Mock<IFirebaseAuthService>();
            var registrationService = new RegistrationService(firebaseAuthMock.Object);

            // Mock registration throwing an exception indicating an existing email
            firebaseAuthMock
                .Setup(auth => auth.CreateUserWithEmailAndPasswordAsync("aaronfourie16@gmail.com", "m3gaCap16!!"))
                .ThrowsAsync(new Exception("The email address is already in use by another account."));

            // Act and Assert
            try
            {
                var result = await registrationService.RegisterUserAsync("aaronfourie16@gmail.com", "m3gaCap16!!", "m3gaCap16!!");
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("The email address is already in use by another account.", ex.Message, "Exception message doesn't match");
            }
        }
    }
}
