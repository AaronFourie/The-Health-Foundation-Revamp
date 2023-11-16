using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace DemoRunTests
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public async Task Login_WithValidCredentials_Success()
        {
            // Arrange
            var firebaseAuthMock = new Mock<IFirebaseAuthService1>();
            var loginService = new LoginService(firebaseAuthMock.Object);

            // Mock successful user login
            firebaseAuthMock.Setup(auth => auth.SignInWithEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(new Firebase.Auth.User());

            // Act
            var result = await loginService.LoginUserAsync("aaronfourie16@gmail.com", "m3gaCap16!!");

            // Assert
            Assert.IsTrue(result, "User login should succeed with valid credentials");
        }

        [TestMethod]
        public async Task Login_WithInvalidCredentials_Failure()
        {
            // Arrange
            var firebaseAuthMock = new Mock<IFirebaseAuthService1>();
            var loginService = new LoginService(firebaseAuthMock.Object);

            // Mock login failure
            firebaseAuthMock.Setup(auth => auth.SignInWithEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ThrowsAsync(new Exception("Login Unsuccessfull: Invalid login credentials."));

            // Assert
            // Act and Assert
            try
            {
                var result = await loginService.LoginUserAsync("aaronfourie16@gmail.com", "12345678");
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Login Unsuccessfull: Invalid login credentials.", ex.Message, "Exception message doesn't match");
            }
        }
    }
}