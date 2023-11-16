using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoRunTests
{

        public interface IFirebaseAuthService
        {
            Task<Firebase.Auth.User> CreateUserWithEmailAndPasswordAsync(string email, string password);
        }

        public class RegistrationService
        {
            private readonly IFirebaseAuthService _firebaseAuthService;

            public RegistrationService(IFirebaseAuthService firebaseAuthService)
            {
                _firebaseAuthService = firebaseAuthService;
            }

            public async Task<bool> RegisterUserAsync(string email, string password, string confirmPassword)
            {
                if (password != confirmPassword)
                {
                    // Password mismatch, handle accordingly
                    return false;
                }

                try
                {
                    // Attempt user registration with Firebase Auth service
                    var user = await _firebaseAuthService.CreateUserWithEmailAndPasswordAsync(email, password);
                    return user != null; // Return true if user creation was successful
                }
                catch (Firebase.Auth.FirebaseAuthException ex)
                {
                    // Handle Firebase Auth exceptions (e.g., weak password, user exists, etc.)
                    return false;
                }
            }
        }
    }

