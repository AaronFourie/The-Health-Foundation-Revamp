using System;
using System.Threading.Tasks;

namespace DemoRunTests
{
    public interface IFirebaseAuthService1
    {
        Task<Firebase.Auth.User> SignInWithEmailAndPasswordAsync(string email, string password);
    }

    public class LoginService
    {
        private readonly IFirebaseAuthService1 _firebaseAuthService1;

        public LoginService(IFirebaseAuthService1 firebaseAuthService)
        {
            _firebaseAuthService1 = firebaseAuthService;
        }

        public async Task<bool> LoginUserAsync(string email, string password)
        {
            try
            {
                // Attempt user login with Firebase Auth service
                var user = await _firebaseAuthService1.SignInWithEmailAndPasswordAsync(email, password);
                return user != null; // Return true if login was successful
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                // Handle Firebase Auth exceptions (e.g., invalid email, wrong password, etc.)
                return false;
            }
        }
    }
}
